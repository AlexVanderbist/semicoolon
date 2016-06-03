using UnityEngine;
using System.Collections;
using LitJson;

public class DataSender : MonoBehaviour {

  // SENDS THE ANSWER OF THE USER WHEN A PAPER IS SWIPED

  public string answerURL = "http://semicolon.multimediatechnology.be/api/v1/";
  public string answerURLTokenPart = "?token=";

  StampController sController;
  GameInfo GI;
  int numberAnswer;

  void Start()
  {
    sController = GetComponent<StampController>();
    GI = GameObject.Find("GameData").GetComponent<GameInfo>();
  }

  // METHOD LISTENS TO SENDMESSAGE, FROM GAME CONTROLLER
  // IF TOKEN IS EXPIRED, TOKENRECEIVER SCRIPT RECEIVES A SENDMESSAGE
  // TOKENRECEIVER SEND A MESSAGE BACK WHEN ITS READY TO THIS METHOD SO THE PROCESS CAN START AGAIN
  public void StartSendingAnswer()
  {
    StartCoroutine(SendAnswer());
  }

  // SET WHEN A NUMBER IS CHOSEN FROM NUMBER PANEL
  public void numberReceiver(int initnumber)
  {
    numberAnswer = initnumber;
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
        value = sController.StampNumber;
        Debug.Log(value);
        break;
      default:
        break;
    }
    Form.AddField("value", value);
    int projectNumber = GI.CurrentProjectNumber;
    int questionNumber = GI.CurrentQuestionNumber -1;
    Debug.Log("ProjectNumber: " + projectNumber + ", QUESTION ID: " + GI.QuestionIds[projectNumber][questionNumber]);
    Debug.Log("Projectvraag: " + GI.Questions[projectNumber][questionNumber]);
    url += answerURL + GI.QuestionIds[projectNumber][questionNumber] + answerURLTokenPart + GI.Token;
   
    WWW antwoordWWW = new WWW(url, Form);

    yield return antwoordWWW;

    textData = JsonMapper.ToObject(antwoordWWW.text);
    if (antwoordWWW.error != null)
    {
        if (textData["error"].ToString() == "token_expired")
        {
          gameObject.SendMessage("StartReceivingNewToken", "StartSendingAnswer");
        }
    }
    else
    {
      if (textData["status"].ToString() == "success")
      {
        Debug.Log("In orde");
      }
    }
  }
}
