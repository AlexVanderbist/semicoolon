using UnityEngine;
using System.Collections;
//using SimpleJSON;
using LitJson;
using System;
using UnityEngine.UI;
using UnityEngine.Experimental.Networking;

public class PaperController : MonoBehaviour {

  public RectTransform paper;
  public Text paperText;
  public Transform startposition, focusposition, endposition;
  public float speed = 0.5f;

  RectTransform currentPaper;
  int currentQuestionNr = 0;
  string[] paperTextArray;
  string url = "http://jsonplaceholder.typicode.com/posts";

  string allText;
  static WWW www;
  WWWForm wwwAnswer;
  JsonData textData;


  IEnumerator Start()
  {
    
    www = new WWW(url);
    yield return www;
    if (www.error == null)
    {
      //allText = JSON.Parse(www.text);
      textData = JsonMapper.ToObject(www.text);
      Debug.Log(textData[1]["title"]);
    }
    else
    {
      Debug.Log("ERROR: " + www.error);
    }
  }
	
	// Update is called once per frame
	void Update () {
    //instantiatePaper();
  }

  void instantiatePaper() {
    Instantiate(paper, focusposition.position, transform.rotation);
  }

  void setNextPaperText() {
    currentQuestionNr++;
    paperText.text = paperTextArray[currentQuestionNr];
  }

  void setPreviousPaperText() {
    currentQuestionNr--;
    paperText.text = paperTextArray[currentQuestionNr];
  }

  public void SwipeNext() {
    currentPaper.transform.position = Vector3.Lerp(startposition.position, focusposition.position, speed);
  }

  public void SwipePrevious() {
    currentPaper.transform.position = Vector3.Lerp(focusposition.position, endposition.position, speed);
  }
}
