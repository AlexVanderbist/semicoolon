using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System;
using UnityEngine.UI;

public class PaperController : MonoBehaviour
{

  public RectTransform paperPrefab;
  public Text paperTextbox;
  public Text titleTextbox;
  public Transform startposition, focusposition, endposition;
  public float speed = 0.5f;

  RectTransform currentPaper;
  int currentQuestionNr = 0;
  //string url = "http://semicolon.multimediatechnology.be/projecten";
  string testurl = "http://jsonplaceholder.typicode.com/posts";

  string titleText = "Vraag: ";
  string questionValue = "body";
  string currentQuestionString;
  List<string> QuestionList = new List<string>();

  int numberOfQuestions = 0;
  static WWW www;
  JsonData textData;


  IEnumerator Start()
  {
    currentQuestionNr++;
    www = new WWW(testurl);
    yield return www;
    if (www.error == null)
    {
      textData = JsonMapper.ToObject(www.text);

      numberOfQuestions = textData.Count;
      for (int i = 0; i < numberOfQuestions; i++)
      {
        QuestionList.Add(textData[i][questionValue].ToString());
      }
      Debug.Log(QuestionList.Count);
    }
    else
    {
      Debug.Log("ERROR: " + www.error);
    }

  }

  public void setText() {
    paperTextbox.text = QuestionList[currentQuestionNr];
    titleTextbox.text = titleText + currentQuestionNr.ToString();
    currentQuestionNr++;
    Debug.Log(currentQuestionNr);
  }

  public void setFirstPaper() {
    RectTransform newPaper;
    if (currentPaper == null)
    {
      setText();
      newPaper = (RectTransform)Instantiate(paperPrefab, focusposition.position, transform.rotation);

      currentPaper = newPaper;

    }
  }

  public void SwipeNext()
  {
    RectTransform newPaper;
      newPaper = (RectTransform)Instantiate(paperPrefab, startposition.position, transform.rotation);
      newPaper.transform.localPosition = Vector3.Lerp(focusposition.position, endposition.position, speed);

      setText();
      currentPaper.transform.localPosition = Vector3.Lerp(startposition.position, focusposition.position, speed);
      Destroy(currentPaper);
      currentPaper = newPaper;
      
    }


  }

  /*
  public void SwipePrevious() {
    currentQuestionNr--;
    currentPaper.transform.position = Vector3.Lerp(focusposition.position, endposition.position, speed);
  }*/


