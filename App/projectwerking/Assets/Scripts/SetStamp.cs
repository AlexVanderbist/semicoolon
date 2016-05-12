using UnityEngine;
using System.Collections;

public class SetStamp : MonoBehaviour {

    public GameObject goodStampPrefab, badStampPrefab, gameManager;
    StampController stamps;
    PaperController paper;
    
	// Use this for initialization
	void Start () {
    stamps = gameManager.GetComponent<StampController>();
    paper = gameManager.GetComponent<PaperController>();
  }

  IEnumerator WaitSecondsForStamp(int seconds, RaycastHit hit)
  {
    yield return new WaitForSeconds(seconds);
    string selectedStamp = stamps.SelectedStamp;

    if (selectedStamp == "green")
    {
      Debug.Log(selectedStamp);
      GameObject printedStamp = (GameObject)Instantiate(goodStampPrefab, hit.point, transform.rotation);
      printedStamp.transform.SetParent(paper.getCurrentPaper.transform, true);
    }
    else if (selectedStamp == "red")
    {
      Debug.Log(selectedStamp);
      GameObject printedStamp = (GameObject)Instantiate(badStampPrefab, hit.point, transform.rotation);
      printedStamp.transform.SetParent(paper.getCurrentPaper.transform, true);
    }
  }

    public void PrintStamp(RaycastHit hit)
    {
      
      foreach (Transform child in paper.getCurrentPaper.transform)
      {
        if (child.name == (badStampPrefab.name + "(Clone)") || child.name == (goodStampPrefab.name + "(Clone)"))
        {
          Destroy(child.gameObject);
        }
      }

    StartCoroutine(WaitSecondsForStamp(1, hit));
      


  }
}
