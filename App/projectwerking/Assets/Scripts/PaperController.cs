﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System;
using UnityEngine.UI;

public class PaperController : MonoBehaviour
{

  // MOVES AND LOADS TEXT OF EACH PAPER
  // DOES NOT LISTEN TO ACTUAL INPUT, GAME CONTROLLER DOES THAT

  public GameObject tMeshNormalText, tMeshTitleText, tMeshPrefab;
  public Sprite goodNotGoodSprite, numberSprite;
  public Transform startposition, focusposition, endposition;
  public bool ListIsReady = false; //PUBLIC BECAUSE GAMECONTROLLER NEEDS TO KNOW IF IT IS READY
  public Text testTextBox;
  public GameObject donePaperPrefab;

  TextMesh tMeshText, tMeshTitle;
  GameObject currentPaper, newPaper;

  string titleText = "Vraag: ";
  string[] QuestionList;

  int currentQuestionNr = 0; // DOES NOT USE GI CURRENT QUESTION NUMBER BECAUSE IT WOULD NEED TO ACCESS IT TOO OFTEN
  int numberOfQuestions = 0;
  int currentProjectNumber = 0;
  GameInfo GI;
  
  // LOAD VARIABLES NEEDED
  void Awake() {
    tMeshText = tMeshNormalText.GetComponent<TextMesh>();
    tMeshTitle = tMeshTitleText.GetComponent<TextMesh>();
    testTextBox.enabled = false;

    GI = GameObject.Find("GameData").GetComponent<GameInfo>();
    GI.CurrentQuestionNumber = 0;
    currentProjectNumber = GI.CurrentProjectNumber;
    QuestionList = GI.Questions[currentProjectNumber];
    numberOfQuestions = QuestionList.Length;
    ListIsReady = true;
    Debug.Log("Aantal vragen: " + numberOfQuestions);
  }

  // SET FIRST PAPER TEXT
  public void SetBeginValues() {
    tMeshText.text = ResolveTextSize(QuestionList[currentQuestionNr], 24);
    tMeshTitle.text = titleText + (currentQuestionNr + 1).ToString();
  }

  // CHECKS IF A LINE IS NOT TOO LONG, SPLITS THE STRING UP IN LINES, NEEDED BECAUSE OF 3D TEXT
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

  // SET TEXT FOR EACH PANEL
  void setText() {
    Debug.Log("QuestionNr: " + currentQuestionNr);
    string temp = ResolveTextSize(GI.Questions[currentProjectNumber][currentQuestionNr-1],24);
    tMeshText.text = temp;
    tMeshTitle.text = titleText + (currentQuestionNr).ToString() + "/" + numberOfQuestions;
 
  }

  public void DestroyCurrentPaper() {
    Destroy(currentPaper);
  }

  // SETS THE NEW PAPER AS CURRENT PAPER BECAUSE THE OLD ONE IS DELETED
  public void setCurrentPaper() {
    currentPaper = newPaper;
  }

  // BOOLEAN BECAUSE IT CHECKS IF THE LAST QUESTION IS REACHED, IF SO GAME CONTROLLER KNOWS ABOUT IT
  public bool createNewPaper()
  {
    bool questionPaperCreated = false;
    currentQuestionNr++;
    GI.CurrentQuestionNumber = currentQuestionNr - 1;
    if (currentQuestionNr <= numberOfQuestions)
    {
      setText();
      newPaper = (GameObject)Instantiate(tMeshPrefab, startposition.position, transform.rotation);
      int type = GI.QuestionTypes[GI.CurrentProjectNumber][currentQuestionNr-1];
      if (type == 1)
      {
        // GOOD / NOT GOOD
        newPaper.transform.FindChild("Paper").GetComponent<SpriteRenderer>().sprite = goodNotGoodSprite;
      }
      else if(type == 2)
      {
        // NUMBERS
        newPaper.transform.FindChild("Paper").GetComponent<SpriteRenderer>().sprite = numberSprite;
      }
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


