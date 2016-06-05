using UnityEngine;
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
  public GameObject[] maskPlanes;
  public Sprite goodNotGoodSprite, numberSprite;
  public Transform startPosition, focusPosition, endPosition;
  public bool ListIsReady = false; //PUBLIC BECAUSE GAMECONTROLLER NEEDS TO KNOW IF IT IS READY
  public Text testTextBox;
  public GameObject donePaperPrefab, numberPaperPrefab;

  TextMesh tMeshText, tMeshTitle;
  GameObject currentPaper, newPaper, numberPaper;
  Vector3 endPoint;
  Vector3 beginPoint;

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
  void SetText() {
    string temp = ResolveTextSize(GI.Questions[currentProjectNumber][currentQuestionNr - 1], 24);
    tMeshText.text = temp;
    tMeshTitle.text = titleText + (currentQuestionNr).ToString() + "/" + numberOfQuestions;

  }

  public void DestroyCurrentNumberPaper()
  {
    Destroy(numberPaper);
  }

  public void DestroyCurrentPaper() {
    Destroy(currentPaper);
  }

  // SETS THE NEW PAPER AS CURRENT PAPER BECAUSE THE OLD ONE IS DELETED
  public void SetCurrentPaper() {
    currentPaper = newPaper;
  }

  // BOOLEAN BECAUSE IT CHECKS IF THE LAST QUESTION IS REACHED, IF SO GAME CONTROLLER KNOWS ABOUT IT
  public bool CreateNewPaper()
  {
    bool questionPaperCreated = false;
    currentQuestionNr++;
    GI.CurrentQuestionNumber = currentQuestionNr - 1;
    if (currentQuestionNr <= numberOfQuestions)
    {
      SetText();
      newPaper = (GameObject)Instantiate(tMeshPrefab, startPosition.position, transform.rotation);
      int type = GI.QuestionTypes[GI.CurrentProjectNumber][currentQuestionNr - 1];
      if (type == 1)
      {
        // GOOD / NOT GOOD
        newPaper.transform.FindChild("Paper").GetComponent<SpriteRenderer>().sprite = goodNotGoodSprite;
        for (int i = 0; i < maskPlanes.Length; i++)
        {
          newPaper.transform.FindChild(maskPlanes[i].name).gameObject.SetActive(true);
        }
      }
      else if (type == 2)
      {
        // NUMBERS
        newPaper.transform.FindChild("Paper").GetComponent<SpriteRenderer>().sprite = numberSprite;
        for (int i = 0; i < maskPlanes.Length; i++)
        {
          newPaper.transform.FindChild(maskPlanes[i].name).gameObject.SetActive(false);
        }
      }
      questionPaperCreated = true;
    }
    else
    {
      newPaper = (GameObject)Instantiate(donePaperPrefab, startPosition.position, transform.rotation);
      questionPaperCreated = false;
    }
    for (int i = 0; i < numberPaperPrefab.transform.childCount; i++)
    {
      if (numberPaperPrefab.transform.FindChild("Hover" + i))
      {
        numberPaperPrefab.transform.FindChild("Hover" + i).gameObject.SetActive(false);
      }
    }

    return questionPaperCreated;
  }

  public void CreateNumbersPaper()
  {
    numberPaper = (GameObject)Instantiate(numberPaperPrefab, startPosition.position, transform.rotation);
  }

  public void MoveNewPaper(float step, string paperName) {
    if (paperName == "normalPaper")
    {
      newPaper.transform.localPosition = Vector3.Lerp(startPosition.position, focusPosition.position, step);
    }
    else if (paperName == "numberPaper")
    {
      numberPaper.transform.localPosition = Vector3.Lerp(endPosition.position, focusPosition.position, step);
    }
  }

  public void ReFocusPaper(float step)
  {
    currentPaper.transform.localPosition = Vector3.Lerp(currentPaper.transform.position, focusPosition.position, step);
  }

  public void MoveFocusPaper(float step, string paperName)
  {
    if (paperName == "currentPaper")
    {
      currentPaper.transform.localPosition = Vector3.Lerp(currentPaper.transform.position, endPoint, step);
    }
    else if (paperName == "numberPaper")
    {
      numberPaper.transform.localPosition = Vector3.Lerp(focusPosition.position, endPosition.position, step);
    }
  }

  public void SetBeginPoint(Transform obj)
  {
    beginPoint = new Vector3(obj.position.x, obj.position.y, obj.position.z);
  }

  public void SetEndPointPaper(Transform obj)
  {
    float x = 0f;
    float y = 0f;
    if (obj.position.y < beginPoint.y)
    {
      y -= 600f;
    }
    else
    {
      y += 600f;
    }
    if (obj.position.x < beginPoint.x)
    {
      x -= 600f;
    }
    else
    {
      x += 600f;
    }
    Debug.Log("x = " + x + "y = " + y);
    endPoint = new Vector3(x, y, currentPaper.transform.localPosition.z);
  }

  public GameObject GetCurrentNumberPaper
  {
    get { return numberPaper; }
  }

  public GameObject GetCurrentPaper
  {
    get { return currentPaper; }
  }
}


