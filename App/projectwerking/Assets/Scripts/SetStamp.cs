using UnityEngine;
using System.Collections;

public class SetStamp : MonoBehaviour {

    public GameObject GoodStampPrefab, BadStampPrefab, GameManager;
    StampController stamps;

    string selectedStamp;
    
	// Use this for initialization
	void Start () {
        stamps = GameManager.GetComponent<StampController>();
        
	
	}
	
	// Update is called once per frame
	void Update () {
        selectedStamp = stamps.selectedStamp;
	
	}

    public void PrintStamp()
    {
        if (selectedStamp == "green")
        {
            Debug.Log(selectedStamp);
            GameObject printedStamp = (GameObject)Instantiate(GoodStampPrefab, transform.position, transform.rotation);
            //printedStamp.transform.parent = stamps.hit.collider.transform;
        }
        else if (selectedStamp == "red")
        {
            Debug.Log(selectedStamp);
            GameObject printedStamp = (GameObject)Instantiate(BadStampPrefab, transform.position, transform.rotation);
            //printedStamp.transform.parent = hit.collider.transform;
        }

    }
}
