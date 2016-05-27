 using UnityEngine;
using System.Collections;
using LitJson;

public class StampController : MonoBehaviour {

  public GameObject redStamp, greenStamp, numberStamp;
  public int maxScaleToAdd = 30;
  public string answerURL = "http://semicolon.multimediatechnology.be/api/v1/";
  public string answerURLTokenPart = "?token=";

  Vector3 restPosRedStamp, restPosGreenStamp, restPosNumberStamp;

  GameObject stampToMove;
  Vector3 restPos, maxScale, restScale, positionToStamp;

  float rotationZ = 0;
  float beginRotation = 0;
  RaycastHit hitInfo;
  GameInfo GI;


  private string selectedStamp = "";

  public string SelectedStamp
  {
    get { return selectedStamp; }
    set { selectedStamp = value; }
  }


  // Use this for initialization
  void Start () {
    restPosRedStamp = redStamp.transform.position;
    restPosGreenStamp = greenStamp.transform.position;
    restPosNumberStamp = numberStamp.transform.position;
    beginRotation = redStamp.transform.rotation.z;

    // ALL REST SCALES HAVE TO BE SAME VALUE
    restScale = redStamp.transform.localScale;
    maxScale = new Vector3(redStamp.transform.localScale.x + maxScaleToAdd, redStamp.transform.localScale.y + maxScaleToAdd, redStamp.transform.localScale.z + maxScaleToAdd);
    GI = GameObject.Find("GameData").GetComponent<GameInfo>();
	}

  public bool CheckStamp(string hit) {
    bool readyChecking = false;
    if (hit == redStamp.name)
    {
      rotationZ = getRotation(redStamp);
      selectedStamp = "red";
      stampToMove = redStamp;
      restPos = restPosRedStamp;
      readyChecking = true;
    }
    else if (hit == greenStamp.name)
    {
      rotationZ = getRotation(greenStamp);
      selectedStamp = "green";
      stampToMove = greenStamp;
      restPos = restPosGreenStamp;
      readyChecking = true;
    }
    else if (hit == numberStamp.name)
    {
      rotationZ = getRotation(numberStamp);
      SelectedStamp = "number";
      stampToMove = numberStamp;
      restPos = restPosNumberStamp;
      readyChecking = true;
    }
    return readyChecking;
  }

  /*
  public void ResetStamps(float step) {
    stampToMove.transform.position = restPos;
    stampToMove.transform.localScale = restScale;
  }*/

  public void ScaleStamp(float step)
  {
    stampToMove.transform.localScale = Vector3.Lerp(stampToMove.transform.localScale, maxScale, step);
  }

  public void DeActivateStamps()
  {
    Debug.Log("QuestionType: " + GI.QuestionTypes[GI.CurrentProjectNumber][GI.CurrentQuestionNumber]);
    if (GI.QuestionTypes[GI.CurrentProjectNumber][GI.CurrentQuestionNumber] == 1)
    {
      numberStamp.tag = "Untagged";
      greenStamp.tag = "Stamp";
      redStamp.tag = "Stamp";
    }
    else
    {
      numberStamp.tag = "Stamp";
      greenStamp.tag = "Untagged";
      redStamp.tag = "Untagged";
    }
  }

  private float getRotation(GameObject stamp)
  {
    float height = stamp.transform.position.y;
    float rotation = 0f;
    if (height < -110f)
    {
      rotation = -65f;
    }
    else if (height >= -110f && height < -65f)
    {
      rotation = -70f;
    }
    else if (height >= -65f && height < 0)
    {
      rotation = -75f;
    }
    else if (height >= 0f && height < 50)
    {
      rotation = -80f;
    }
    else if (height >= 50f)
    {
      rotation = -85f;
    }
    return rotation;
  }

  public void MoveStampToPaper(float step) {
    stampToMove.transform.localPosition = Vector3.Lerp(stampToMove.transform.position, positionToStamp, step);
    stampToMove.transform.rotation = Quaternion.Slerp(stampToMove.transform.rotation, Quaternion.Euler(0, 90, rotationZ), step);
  }

  public void MoveStampBackToRestPosition(float step)
  {
    stampToMove.transform.localPosition = Vector3.Lerp(stampToMove.transform.position, restPos, step);
    stampToMove.transform.rotation = Quaternion.Slerp(stampToMove.transform.rotation, Quaternion.Euler(0, 90, 315f), step);
    stampToMove.transform.localScale = Vector3.Lerp(stampToMove.transform.localScale, restScale, step);
  }

  public bool setRaycastHit(RaycastHit hit) {
    hitInfo = hit;
    positionToStamp = new Vector3(hit.point.x, hit.point.y, hit.point.z - 20f);
    return true;
  }
}
