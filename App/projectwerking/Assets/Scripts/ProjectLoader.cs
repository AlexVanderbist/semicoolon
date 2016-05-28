using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ProjectLoader : MonoBehaviour
{

  //LOADS ALL PROJECT PANELS BASED ON GAMEINFO DATA
  public GameObject projectPanelPrefab, projectPaperDonePrefab, projectPaperToDoPrefab;
  public GameObject containerToDo, containerDone;
  public RectTransform containerRecToDo, containerRecDone , projectButtonRec;
  public string readMoreUrl = "http://semicolon.multimediatechnology.be/projecten/";
  public string sceneToLoad = "MainScene";
  public float marge = 250;
  
  //TEST IMAGES
  public Sprite[] tempStockImages;

  GameInfo GI;

  void Start()
  {
    marge += 1000; // THIS IS THE LENGTH OF A PROJECT PANEL
    GI = GameObject.Find("GameData").GetComponent<GameInfo>();
  }

  //STARTS WHEN DATA OBTAINER IS READY (SENDMESSAGE)
  //ADDS FOR EACH PANEL THE RIGHT VALUES
  //DEPENDING ON TYPE IT GETS ADD TO "TO DO" OR "DONE" CONTAINER
  private void SpawnButtons()
  {
    for (int i = 0; i < GI.PlaceNameList.Count; i++)
    {
      int tempInt = i;
      GameObject panel = Instantiate(projectPanelPrefab) as GameObject;
      panel.transform.FindChild("DoneSign").GetComponent<Image>().enabled = false;
      panel.transform.FindChild("Plaatsnaam").GetComponent<Text>().text = GI.PlaceNameList[i];
      panel.transform.FindChild("Uitleg").GetComponent<Text>().text = GI.ProjectDescriptions[i];
      panel.transform.FindChild("Banner").GetComponent<Image>().sprite = tempStockImages[Random.Range(0, tempStockImages.Length)];
      panel.transform.FindChild("Title").GetComponent<Text>().text = GI.ProjectNameList[i];
      panel.transform.FindChild("MeerLezen").GetComponent<Button>().onClick.AddListener(() => ReadMoreUrls(GI.ProjectIds[tempInt]));

      // IF THERE ARE NO MORE QUESTIONS LEFT ADD TO "DONE" CONTAINER
      if (GI.Questions[i] == null)
      {
        panel.transform.FindChild("DoneSign").GetComponent<Image>().enabled = true;
        panel.transform.FindChild("BeginMetStempelen").GetComponent<Button>().interactable = false;
        panel.transform.SetParent(containerDone.transform, false);
        int numberOfChilds = 0;

        // MAKE THE CONTAINER LONGER TO BE ABLE TO SCROLL
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
        panel.transform.FindChild("BeginMetStempelen").GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneToLoad, tempInt));
        panel.transform.SetParent(containerToDo.transform, false);
        int numberOfChilds = 0;

        // MAKE THE CONTAINER LONGER TO BE ABLE TO SCROLL
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

    // ADDS A PANEL WITH INFORMATION BECAUSE THERE ARE NO PANELS TO SHOW FOR THE SPECIFIC CONTAINER
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

  //ON CLICK THE GIVEN URL NEEDS TO BE LOADED
  private void ReadMoreUrls(int projectNumber) {
    Application.OpenURL(readMoreUrl + projectNumber.ToString());
  }

  // LOADS MAINSCENE AND UPDATES GI PROJECTNUMBER SO THE RIGHT QUESTIONS WILL BE ASKED
  private void LoadLevel(string sceneName, int projectNumber) {
    GI.CurrentProjectNumber = projectNumber;
    SceneManager.LoadScene(sceneName);
  }
}

