using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProjectLoader : MonoBehaviour
{
  public GameObject projectButtonPrefab, projectPaperDonePrefab, projectPaperToDoPrefab;
  public GameObject containerToDo, containerDone;
  public RectTransform containerRecToDo, containerRecDone , projectButtonRec;
  public string readMoreUrl = "http://semicolon.multimediatechnology.be/projecten/";
  public string sceneToLoad = "MainScene";
  public float marge = 250;

  public Sprite[] tempStockImages;

  GameInfo GI;

  void Start()
  {
    marge += 1000;
    GI = GameObject.Find("GameData").GetComponent<GameInfo>();
  }

  private void SpawnButtons()
  {
    
    for (int i = 0; i < GI.PlaceNameList.Count; i++)
    {
      int tempInt = i;
      GameObject button = Instantiate(projectButtonPrefab) as GameObject;
      button.transform.FindChild("DoneSign").GetComponent<Image>().enabled = false;
      button.transform.FindChild("Plaatsnaam").GetComponent<Text>().text = GI.PlaceNameList[i];
      button.transform.FindChild("Uitleg").GetComponent<Text>().text = GI.ProjectDescriptions[i];
      button.transform.FindChild("Banner").GetComponent<Image>().sprite = tempStockImages[Random.Range(0, tempStockImages.Length)];
      button.transform.FindChild("Title").GetComponent<Text>().text = GI.ProjectNameList[i];
      button.transform.FindChild("MeerLezen").GetComponent<Button>().onClick.AddListener(() => ReadMoreUrls(GI.ProjectIds[tempInt]));


      if (GI.Questions[i] == null)
      {
        button.transform.FindChild("DoneSign").GetComponent<Image>().enabled = true;
        button.transform.FindChild("BeginMetStempelen").GetComponent<Button>().interactable = false;
        button.transform.SetParent(containerDone.transform, false);
        int numberOfChilds = 0;

        foreach (Transform trans in containerDone.transform)
        {
          numberOfChilds++;
        }
        if (numberOfChilds > 1)
        {
          containerRecDone.sizeDelta = new Vector2(containerRecToDo.rect.width, containerRecDone.rect.height + marge);
        }
      }
      else
      {
        button.transform.FindChild("BeginMetStempelen").GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneToLoad, tempInt));
        button.transform.SetParent(containerToDo.transform, false);
        int numberOfChilds = 0;

        foreach (Transform trans in containerToDo.transform)
        {
          numberOfChilds++;
        }
        if (numberOfChilds > 1)
        {
          containerRecToDo.sizeDelta = new Vector2(containerRecToDo.rect.width, containerRecToDo.rect.height + marge);
        }
      }
    }

    if (containerToDo.transform.childCount == 0)
    {
      GameObject button = Instantiate(projectPaperDonePrefab) as GameObject;
      button.transform.SetParent(containerToDo.transform, false);
    }
    if (containerDone.transform.childCount == 0)
    {
      GameObject button = Instantiate(projectPaperToDoPrefab) as GameObject;
      button.transform.SetParent(containerDone.transform, false);
    }
  }

  private void ReadMoreUrls(int projectNumber) {
    Application.OpenURL(readMoreUrl + projectNumber.ToString());
  }

  private void LoadLevel(string sceneName, int projectNumber) {
    GI.CurrentProjectNumber = projectNumber;
    SceneManager.LoadScene(sceneName);
  }
}

