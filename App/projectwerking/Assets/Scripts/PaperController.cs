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
  public Text testTextBox;
  
  TextMesh tMeshText, tMeshTitle;
  GameObject currentPaper, newPaper;
  int currentQuestionNr = 0;
  //string url = "http://semicolon.multimediatechnology.be/projecten";

  string titleText = "Vraag: ";
  List<string> QuestionList = new List<string>();

  int numberOfQuestions = 0;
  GameInfo GI;

  void Start()
  {
    GI = GetComponent<GameInfo>();
    numberOfQuestions = GI.Questions[GI.CurrentProjectNumber].Length;
  }

  void Awake() {
    tMeshText = tMeshPrefab.GetComponent<TextMesh>();
    tMeshTitle = tMeshTitlePrefab.GetComponent<TextMesh>();

    testTextBox.enabled = false;
    
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
    string temp = ResolveTextSize(GI.Questions[GI.CurrentProjectNumber][currentQuestionNr],24);
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

  public bool createNewPaper()
  {
    bool paperCreated = false;
    currentQuestionNr++;
    if (currentQuestionNr <= numberOfQuestions)
    {
      newPaper = (GameObject)Instantiate(tMeshPrefab, startposition.position, transform.rotation);
      setText();
      paperCreated = true;
    }
    return paperCreated;
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


