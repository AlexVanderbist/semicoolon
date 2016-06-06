using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour {

    public Text loadingText;
    float counter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        counter += Time.deltaTime;

        switch ((int)counter)
        {
            case 0:
                loadingText.text = "Laden";
                break;
            case 1:
                loadingText.text = "Laden.";
                break;
            case 2:
                loadingText.text = "Laden..";
                break;
            case 3:
                loadingText.text = "Loading...";
                break;
            case 4:
                counter = 0;
                break;
            default:
                break;
        }
	
	}
}
