using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GoBackToProjects()
    {
        SceneManager.LoadScene("ProjectsScene");
    }

    public void KeepStamping()
    {
        GameObject.Find("PausePanel").SetActive(false);
    }
}
