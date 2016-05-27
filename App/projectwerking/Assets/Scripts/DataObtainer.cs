using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LitJson;

public class DataObtainer : MonoBehaviour {

  public string urlProjects = "http://semicolon.multimediatechnology.be/api/v1/projects?token=";
  public string urlProposalsPartOne = "http://semicolon.multimediatechnology.be/api/v1/projects/";
  public string urlProposalsPartTwo = "/proposals/user?token=";

  static int numberOfProjects = 0;
  static int numberOfProposals = 0;

  static WWW www;
  JsonData textData;
  GameInfo GI;

  List<string> projectNameList = new List<string>();
  List<string> placeNameList = new List<string>();
  List<string> projectDescriptions = new List<string>();
  List<int> projectIds = new List<int>();

  string[][] questionArray = null;
  int[][] questionIdArray = null;
  int[][] questionTypeArray = null;

  string projectNameString = "name";
  string placeNameString = "locationText";

  IEnumerator Start()
  {
    string tempUrl = urlProjects;
    GI = GameObject.Find("GameData").GetComponent<GameInfo>();

    tempUrl += GI.Token;
    Debug.Log(tempUrl);
    www = new WWW(tempUrl);
    yield return www;

    textData = JsonMapper.ToObject(www.text);
    if (www.error == null)
    {
      numberOfProjects = textData["projects"].Count;
      questionArray = new string[numberOfProjects][];
      questionIdArray = new int[numberOfProjects][];
      questionTypeArray = new int[numberOfProjects][];
      for (int i = 0; i < numberOfProjects; i++)
      {
        projectNameList.Add(textData["projects"][i][projectNameString].ToString());
        placeNameList.Add(textData["projects"][i][placeNameString].ToString());
        projectIds.Add(int.Parse(textData["projects"][i]["id"].ToString()));

        string tempString = StripTagsRegex(textData["projects"][i]["description"].ToString());
        projectDescriptions.Add(tempString);
      }
      GI.ProjectNameList = projectNameList;
      GI.PlaceNameList = placeNameList;
      GI.ProjectIds = projectIds;
      GI.ProjectDescriptions = projectDescriptions;
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
      string urlProposals = urlProposalsPartOne + projectIds[i] + urlProposalsPartTwo + GI.Token;
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
          questionArray[i] = tempProposals;
          questionIdArray[i] = tempProposalsIds;
          questionTypeArray[i] = tempProposalTypes;
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
    GI.Questions = questionArray;
    GI.QuestionIds = questionIdArray;
    GI.QuestionTypes = questionTypeArray;
    gameObject.SendMessage("SpawnButtons");
  }

  public void ReObtainData()
  {
    StartCoroutine(Start());
  }

  public void ReObtainProposals()
  {
    StartCoroutine(GetProposals());
  }

  public static string StripTagsRegex(string source)
  {
    return Regex.Replace(source, "<.*?>", string.Empty);
  }
}
