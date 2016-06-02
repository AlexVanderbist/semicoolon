using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInfo : MonoBehaviour {

  // THIS CLASS HOLDS ALL DATA THAT RETURNS FROM API
  // THIS DATA IS USED BY MANY CLASSES, ALWAYS REFERRED AS "GI"
  // ABLE TO ACCES THIS DATA BY : "GameInfo GI; GI = GameObject.Find("GameData").GetComponent<GameInfo>();"
  // GAMEDATA IS KNOWN IN ALL SCENES (DONT DESTROY ON LOAD)

  private static string token;
  private static int currentProjectNumber;
  private static string firstNamePerson;
  private static string lastNamePerson;
  private static string email;
  private static string password;
  private static string numberOfStampsDone;
  private static int currentQuestionNumber;

  private static List<int> projectIds = new List<int>();
  private static List<string> projectNameList = new List<string>();
  private static List<string> placeNameList = new List<string>();
  private static List<string> projectDescriptions = new List<string>();
  private static string[][] questionArray;
  private static int[][] questionTypes;
  private static int[][] questionIds;

  public string Password
  {
    get { return password; }
    set { password = value; }
  }

  public string Email
  {
    get { return email; }
    set { email = value; }
  }

  public string FirstNamePerson
  {
    get { return firstNamePerson; }
    set { firstNamePerson = value; }
  }

  public string LastNamePerson
  {
    get { return lastNamePerson; }
    set { lastNamePerson = value; }
  }

  public int CurrentQuestionNumber
  {
    get { return currentQuestionNumber; }
    set { currentQuestionNumber = value; }
  }

  public List<string> ProjectDescriptions
  {
    get { return projectDescriptions; }
    set { projectDescriptions = value; }
  }

  public int NumberOfProjects
  {
    get { return projectNameList.Count - 1; }
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
