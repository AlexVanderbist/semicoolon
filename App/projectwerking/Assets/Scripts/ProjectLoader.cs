using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProjectLoader : MonoBehaviour
{
  public GameObject levelButtonPrefab;
  public GameObject containerToDo, containerDone;
  public RectTransform containerRecToDo, containerRecDone ;
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
      //button.transform.FindChild("Banner").GetComponent<Canvas>().sortingLayerName = "Banner";
      button.transform.FindChild("Title").GetComponent<Text>().text = GI.ProjectNameList[i];
      button.transform.FindChild("MeerLezen").GetComponent<Button>().onClick.AddListener(() => ReadMore(GI.ProjectIds[tempInt]));


      if (GI.Questions[i] == null)
      {
        button.transform.FindChild("DoneSign").GetComponent<Image>().enabled = true;
        //button.transform.GetComponent<Button>().interactable = false;
        button.transform.SetParent(containerDone.transform, false);
        int numberOfChilds = 0;

        foreach (Transform trans in containerDone.transform)
        {
          numberOfChilds++;
        }
        if (numberOfChilds > 1)
        {
          containerRecDone.sizeDelta = new Vector2(containerRecToDo.rect.width, containerRecDone.rect.height + 1100);
        }
      }
      else
      {
        Debug.Log(GI.Questions[i][0]);
        button.transform.FindChild("CanvasMask").FindChild("BeginMetStempelen").GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneToLoad, tempInt));
        button.transform.SetParent(containerToDo.transform, false);
        int numberOfChilds = 0;

        foreach (Transform trans in containerToDo.transform)
        {
          numberOfChilds++;
        }
        if (numberOfChilds > 1)
        {
          containerRecToDo.sizeDelta = new Vector2(containerRecToDo.rect.width, containerRecToDo.rect.height + 1100);
        }
      }

      //button.transform.FindChild("BeginMetStempelen").GetComponent<Canvas>().sortingLayerName = "StartButton";


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

