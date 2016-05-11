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
  public RectTransform containerRecT;
  public RectTransform buttonRecT;
  public string url = "http://semicolon.multimediatechnology.be/api/v1/projects?token=";
  public string readMoreUrl = "http://semicolon.multimediatechnology.be/projecten/";
  public string sceneToLoad = "MainScene";

  public Sprite[] tempImageStock;

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
      url += GI.Token;
      //StartCoroutine( GetProjects());
      www = new WWW(url);
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
  }

  private void SpawnButtons()
  {
    
    for (int i = 0; i < ProjectNameList.Count; i++)
    {
      int tempInt = i+1;
      GameObject container = Instantiate(levelButtonPrefab) as GameObject;
      container.transform.FindChild("Plaatsnaam").GetComponent<Text>().text = placeNameList[i];
      container.transform.FindChild("Banner").GetComponent<Image>().sprite = tempImageStock[Random.Range(0,tempImageStock.Length)];
      container.transform.FindChild("Title").GetComponent<Text>().text = ProjectNameList[i];
      container.transform.FindChild("MeerLezen").GetComponent<Button>().onClick.AddListener(() => ReadMore(tempInt));
      container.transform.SetParent(levelButtonContainer.transform, false);
      if (i != ProjectNameList.Count - 1)
      {
        containerRecT.sizeDelta = new Vector2(containerRecT.rect.width, containerRecT.rect.height + 1000);
      }
      container.GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneToLoad, tempInt));
      //container.GetComponent<Image>().CrossFadeAlpha(0.1f, 2.0f, false);
      Debug.Log(i);
      
    }
  }

  private void ReadMore(int projectNumber) {
    Application.OpenURL(readMoreUrl + projectNumber.ToString());
    Debug.Log(projectNumber);
  }

  private void LoadLevel(string sceneName, int projectNumber) {
    GI.CurrentProjectNumber = projectNumber;
    Debug.Log(GI.CurrentProjectNumber);
    SceneManager.LoadScene(sceneName);
  }
}

