using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LitJson;
using UnityEngine.UI;

public class DataObtainer : MonoBehaviour {

  // APART FROM "LOGIN" AND "TOKENRECEIVER", 
  // THIS CLASS OBTAINS ALL DATA FROM API AND GIVES IT TO GAMEINFO

  public string urlProjects = "http://semicolon.multimediatechnology.be/api/v1/projects?token=";
  public string urlProposalsPartOne = "http://semicolon.multimediatechnology.be/api/v1/projects/";
  public string urlProposalsPartTwo = "/proposals/user?token=";
  public string urlAuthenticateUser = "http://semicolon.multimediatechnology.be/api/v1/authenticate/user?token=";
  public Text loadPercentage;
  public Canvas canvasWithMask;
  public GameObject loadScenePanel;
  public Image loadingBar;


  static int numberOfProjects = 0;
  static int numberOfProposals = 0;

  int progress = 0, total = 1;

  
  static WWW www;
  JsonData textData;
  GameInfo GI;
  bool isSynced = false;

  List<string> projectBannerList = new List<string>();
  List<string> projectNamesList = new List<string>();
  List<string> placeNamesList = new List<string>();
  List<string> projectDescriptionsList = new List<string>();
  List<int> projectIDsList = new List<int>();

  string[][] questionsArray = null;
  int[][] questionsIDArray = null;
  int[][] questionsTypeArray = null;

  string projectNameString = "name";
  string placeNameString = "locationText";

  IEnumerator Start()
  {
    StartCoroutine(LoadingCoroutine());
    string tempUrl = urlProjects;
    GI = GameObject.Find("GameData").GetComponent<GameInfo>();

    tempUrl += GI.Token;
    www = new WWW(tempUrl);
   
    yield return www;


    if (www.error == null)
    {
      textData = JsonMapper.ToObject(www.text);
      numberOfProjects = textData["projects"].Count;
      total += numberOfProjects;
      questionsArray = new string[numberOfProjects][];
      questionsIDArray = new int[numberOfProjects][];
      questionsTypeArray = new int[numberOfProjects][];
      for (int i = 0; i < numberOfProjects; i++)
      {
        projectNamesList.Add(textData["projects"][i][projectNameString].ToString());
        placeNamesList.Add(textData["projects"][i][placeNameString].ToString());
        projectIDsList.Add(int.Parse(textData["projects"][i]["id"].ToString()));

        if (textData["projects"][i]["header_image"] != null)
        {
          projectBannerList.Add(textData["projects"][i]["header_image"]["filename"].ToString());
        }
        else
        {
          projectBannerList.Add("");
        }
        string tempString = StripTagsRegex(textData["projects"][i]["description"].ToString());
        tempString = StripLinkRegex(tempString);
        projectDescriptionsList.Add(tempString);
        progress++;
      }
      GI.ProjectNameList = projectNamesList;
      GI.PlaceNameList = placeNamesList;
      GI.ProjectIds = projectIDsList;
      GI.ProjectDescriptions = projectDescriptionsList;
      GI.ProjectBannerList = projectBannerList;
    }
    else
    {
      if (textData["error"].ToString() == "token_expired")
      {
        gameObject.SendMessage("StartReceivingNewToken", "ReObtainData");
      }
    }
    StartCoroutine(GetProposals());
  }

  IEnumerator GetProposals()
  {
    total--;
    for (int i = 0; i < numberOfProjects; i++)
    {
      string urlProposals = urlProposalsPartOne + projectIDsList[i] + urlProposalsPartTwo + GI.Token;
      www = new WWW(urlProposals);
      yield return www;

      if (www.error == null)
      {
        textData = JsonMapper.ToObject(www.text);
        numberOfProposals = textData["proposals"].Count;
        total += numberOfProposals;
        string[] tempProposals = new string[numberOfProposals];
        int[] tempProposalsIds = new int[numberOfProposals];
        int[] tempProposalTypes = new int[numberOfProposals];
        if (numberOfProposals != 0)
        {
          for (int j = 0; j < numberOfProposals; j++)
          {
            tempProposals[j] = textData["proposals"][j]["description"].ToString();
            tempProposalsIds[j] = int.Parse(textData["proposals"][j]["id"].ToString());
            tempProposalTypes[j] = int.Parse(textData["proposals"][j]["type"].ToString());
            progress++;
          }
          questionsArray[i] = tempProposals;
          questionsIDArray[i] = tempProposalsIds;
          questionsTypeArray[i] = tempProposalTypes;
        }
      }
      else
      {
        Debug.Log(www.error.ToString());
        foreach (var data in textData)
        {
          Debug.Log(data);
        }

        if (textData["error"].ToString() == "token_expired")
        {
          gameObject.SendMessage("StartReceivingNewToken", "ReObtainProposals");
        }
      }
    }
    GI.Questions = questionsArray;
    GI.QuestionIds = questionsIDArray;
    GI.QuestionTypes = questionsTypeArray;
    isSynced = true;
    gameObject.SendMessage("SpawnButtons");
    StartCoroutine(GetUserData());
  }

  IEnumerator GetUserData()
  {
    string tempUrl = urlAuthenticateUser;

    tempUrl += GI.Token;
    www = new WWW(tempUrl);
    yield return www;

    if (www.error == null)
    {
      textData = JsonMapper.ToObject(www.text);
      GI.FirstNamePerson = textData["user"]["firstname"].ToString();
      GI.LastNamePerson = textData["user"]["lastname"].ToString();
      GI.Email = textData["user"]["email"].ToString();
      GI.NumberOfTimesStamped = int.Parse(textData["user"]["num_opinions"].ToString());
      gameObject.SendMessage("LoadProfileData"); //ProfileLoader
    }
    else
    {
      if (textData["error"].ToString() == "token_expired")
      {
        gameObject.SendMessage("StartReceivingNewToken", "ReObtainUserInfo");
      }
    }
  }

  IEnumerator LoadingCoroutine()
  {
    canvasWithMask.overrideSorting = false;
    loadScenePanel.SetActive(true);


    while (!isSynced)
    {
      yield return null;
    }
    loadScenePanel.SetActive(false);
    canvasWithMask.overrideSorting = true;
  }

  public void ReObtainUserInfo()
  {
    StartCoroutine(GetUserData());
  }

  public void ReObtainData()
  {
    StartCoroutine(Start());
  }

  public void ReObtainProposals()
  {
    StartCoroutine(GetProposals());
  }

  // GET RID OF < > FROM HTML TEXT
  public static string StripTagsRegex(string source)
  {
    return Regex.Replace(source, "<.*?>", string.Empty);
  }

  public static string StripLinkRegex(string source)
  {
    return Regex.Replace(source, "&nbsp;", " ");
  }
}