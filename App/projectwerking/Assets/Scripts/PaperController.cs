using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System;
using UnityEngine.UI;

public class PaperController : MonoBehaviour
{
  public GameObject tMeshNormalText, tMeshTitleText, tMeshPrefab;
  public Transform startposition, focusposition, endposition;
  public bool ListIsReady = false;
  public Text testTextBox;
  public GameObject donePaperPrefab;

  TextMesh tMeshText, tMeshTitle;
  GameObject currentPaper, newPaper;
  int currentQuestionNr = 0;
  //string url = "http://semicolon.multimediatechnology.be/projecten";

  string titleText = "Vraag: ";
  string[] QuestionList;

  int numberOfQuestions = 0;
  int currentProjectNumber = 0;
  GameInfo GI;

  void Awake() {
    tMeshText = tMeshNormalText.GetComponent<TextMesh>();
    tMeshTitle = tMeshTitleText.GetComponent<TextMesh>();
    testTextBox.enabled = false;

    GI = GameObject.Find("GameData").GetComponent<GameInfo>();
    currentProjectNumber = GI.CurrentProjectNumber;
    QuestionList = GI.Questions[currentProjectNumber];
    numberOfQuestions = QuestionList.Length;
    ListIsReady = true;
    Debug.Log("Aantal vragen: " + numberOfQuestions);
  }

  public void SetBeginValues() {
    tMeshText.text = ResolveTextSize(QuestionList[currentQuestionNr], 24);
    tMeshTitle.text = titleText + (currentQuestionNr + 1).ToString();
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


  void setText() {
    Debug.Log("QuestionNr: " + currentQuestionNr);
    string temp = ResolveTextSize(GI.Questions[currentProjectNumber][currentQuestionNr-1],24);
    tMeshText.text = temp;
    tMeshTitle.text = titleText + (currentQuestionNr).ToString();
 
  }

  public void DestroyCurrentPaper() {
    Destroy(currentPaper);
  }

  public void setCurrentPaper() {
    currentPaper = newPaper;
  }

  public bool createNewPaper()
  {
    bool questionPaperCreated = false;
    currentQuestionNr++;
    if (currentQuestionNr <= numberOfQuestions)
    {
      setText();
      newPaper = (GameObject)Instantiate(tMeshPrefab, startposition.position, transform.rotation);
      questionPaperCreated = true;
    }
    else
    {
      newPaper = (GameObject)Instantiate(donePaperPrefab, startposition.position, transform.rotation);
      questionPaperCreated = false;
    }
 
    return questionPaperCreated;
  }

  public void moveNewPaper(float step) {
    newPaper.transform.localPosition = Vector3.Lerp(startposition.position, focusposition.position, step);
  }

  public void moveFocusPaper(float step)
  {  
    currentPaper.transform.localPosition = Vector3.Lerp(focusposition.position, endposition.position, step);
  }

  public GameObject getCurrentPaper
  {
    get { return currentPaper; }
  }
}


