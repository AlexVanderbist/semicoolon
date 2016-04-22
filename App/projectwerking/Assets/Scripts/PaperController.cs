using UnityEngine;
using System.Collections;
//using SimpleJSON;
using LitJson;
using System;
using UnityEngine.UI;
using UnityEngine.Experimental.Networking;
using UnityEngine.SceneManagement;

public class PaperController : MonoBehaviour
{

  public RectTransform paperPrefab;
  public Text paperTextbox;
  public Text titleTextbox;
  public Transform startposition, focusposition, endposition;
  public float speed = 0.5f;

  RectTransform currentPaper;
  int currentQuestionNr = 0;
  string url = "http://semicolon.multimediatechnology.be/projecten";
  string testurl = "http://jsonplaceholder.typicode.com/posts";

  string titleText = "Vraag: ";
  string questionValue = "body";

  static WWW www;
  WWWForm wwwAnswer;
  JsonData textData;


  IEnumerator setText()
  {
    currentQuestionNr++;
    www = new WWW(testurl);
    yield return www;
    if (www.error == null)
    {
      textData = JsonMapper.ToObject(www.text);
      paperTextbox.text = textData[currentQuestionNr][questionValue].ToString();
      titleTextbox.text = titleText + currentQuestionNr.ToString();
    }
    else
    {
      Debug.Log("ERROR: " + www.error);
    }
    

  }

  // Update is called once per frame
  void Update()
  {
    //instantiatePaper();
  }

  public void SwipeNext()
  {
    RectTransform newPaper;
    if (currentPaper = null) {
      newPaper = (RectTransform)Instantiate(paperPrefab, startposition.position, transform.rotation);
      setText();
      newPaper.transform.position = Vector3.Lerp(startposition.position, focusposition.position, speed);
      currentPaper = newPaper;
    }
    else
    {
      /*
      newPaper = (RectTransform)Instantiate(paperPrefab, startposition.position, transform.rotation);
      currentPaper.transform.position = Vector3.Lerp(focusposition.position, endposition.position, speed);

      setText();
      newPaper.transform.position = Vector3.Lerp(startposition.position, focusposition.position, speed);
      Destroy(currentPaper);
      currentPaper = newPaper;
      */
    }


  }

  /*
  public void SwipePrevious() {
    currentQuestionNr--;
    currentPaper.transform.position = Vector3.Lerp(focusposition.position, endposition.position, speed);
  }*/
}

