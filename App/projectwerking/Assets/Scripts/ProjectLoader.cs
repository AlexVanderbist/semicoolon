using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProjectLoader : MonoBehaviour
{
  public GameObject levelButtonPrefab;
  public GameObject containerToDo, containerDone;
  public RectTransform containerRecToDo, containerRectDone ;
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
      int tempInt = i;
      GameObject button = Instantiate(levelButtonPrefab) as GameObject;
      button.transform.FindChild("DoneSign").GetComponent<Image>().enabled = false;
      button.transform.FindChild("Plaatsnaam").GetComponent<Text>().text = GI.PlaceNameList[i];
      button.transform.FindChild("Uitleg").GetComponent<Text>().text = GI.ProjectDescriptions[i];
      button.transform.FindChild("Banner").GetComponent<Image>().sprite = tempImageStock[Random.Range(0, tempImageStock.Length)];
      button.transform.FindChild("Title").GetComponent<Text>().text = GI.ProjectNameList[i];
      button.transform.FindChild("MeerLezen").GetComponent<Button>().onClick.AddListener(() => ReadMore(GI.ProjectIds[tempInt]));


      if (GI.Questions[i] == null)
      {
        button.transform.FindChild("DoneSign").GetComponent<Image>().enabled = true;
        button.transform.GetComponent<Button>().interactable = false;
        button.transform.SetParent(containerDone.transform, false);
      }
      else
      {
        button.transform.SetParent(containerToDo.transform, false);
        button.GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneToLoad, tempInt));
      }

     
      if (i != GI.PlaceNameList.Count - 1)
      {
        containerRecToDo.sizeDelta = new Vector2(containerRecToDo.rect.width, containerRecToDo.rect.height + 1100);
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

