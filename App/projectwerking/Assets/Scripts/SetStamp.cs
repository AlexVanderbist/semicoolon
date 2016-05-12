using UnityEngine;
using System.Collections;

public class SetStamp : MonoBehaviour {

    public GameObject goodStampPrefab, badStampPrefab, gameManager;
    StampController stamps;
    PaperController paper;

    public AudioClip sound;
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

	// Use this for initialization
	void Start () {
    stamps = gameManager.GetComponent<StampController>();
    paper = gameManager.GetComponent<PaperController>();
    gameObject.AddComponent<AudioSource>();
    source.clip = sound;
    source.playOnAwake = false;
  }

  IEnumerator WaitSecondsForStamp(int seconds, RaycastHit hit)
  {
    
    yield return new WaitForSeconds(seconds);
    string selectedStamp = stamps.SelectedStamp;

    PlaySound();
    if (selectedStamp == "green")
    {
        gameObject.GetComponentInChildren<ParticleSystem>().enableEmission = true;
        Debug.Log(selectedStamp);
        GameObject printedStamp = (GameObject)Instantiate(goodStampPrefab, hit.point, transform.rotation);
        printedStamp.transform.SetParent(paper.getCurrentPaper.transform, true);
    }
    else if (selectedStamp == "red")
    {
        gameObject.GetComponentInChildren<ParticleSystem>().enableEmission = true;
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
    void PlaySound()
    {
        source.PlayOneShot(sound);
    }
}
