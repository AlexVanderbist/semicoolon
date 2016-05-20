using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
  List<string> projectNameList = null;
  List<string> placeNameList = null;
  string[][] questionArray = null;
  int[][] questionIdArray = null;
  int[][] questionTypeArray = null;
  List<int> projectIds = null;

  string projectNameString = "name";
  string placeNameString = "locationText";

  IEnumerator Start()
  {
    projectIds = new List<int>();
    projectNameList = new List<string>();
    placeNameList = new List<string>();

    GI = GameObject.Find("GameData").GetComponent<GameInfo>();

    urlProjects += GI.Token;
    Debug.Log(urlProjects);
    www = new WWW(urlProjects);
    yield return www;
    if (www.error == null)
    {
      textData = JsonMapper.ToObject(www.text);

      numberOfProjects = textData["projects"].Count;
      questionArray = new string[numberOfProjects][];
      questionIdArray = new int[numberOfProjects][];
      questionTypeArray = new int[numberOfProjects][];
      for (int i = 0; i < numberOfProjects; i++)
      {
        projectNameList.Add(textData["projects"][i][projectNameString].ToString());
        placeNameList.Add(textData["projects"][i][placeNameString].ToString());
        projectIds.Add(int.Parse(textData["projects"][i]["id"].ToString()));
      }
      GI.ProjectNameList = projectNameList;
      GI.PlaceNameList = placeNameList;
      GI.ProjectIds = projectIds;
      StartCoroutine(GetProposals());
    }
    else
    {
        Debug.Log("ERROR: " + www.error);
    }
  }

  IEnumerator GetProposals()
  {
    for (int i = 0; i < numberOfProjects; i++)
    {
      string urlProposals = urlProposalsPartOne + projectIds[i] + urlProposalsPartTwo + GI.Token;
      Debug.Log(urlProposals);
      www = new WWW(urlProposals);
      yield return www;
      if (www.error == null)
      {
        textData = JsonMapper.ToObject(www.text);

        numberOfProposals = textData["proposals"].Count;
        Debug.Log("Nummer van proposals voor project " + projectIds[i] + "is" + numberOfProposals);
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
          Debug.Log("ERROR: " + www.error);
      }
    }
    GI.Questions = questionArray;
    GI.QuestionIds = questionIdArray;
    GI.QuestionTypes = questionTypeArray;
    gameObject.SendMessage("SpawnButtons");
  }
}
