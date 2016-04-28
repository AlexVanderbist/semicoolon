using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameInfo))]
public class ProjectSelecter : MonoBehaviour
{
  public GameObject levelButtonPrefab;
  public GameObject levelButtonContainer;
  public string url = "http://semicolon.multimediatechnology.be/api/v1/projects?token=";

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
    GI = GetComponent<GameInfo>();
    url += GI.Token;
    //StartCoroutine( GetProjects());
    www = new WWW(url);
    yield return www;
    if (www.error == null)
    {
      textData = JsonMapper.ToObject(www.text);

      numberOfProjects = textData.Count;
      for (int i = 0; i <= numberOfProjects; i++)
      {
        ProjectNameList.Add(textData["projects"][i][projectNameString].ToString());
        placeNameList.Add(textData["projects"][i][placeNameString].ToString());
      }
      SpawnButtons();
    }
    else
    {
      if (counter < 5)
      {
        counter++;
        Debug.Log("ERROR: " + www.error);
      }
    }
    //SpawnButtons();
  }

  private void SpawnButtons()
  {
    for (int i = 0; i < ProjectNameList.Count; i++)
    {
      GameObject container = Instantiate(levelButtonPrefab) as GameObject;
      container.GetComponentInChildren<Text>().text = ProjectNameList[i] + "\n" + placeNameList[i];
      container.transform.SetParent(levelButtonContainer.transform, false);
      container.GetComponent<Button>().onClick.AddListener(() => LoadLevel("MainScene", i));
    }
  }

  private void LoadLevel(string sceneName, int projectNumber) {
    GI.CurrentProjectNumber = projectNumber;
    Debug.Log(GI.CurrentProjectNumber);
    SceneManager.LoadScene(sceneName);
  }
}

