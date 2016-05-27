using UnityEngine;
using System.Collections;
using LitJson;

public class DataSender : MonoBehaviour {

  public string answerURL = "http://semicolon.multimediatechnology.be/api/v1/";
  public string answerURLTokenPart = "?token=";

  StampController sController;
  GameInfo GI;
  int currentQuestionNumber = 0;
  int numberAnswer;
  string selectedStamp;

  void Start()
  {
    sController = GetComponent<StampController>();
    GI = GameObject.Find("GameData").GetComponent<GameInfo>();
  }
  
  public void StartSendingAnswer()
  {
    Debug.Log("send");
    StartCoroutine(SendAnswer());
  }

  public void numberReceiver(int initnumber)
  {
    numberAnswer = initnumber;
  }

  public void ReSendAnswer()
  {
    StartCoroutine(SendAnswer());
  }

  IEnumerator SendAnswer()
  {
    int value = 0;
    string url = "";
    JsonData textData;
    WWWForm Form = new WWWForm();
    switch (sController.SelectedStamp)
    {
      case "green":
        value = 1;
        break;
      case "red":
        value = 2;
        break;
      case "number":
        value = numberAnswer;
        Debug.Log(value);
        break;
      default:
        break;
    }
    Form.AddField("value", value);
    int projectNumber = GI.CurrentProjectNumber;
    Debug.Log("ProjectNumber: " + projectNumber + ", QUESTION ID: " + GI.QuestionIds[projectNumber][currentQuestionNumber]);
    url += answerURL + GI.QuestionIds[projectNumber][currentQuestionNumber] + answerURLTokenPart + GI.Token;
   
    WWW antwoordWWW = new WWW(url, Form);

    yield return antwoordWWW;

    textData = JsonMapper.ToObject(antwoordWWW.text);
    if (antwoordWWW.error != null)
    {
        if (textData["error"].ToString() == "token_expired")
        {
          gameObject.SendMessage("StartReceivingNewToken", "ReSendAnswer");
        }
    }
    else
    {
      if (textData["status"].ToString() == "success")
      {
        Debug.Log("In orde");
      }
    }

    //set currentQuestionNumber For Next Answer
    currentQuestionNumber++;
  }
}
