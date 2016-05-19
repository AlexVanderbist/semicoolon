 using UnityEngine;
using System.Collections;
using LitJson;

public class StampController : MonoBehaviour {

  public GameObject redStamp, greenStamp, numberStamp;
  public int maxScaleToAdd = 30;
  public string answerURL = "http://semicolon.multimediatechnology.be/api/v1/";
  public string answerURLTokenPart = "?token=";

  Vector3 restPosRedStamp, restPosGreenStamp, restPosNumberStamp;
  Vector3 maxScaleRedStamp, maxScaleGreenStamp, maxScaleNumberStamp;

  GameObject stampToMove;
  Vector3 restPos, maxScale, restScale;

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
      restScale = redStamp.transform.localScale;
      readyChecking = true;
    }
    else if (hit == greenStamp.name)
    {
      rotationZ = getRotation(greenStamp);
      selectedStamp = "green";
      stampToMove = greenStamp;
      restPos = restPosGreenStamp;
      restScale = greenStamp.transform.localScale;
      readyChecking = true;
    }
    else if (hit == numberStamp.name)
    {
      rotationZ = getRotation(numberStamp);
      SelectedStamp = "number";
      stampToMove = numberStamp;
      restPos = restPosNumberStamp;
      restScale = numberStamp.transform.localScale;
      readyChecking = true;
    }
    return readyChecking;
  }

  public void resetStamps() {
    stampToMove.transform.position = restPos;
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
    stampToMove.transform.localPosition = Vector3.Lerp(stampToMove.transform.position, hitInfo.point, step);
    stampToMove.transform.rotation = Quaternion.Slerp(stampToMove.transform.rotation, Quaternion.Euler(0, 90, rotationZ), step);
    stampToMove.transform.localScale = Vector3.Lerp(stampToMove.transform.localScale, maxScale, step);
  }

  public void MoveStampBackToRestPosition(float step)
  {
    stampToMove.transform.localPosition = Vector3.Lerp(stampToMove.transform.position, restPos, step);
    stampToMove.transform.rotation = Quaternion.Slerp(stampToMove.transform.rotation, Quaternion.Euler(0, 90, 315f), step);
    stampToMove.transform.localScale = Vector3.Lerp(stampToMove.transform.localScale, restScale, step);
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
    }
    else if(hit.transform.name != "Paper")
    {
      stampIsReady = false;
    }
    return stampIsReady;
  }
}
