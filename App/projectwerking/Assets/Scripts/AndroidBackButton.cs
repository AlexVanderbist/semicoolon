using UnityEngine;
using System.Collections;
using System.IO;

public class AndroidBackButton : MonoBehaviour {

    private bool isPause = false;
	
	// Update is called once per frame
	void Update () {

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("BackButton clicked");
                isPause = !isPause;
                if (isPause)
                {
                    Time.timeScale = 0;
                    Debug.Log("looking for pause panel");
                    GameObject.Find("PausePanel").SetActive(true);
                }
                else
                {
                    Time.timeScale = 1;
                }

            }
        }
        
	
	}
}
