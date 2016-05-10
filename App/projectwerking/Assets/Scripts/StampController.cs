 using UnityEngine;
using System.Collections;

public class StampController : MonoBehaviour {

  public GameObject redStamp, greenStamp;
  public int maxScaleToAdd = 30;
  Vector3 restStateRedStamp, restStateGreenStamp;
  Vector3 maxScaleRedStamp, maxScaleGreenStamp;
  Vector3 greenScaleRestState, redScaleRestState;

  float rotationZ = 0;
  Vector3 extraStampDest;
  int currentQuestionNumber = 0;
  string selectedStamp = "";
  string answer = "";
  public string answerURL = "http://semicolon.multimediatechnology.be/api/v1/";
  RaycastHit hitInfo;
  GameInfo GI;

  // Use this for initialization
  void Start () {
    restStateRedStamp = redStamp.transform.position;
    restStateGreenStamp = greenStamp.transform.position;
    maxScaleRedStamp = new Vector3(redStamp.transform.localScale.x + maxScaleToAdd, redStamp.transform.localScale.y + maxScaleToAdd, redStamp.transform.localScale.z + maxScaleToAdd);
    maxScaleGreenStamp = new Vector3(greenStamp.transform.localScale.x + maxScaleToAdd, greenStamp.transform.localScale.y + maxScaleToAdd, greenStamp.transform.localScale.z + maxScaleToAdd);
    greenScaleRestState = greenStamp.transform.localScale;
    redScaleRestState = redStamp.transform.localScale;
    GI = GetComponent<GameInfo>();
	}

  public bool CheckStamp(string hit) {
    bool readyChecking = false;
      if (hit == "RedStamp")
      {
        rotationZ = getRotation(redStamp);
        selectedStamp = "red";
        answer = "false";
        readyChecking = true;
      }
      else if (hit == "GreenStamp")
      {
        rotationZ = getRotation(greenStamp);
        selectedStamp = "green";
        answer = "true";
        readyChecking = true;
      }
    return readyChecking;
  }

  public void resetStamps() {
    greenStamp.transform.position = restStateGreenStamp;
    redStamp.transform.position = restStateRedStamp;
  }

  private float getRotation(GameObject stamp)
  {
    float height = stamp.transform.position.y;
    float rotation = 0f;
    if (height < -110f)
    {
      rotation = -70f;
    }
    else if (height >= -110f && height < -65f)
    {
      rotation = -75f;
    }
    else if (height >= -65f && height < 0)
    {
      rotation = -80f;
    }
    else if (height >= 0f && height < 50)
    {
      rotation = -85f;
    }
    else if (height >= 50f)
    {
      rotation = -90f;
    }
    return rotation;
  }

  public void MoveStampToPaper(float step) {
    if (selectedStamp == "green") {
      greenStamp.transform.localPosition = Vector3.Lerp(greenStamp.transform.position, hitInfo.point, step);
      greenStamp.transform.rotation = Quaternion.Slerp(greenStamp.transform.rotation, Quaternion.Euler(0, 90, rotationZ), step);
      greenStamp.transform.localScale = Vector3.Lerp(greenStamp.transform.localScale, maxScaleGreenStamp,step);
    }
    else if (selectedStamp == "red")
    {
      redStamp.transform.localPosition = Vector3.Lerp(redStamp.transform.position, hitInfo.point, step);
      redStamp.transform.rotation = Quaternion.Slerp(redStamp.transform.rotation, Quaternion.Euler(0, 90, rotationZ), step);
      redStamp.transform.localScale = Vector3.Lerp(redStamp.transform.localScale, maxScaleRedStamp, step);
    }
  }

  public void MoveStampBackToRestPosition(float step)
  {
    if (selectedStamp == "green")
    {
      greenStamp.transform.localPosition = Vector3.Lerp(greenStamp.transform.position, restStateGreenStamp, step);
      greenStamp.transform.rotation = Quaternion.Slerp(greenStamp.transform.rotation, Quaternion.Euler(0, 90, 340f), step);
      greenStamp.transform.localScale = Vector3.Lerp(greenStamp.transform.localScale, greenScaleRestState, step);
    }
    else if (selectedStamp == "red")
    {
      redStamp.transform.localPosition = Vector3.Lerp(redStamp.transform.position, restStateRedStamp, step);
      redStamp.transform.rotation = Quaternion.Slerp(redStamp.transform.rotation, Quaternion.Euler(0, 90, 340f), step);
      redStamp.transform.localScale = Vector3.Lerp(redStamp.transform.localScale, redScaleRestState, step);
    }
  }

  public bool setRaycastHit(RaycastHit hit) {
    hitInfo = hit;
    return true;
  }

  public bool CheckPaper(RaycastHit hit) {
    bool stampIsReady = false;
    if (hit.transform.name == "Paper")
    {
      hitInfo = hit;
      stampIsReady = true;
      //SendAnswer();
    }
    else if(hit.transform.name != "Paper")
    {
      stampIsReady = false;
    }
    return stampIsReady;
  }

  IEnumerator SendAnswer() {
    WWWForm Form = new WWWForm();

    Form.AddField("response", answer);
    Form.AddField("token", GI.Token);
    answerURL += GI.CurrentProjectNumber + "/" + currentQuestionNumber;

    WWW antwoordWWW = new WWW(answerURL, Form);

    yield return antwoordWWW;

      if (antwoordWWW.error != null)
      {
        Debug.LogError("Can't send answer to API");
      }
      else
      {
        
      }

      //set currentQuestionNumber For Next Answer
      currentQuestionNumber++;
    }
}
