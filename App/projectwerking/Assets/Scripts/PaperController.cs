using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System;
using UnityEngine.UI;

public class PaperController : MonoBehaviour
{
  public GameObject tMeshPrefab, tMeshTitlePrefab;
  public Transform startposition, focusposition, endposition;
  public bool ListIsReady = false;

  TextMesh tMeshText, tMeshTitle;
  GameObject currentPaper, newPaper;
  int currentQuestionNr = 0;
  //string url = "http://semicolon.multimediatechnology.be/projecten";
  string testurl = "http://jsonplaceholder.typicode.com/posts";

  string titleText = "Vraag: ";
  string questionValue = "body";
  List<string> QuestionList = new List<string>();

  int numberOfQuestions = 0;
  static WWW www;
  JsonData textData;

  void Awake() {
    tMeshText = tMeshPrefab.GetComponent<TextMesh>();
    tMeshTitle = tMeshTitlePrefab.GetComponent<TextMesh>();
  }

  public void SetBeginValues() {
    //currentQuestionNr = 0;
    tMeshText.text = ResolveTextSize(QuestionList[currentQuestionNr], 24);
    tMeshTitle.text = titleText + (currentQuestionNr + 1).ToString();
  }

  IEnumerator Start()
  {
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
      ListIsReady = true;
    }
    else
    {
      Debug.Log("ERROR: " + www.error);
    }
  }

 private string ResolveTextSize(string input, int lineLength)
  {

    // Split string by char " "         
    string[] words = input.Split(" "[0]);

    // Prepare result
    string result = "";

    // Temp line string
    string line = "";

    // for each all words        
    foreach (string s in words)
    {
      // Append current word into line
      string temp = line + " " + s;

      // If line length is bigger than lineLength
      if (temp.Length > lineLength)
      {

        // Append current line into result
        result += line + "\n";
        // Remain word append into new line
        line = s;
      }
      // Append current word into current line
      else {
        line = temp;
      }
    }

    // Append last line into result        
    result += line;

    // Remove first " " char
    return result.Substring(1, result.Length - 1);
  }


  public void setText() {
    currentQuestionNr++;
    string temp = ResolveTextSize(QuestionList[currentQuestionNr],24);
    tMeshText.text = temp;
    tMeshTitle.text = titleText + (currentQuestionNr + 1).ToString();
    Debug.Log(currentQuestionNr);
  }

  public void DestroyCurrentPaper() {
    Destroy(currentPaper);
  }

  public void setCurrentPaper() {
    currentPaper = newPaper;
  }

  public void createNewPaper()
  {
    newPaper = (GameObject)Instantiate(tMeshPrefab, startposition.position, transform.rotation);
    setText();
  }

  public void moveNewPaper(float step) {
    newPaper.transform.localPosition = Vector3.Lerp(startposition.position, focusposition.position, step);
  }

  public void moveFocusPaper(float step)
  {  
    currentPaper.transform.localPosition = Vector3.Lerp(focusposition.position, endposition.position, step);
  }
}


