using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInfo : MonoBehaviour {

  private static string token;
  private static int currentProjectNumber;

  static List<int> projectIds = new List<int>();
  static int[][] questionIds;
  static List<string> projectNameList = new List<string>();
  static List<string> placeNameList = new List<string>();
  static string[][] questionArray;
  static int[][] questionTypes;

  private List<string> projectDescriptions;

  public List<string> ProjectDescriptions
  {
    get { return projectDescriptions; }
    set { projectDescriptions = value; }
  }


  public int[][] QuestionTypes
  {
    get { return questionTypes; }
    set { questionTypes = value; }
  }

  public int[][] QuestionIds
  {
    get { return questionIds; }
    set { questionIds = value; }
  }

  public List<int> ProjectIds
  {
    get { return projectIds; }
    set { projectIds = value; }
  }

  public int CurrentProjectNumber
  {
    get { return currentProjectNumber; }
    set { currentProjectNumber = value; }
  }

  public string Token
  {
    get { return token; }
    set { token = value; }
  }

  public string[][] Questions
  {
    get { return questionArray; }
    set { questionArray = value; }
  }

  public List<string> ProjectNameList
  {
    get { return projectNameList; }
    set { projectNameList = value; }
  }

  public List<string> PlaceNameList
  {
    get { return placeNameList; }
    set { placeNameList = value; }
  }
}
