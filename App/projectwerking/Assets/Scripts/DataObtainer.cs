﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class DataObtainer : MonoBehaviour {

  public string urlProjects = "http://semicolon.multimediatechnology.be/api/v1/projecten?token=";
  public string urlProposalsPartOne = "http://semicolon.multimediatechnology.be/api/v1/projecten/";
  public string urlProposalsPartTwo = "/proposals?token=";

  static int numberOfProjects = 0;
  static int numberOfProposals = 0;

  static WWW www;
  JsonData textData;
  GameInfo GI;

  List<string> projectNameList = new List<string>();
  List<string> placeNameList = new List<string>();
  string[][] questionArray;
  List<int> projectIds = new List<int>();

  string projectNameString = "name";
  string placeNameString = "locationText";

  IEnumerator Start()
  {
    projectNameList = new List<string>();
    placeNameList = new List<string>();
    GI = GetComponent<GameInfo>();
    urlProjects += GI.Token;
    www = new WWW(urlProjects);
    yield return www;
    if (www.error == null)
    {
      textData = JsonMapper.ToObject(www.text);

      numberOfProjects = textData["projects"].Count;
      questionArray = new string[numberOfProjects][];
      for (int i = 0; i < numberOfProjects; i++)
      {
        projectNameList.Add(textData["projects"][i][projectNameString].ToString());
        placeNameList.Add(textData["projects"][i][placeNameString].ToString());
        projectIds.Add(int.Parse(textData["projects"][i]["id"].ToString()));
      }
      GI.ProjectNameList = projectNameList;
      GI.PlaceNameList = placeNameList;
      StartCoroutine(GetProposals());
    }
    else
    {
        Debug.Log("ERROR: " + www.error);
    }
  }

  IEnumerator GetProposals()
  {
    GI = GetComponent<GameInfo>();

    for (int i = 0; i < numberOfProjects; i++)
    {
      string urlProposals = urlProposalsPartOne + projectIds[i] + urlProposalsPartTwo + GI.Token;
      www = new WWW(urlProposals);
      yield return www;
      if (www.error == null)
      {
        textData = JsonMapper.ToObject(www.text);

        numberOfProposals = textData["proposals"].Count;
        string[] tempProposals = new string[numberOfProposals];
        if (numberOfProposals != 0)
        {
          for (int j = 0; j < numberOfProposals; j++)
          {
            tempProposals[j] = textData["proposals"][j]["description"].ToString();
          }
          questionArray[i] = tempProposals;
          GI.Questions = questionArray;
        }
      }
      else
      {
          Debug.Log("ERROR: " + www.error);
      }
    }
    gameObject.SendMessage("SpawnButtons");
  }
}
