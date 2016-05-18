using UnityEngine;
using System.Collections;

public class SetStamp : MonoBehaviour {

  public GameObject goodStampPrefab, badStampPrefab, gameManager, goodStampParticle, badStampParticle, numberStampParticle;
  public GameObject[] numberStampPrefabs;
  public GameObject numberPanel;
  public AudioClip sound;
  public float showPanelSpeed = 0.7f;

  StampController stamps;
  PaperController paper;

  private AudioSource source { get { return GetComponent<AudioSource>(); } }
  private bool numberIsSet = false;
  private bool readyToMovePanel = false;
  private bool panelIsShown = false;
  private int number;
  private float step;
  private Vector3 numberPanelHiddenPos;
  private Vector3 numberPanelShownPos;

  public int Number
  {
    get { return number; }
    set { number = value; }
  }

  // Use this for initialization
  void Start () {
    stamps = gameManager.GetComponent<StampController>();
    paper = gameManager.GetComponent<PaperController>();
    numberPanelHiddenPos = numberPanel.transform.position;
    numberPanelShownPos = new Vector3(numberPanel.transform.position.x, (Screen.height/ 854) / 2, numberPanel.transform.position.z); // 854 IS FOR ASPECT RATIO
    gameObject.AddComponent<AudioSource>();
    source.clip = sound;
    source.playOnAwake = false;
  }

  void Update()
  {
    if (readyToMovePanel)
    {
      if (!panelIsShown)
      {
        step += showPanelSpeed * Time.deltaTime;
        numberPanel.transform.position = Vector3.Lerp(numberPanel.transform.position, numberPanelShownPos, step);
        if (step >= 1)
        {
          Debug.Log("ready");
          step = 0;
          readyToMovePanel = false;
          panelIsShown = true;
        }
      }
      else
      {
        step += showPanelSpeed * Time.deltaTime;
        numberPanel.transform.position = Vector3.Lerp(numberPanel.transform.position, numberPanelHiddenPos, step);
        if (step >= 1)
        {
          Debug.Log("Move back");
          step = 0;
          readyToMovePanel = false;
          panelIsShown = false;
        }
      }
    }
  }

  IEnumerator WaitSecondsForStamp(int seconds, RaycastHit hit)
  {
    yield return new WaitForSeconds(seconds);
    string selectedStamp = stamps.SelectedStamp;
    GameObject printedStamp = null;
    PlaySound();
    if (selectedStamp == "green")
    {
        goodStampParticle.GetComponentInChildren<ParticleSystem>().Play();
        printedStamp = (GameObject)Instantiate(goodStampPrefab, hit.point, transform.rotation);
        printedStamp.transform.SetParent(paper.getCurrentPaper.transform, true);
    }
    else if (selectedStamp == "red")
    {
        badStampParticle.GetComponentInChildren<ParticleSystem>().Play();
        printedStamp = (GameObject)Instantiate(badStampPrefab, hit.point, transform.rotation);
        printedStamp.transform.SetParent(paper.getCurrentPaper.transform, true);
    }
    else if (selectedStamp == "number")
    {
      readyToMovePanel = true;
      switch (Number)
      {
        case 1:
          printedStamp = (GameObject)Instantiate(numberStampPrefabs[0], hit.point, transform.rotation);
          break;
        case 2:
          printedStamp = (GameObject)Instantiate(numberStampPrefabs[1], hit.point, transform.rotation);
          break;
        case 3:
          printedStamp = (GameObject)Instantiate(numberStampPrefabs[2], hit.point, transform.rotation);
          break;
        case 4:
          printedStamp = (GameObject)Instantiate(numberStampPrefabs[3], hit.point, transform.rotation);
          break;
        case 5:
          printedStamp = (GameObject)Instantiate(numberStampPrefabs[4], hit.point, transform.rotation);
          break;
        default:
          break;
      }
      numberStampParticle.GetComponentInChildren<ParticleSystem>().Play();
      printedStamp.transform.SetParent(paper.getCurrentPaper.transform, true);
    }
    printedStamp.transform.rotation = Quaternion.AngleAxis(10, Vector3.right);
  }

    public void PrintStamp(RaycastHit hit)
    {
      
      foreach (Transform child in paper.getCurrentPaper.transform)
      {
        if (child.name == (badStampPrefab.name + "(Clone)") || child.name == (goodStampPrefab.name + "(Clone)"))
        {
        Destroy(child.gameObject);
        }
        for (int i = 0; i < numberStampPrefabs.Length; i++)
        {
        if (child.name == numberStampPrefabs[i].name + "(Clone)")
          {
            Destroy(child.gameObject);
          }
        }
      }

    StartCoroutine(WaitSecondsForStamp(1, hit));
      
    }
    void PlaySound()
    {
        source.PlayOneShot(sound);
    }

  public void ShowPanel()
  {
    readyToMovePanel = true;
  }

  public void NumberSet(int initnumber)
  {
    Number = initnumber;
    numberIsSet = true;
    readyToMovePanel = true;
    gameObject.SendMessage("StopNumberInput");
  }
}
