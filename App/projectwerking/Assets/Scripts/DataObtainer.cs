using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LitJson;

public class DataObtainer : MonoBehaviour {

  // APART FROM "LOGIN" AND "TOKENRECEIVER", 
  // THIS CLASS OBTAINS ALL DATA FROM API AND GIVES IT TO GAMEINFO

  public string urlProjects = "http://semicolon.multimediatechnology.be/api/v1/projects?token=";
  public string urlProposalsPartOne = "http://semicolon.multimediatechnology.be/api/v1/projects/";
  public string urlProposalsPartTwo = "/proposals/user?token=";
  public string urlAuthenticateUser = "http://semicolon.multimediatechnology.be/api/v1/authenticate/user?token=";

  static int numberOfProjects = 0;
  static int numberOfProposals = 0;

  static WWW www;
  JsonData textData;
  GameInfo GI;

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
    string tempUrl = urlProjects;
    GI = GameObject.Find("GameData").GetComponent<GameInfo>();

    tempUrl += GI.Token;
    www = new WWW(tempUrl);
    yield return www;

    textData = JsonMapper.ToObject(www.text);
    if (www.error == null)
    {
      numberOfProjects = textData["projects"].Count;
      questionsArray = new string[numberOfProjects][];
      questionsIDArray = new int[numberOfProjects][];
      questionsTypeArray = new int[numberOfProjects][];
      for (int i = 0; i < numberOfProjects; i++)
      {
        projectNamesList.Add(textData["projects"][i][projectNameString].ToString());
        placeNamesList.Add(textData["projects"][i][placeNameString].ToString());
        projectIDsList.Add(int.Parse(textData["projects"][i]["id"].ToString()));

        string tempString = StripTagsRegex(textData["projects"][i]["description"].ToString());
        projectDescriptionsList.Add(tempString);
      }
      GI.ProjectNameList = projectNamesList;
      GI.PlaceNameList = placeNamesList;
      GI.ProjectIds = projectIDsList;
      GI.ProjectDescriptions = projectDescriptionsList;
      StartCoroutine(GetProposals());
    }
    else
    {
      if (textData["error"].ToString() == "token_expired")
      {
        gameObject.SendMessage("StartReceivingNewToken", "ReObtainData");
      }
    }
  }

  IEnumerator GetProposals()
  { 
    for (int i = 0; i < numberOfProjects; i++)
    {
      string urlProposals = urlProposalsPartOne + projectIDsList[i] + urlProposalsPartTwo + GI.Token;
      www = new WWW(urlProposals);
      yield return www;

      textData = JsonMapper.ToObject(www.text);
      if (www.error == null)
      {
        numberOfProposals = textData["proposals"].Count;
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
          }
          questionsArray[i] = tempProposals;
          questionsIDArray[i] = tempProposalsIds;
          questionsTypeArray[i] = tempProposalTypes;
        }
      }
      else
      {
        if (textData["error"].ToString() == "token_expired")
        {
          gameObject.SendMessage("StartReceivingNewToken", "ReObtainProposals");
        }
      }
    }
    GI.Questions = questionsArray;
    GI.QuestionIds = questionsIDArray;
    GI.QuestionTypes = questionsTypeArray;
    gameObject.SendMessage("SpawnButtons");
    StartCoroutine(GetUserData());
  }

  IEnumerator GetUserData()
  {
    string tempUrl = urlAuthenticateUser;

    tempUrl += GI.Token;
    www = new WWW(tempUrl);
    yield return www;

    textData = JsonMapper.ToObject(www.text);
    if (www.error == null)
    {
      GI.FirstNamePerson = textData["user"]["firstname"].ToString();
      GI.LastNamePerson = textData["user"]["lastname"].ToString();
      GI.Email = textData["user"]["email"].ToString();
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
}
