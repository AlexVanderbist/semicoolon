using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ProjectSelecter : MonoBehaviour
{
  public GameObject levelButtonPrefab;
  public GameObject levelButtonContainer;
  public RectTransform containerRecT;
  public string readMoreUrl = "http://semicolon.multimediatechnology.be/projecten/";
  public string sceneToLoad = "MainScene";

  public Sprite[] tempImageStock;

  GameInfo GI;

  void Start()
  {
    GI = GameObject.Find("GameData").GetComponent<GameInfo>();
  }

  private void SpawnButtons()
  {
    
    for (int i = 0; i < GI.PlaceNameList.Count; i++)
    {
      int tempInt = i+1;
      GameObject container = Instantiate(levelButtonPrefab) as GameObject;
      container.transform.FindChild("DoneSign").GetComponent<Image>().enabled = false;
      container.transform.FindChild("Plaatsnaam").GetComponent<Text>().text = GI.PlaceNameList[i];
      container.transform.FindChild("Banner").GetComponent<Image>().sprite = tempImageStock[Random.Range(0, tempImageStock.Length)];
      container.transform.FindChild("Title").GetComponent<Text>().text = GI.ProjectNameList[i];
      container.transform.FindChild("MeerLezen").GetComponent<Button>().onClick.AddListener(() => ReadMore(GI.ProjectIds[tempInt]));
      container.transform.SetParent(levelButtonContainer.transform, false);

      if (GI.Questions[i] == null)
      {
        container.transform.FindChild("DoneSign").GetComponent<Image>().enabled = true;
        container.transform.GetComponent<Button>().interactable = false;
      }
      else
      {
        container.GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneToLoad, tempInt));
      }

     
      if (i != GI.PlaceNameList.Count - 1)
      {
        containerRecT.sizeDelta = new Vector2(containerRecT.rect.width, containerRecT.rect.height + 1000);
      }

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

