 using UnityEngine;
using System.Collections;

public class StampController : MonoBehaviour {

  public GameObject redStamp, greenStamp, paper;
  Vector3 restStateRedStamp, restStateGreenStamp;

  private float fixedRotation = 2.58f;
  private float rotationSpeed = 1.5f;
  int currentQuestionNumber = 0;
  string selectedStamp = "";
  private string antwoordUrl = "http://semicolon.multimediatechnology.be/Project1/Stelling1";
  GameObject hitInfo;

  // Use this for initialization
  void Start () {
    restStateRedStamp = redStamp.transform.position;
    restStateGreenStamp = greenStamp.transform.position;
	}

  public bool CheckStamp(string hit) {
    bool readyChecking = false;
      if (hit == "RedStamp")
      {
        redStamp.transform.position = new Vector3(redStamp.transform.position.x, restStateRedStamp.y + 20, redStamp.transform.position.z);
        greenStamp.transform.position = restStateGreenStamp;
        selectedStamp = "red";
        readyChecking = true;
      }
      else if (hit == "GreenStamp")
      {
        greenStamp.transform.position = new Vector3(greenStamp.transform.position.x, restStateGreenStamp.y + 20, greenStamp.transform.position.z);
        redStamp.transform.position = restStateRedStamp;
        selectedStamp = "green";
        readyChecking = true;
      }
    return readyChecking;
  }

  public void MoveStampToPaper(float step) {
    if (selectedStamp == "green") {
      greenStamp.transform.localPosition = Vector3.Lerp(greenStamp.transform.position, hitInfo.transform.position, step);
      if (greenStamp.transform.rotation.z > -70)
      {
        greenStamp.transform.rotation = Quaternion.Slerp(greenStamp.transform.rotation, Quaternion.Euler(0, 90, -70), step);
      }
    }
    else if (selectedStamp == "red")
    {
      redStamp.transform.localPosition = Vector3.Lerp(redStamp.transform.position, hitInfo.transform.position, step);
    }
  }

  public void MoveStampBackToRestPosition(float step)
  {
    if (selectedStamp == "green")
    {
      greenStamp.transform.localPosition = Vector3.Lerp(greenStamp.transform.position, restStateGreenStamp, step);
      if (greenStamp.transform.rotation.z < fixedRotation)
      {
        greenStamp.transform.rotation = Quaternion.Slerp(greenStamp.transform.rotation, Quaternion.Euler(0, 90, 2.58f), step);
      }
    }
    else if (selectedStamp == "red")
    {
      redStamp.transform.localPosition = Vector3.Lerp(redStamp.transform.position, restStateRedStamp, step);
    }
  }

  public bool CheckPaper(GameObject hit) {
    bool stampIsReady = false;
    if (hit.transform.name == "Paper")
    {
      hitInfo = hit;
      stampIsReady = true;
    }
    else if(hit.transform.name != "Paper")
    {
      stampIsReady = false;
    }
    return stampIsReady;
  }

  IEnumerator SendAnswer() {
    WWWForm Form = new WWWForm();

    Form.AddField("Antwoord", currentQuestionNumber);
    Form.AddField("VraagNummer", currentQuestionNumber);

    WWW antwoordWWW = new WWW(antwoordUrl, Form);

    yield return antwoordWWW;

    if (antwoordWWW.error != null)
    {
      Debug.LogError("Cannot connect to Login");
    }
    else
    {
    }
    }
}
