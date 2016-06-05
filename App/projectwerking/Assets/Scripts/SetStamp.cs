using UnityEngine;
using System.Collections;

public class SetStamp : MonoBehaviour {

  // GIVES A SPRITE STAMP TO THE PAPER
  // INPUT OF STAMPS IS FOUND IN GAME CONTROLLER SCRIPT
  // ALSO, SHOWS NUMBER PANEL

  public GameObject gameManager, goodStampParticle, badStampParticle, numberStampParticle;
  public GameObject[] numberStampPrefabs, goodStampPrefabs, badStampPrefabs;
  public GameObject checkmarkPrefab;
  public AudioClip sound;
  public float showPanelSpeed = 0.7f, randomStampPrint;
  public Transform hashtagPos;

  StampController stamps;
  PaperController paper;

  private AudioSource source { get { return GetComponent<AudioSource>(); } }

  // LOAD VARIABLES NEEDED
  void Start () {
    stamps = gameManager.GetComponent<StampController>();
    paper = gameManager.GetComponent<PaperController>();
    gameObject.AddComponent<AudioSource>();
    source.clip = sound;
    source.playOnAwake = false;
  }

  // A DELAY OF ONE SECOND BECAUSE THE STAMP SPRITE IS OTHERWISE TOO FAST
  IEnumerator WaitSecondsForStamp(int seconds, RaycastHit hit)
  {
    yield return new WaitForSeconds(seconds);
    string selectedStamp = stamps.SelectedStamp;
    GameObject printedStamp = null;
    GameObject checkmarkStamp = null;
    
    randomStampPrint = Random.Range(0, 3);

    //CHECK FOR THE SELECTED STAMP
    if (selectedStamp == "green")
    {
        printedStamp = (GameObject)Instantiate(goodStampPrefabs[(int)randomStampPrint], hit.point, transform.rotation);
    }
    else if (selectedStamp == "red")
    {
        printedStamp = (GameObject)Instantiate(badStampPrefabs[(int)randomStampPrint], hit.point, transform.rotation);
    }
    else if (selectedStamp == "number")
    {
      switch (hit.collider.gameObject.name)
      {
        case "1":
          printedStamp = (GameObject)Instantiate(numberStampPrefabs[0], hashtagPos.position, transform.rotation);
          break;
        case "2":
          printedStamp = (GameObject)Instantiate(numberStampPrefabs[1], hashtagPos.position, transform.rotation);
          break;
        case "3":
          printedStamp = (GameObject)Instantiate(numberStampPrefabs[2], hashtagPos.position, transform.rotation);
          break;
        case "4":
          printedStamp = (GameObject)Instantiate(numberStampPrefabs[3], hashtagPos.position, transform.rotation);
          break;
        case "5":
          printedStamp = (GameObject)Instantiate(numberStampPrefabs[4], hashtagPos.position, transform.rotation);
          break;
        default:
          break;
      }
      checkmarkStamp = (GameObject)Instantiate(checkmarkPrefab, hit.point, transform.rotation);
      checkmarkStamp.transform.SetParent(paper.GetCurrentNumberPaper.transform, true);
      checkmarkStamp.transform.rotation = Quaternion.AngleAxis(10, Vector3.right);   
    }
    printedStamp.transform.SetParent(paper.GetCurrentPaper.transform, true);
    printedStamp.transform.rotation = Quaternion.AngleAxis(10, Vector3.right);
  }

  //RECEIVES A MESSAGE FROM GAME CONTROLLER, CHECKS IF THERE IS A SPRITE CHILD AND DELETES IT
  public void PrintStamp(RaycastHit hit)
    {
      foreach (Transform child in paper.GetCurrentPaper.transform)
      {
        if (child.name == (badStampPrefabs[(int)randomStampPrint].name + "(Clone)") || child.name == (goodStampPrefabs[(int)randomStampPrint].name + "(Clone)"))
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
    StartCoroutine(DelaySound(0.5f));
    StartCoroutine(WaitSecondsForStamp(1, hit));
  }

  IEnumerator DelaySound(float seconds)
  {
    yield return new WaitForSeconds(seconds);
    PlaySound();
  }

  void PlaySound()
  {
    source.PlayOneShot(sound);
  }
}
