using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class DataHolder : MonoBehaviour {

  public string urlProjects = "http://semicolon.multimediatechnology.be/api/v1/projects?token=";
  public string readMoreUrl = "http://semicolon.multimediatechnology.be/projecten/";

  static int numberOfProjects = 0;
  int counter = 0;

  static WWW www;
  JsonData textData;
  GameInfo GI;

  static List<string> ProjectNameList = new List<string>();
  static List<string> placeNameList = new List<string>();

  string projectNameString = "name";
  string placeNameString = "locationText";


  IEnumerator Start()
  {
    ProjectNameList = new List<string>();
    placeNameList = new List<string>();
    GI = GetComponent<GameInfo>();
    urlProjects += GI.Token;
    //StartCoroutine( GetProjects());
    www = new WWW(urlProjects);
    yield return www;
    if (www.error == null)
    {
      textData = JsonMapper.ToObject(www.text);

      numberOfProjects = textData["projects"].Count;
      for (int i = 0; i < numberOfProjects; i++)
      {
        ProjectNameList.Add(textData["projects"][i][projectNameString].ToString());
        placeNameList.Add(textData["projects"][i][placeNameString].ToString());
        //SpawnButtons();
      }
    }
    else
    {
      if (counter < 5)
      {
        counter++;
        Debug.Log("ERROR: " + www.error);
      }
    }
  }

  // Update is called once per frame
  void Update () {
	
	}
}
