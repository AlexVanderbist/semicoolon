 using UnityEngine;
using System.Collections;
using LitJson;

public class StampController : MonoBehaviour {

  // MOVES THE STAMPS, DOES NOT CHECK FOR INPUT
  // GAME CONTROLLER PROVIDES THE INPUT

  public GameObject redStamp, greenStamp, numberStamp;
  public int maxScaleToAdd = 25;
  public string answerURL = "http://semicolon.multimediatechnology.be/api/v1/";
  public string answerURLTokenPart = "?token=";
  
  //POSITIONS AND SCALES TO REMEMBER TO MOVE/SCALE TO
  Vector3 restPosRedStamp, restPosGreenStamp, restPosNumberStamp, redPosHide, greenPosHide, numberPosHide;
  Vector3 restPos, maxScale, restScale, positionToStamp;
  GameObject stampToMove;

  int previousQuestionType = 0; //NEED TO KNOW BECAUSE OTHERWISE THE HIDING OF STAMPS STARTS FOR NOTHING
  float rotationZ = 0;
  RaycastHit hitInfo;
  GameInfo GI;

  private string selectedStamp = ""; // SOME CLASSES NEED TO KNOW ABOUT THIS SO A PROPERTY IS DECLARED
  private int stampNumber; // DATA SENDER NEEDS TO KNOW, IS SET WITH METHOD SET RAYCAST HIT

  // VALUES THAT NEED TO BE SET AND USED THROUGH THE PROCES
  void Start () {
    restPosRedStamp = redStamp.transform.position;
    restPosGreenStamp = greenStamp.transform.position;
    restPosNumberStamp = numberStamp.transform.position;

    redPosHide = new Vector3(redStamp.transform.position.x - 250f, redStamp.transform.position.y, redStamp.transform.position.z);
    greenPosHide = new Vector3(greenStamp.transform.position.x - 250f, greenStamp.transform.position.y, greenStamp.transform.position.z);
    numberPosHide = new Vector3(numberStamp.transform.position.x + 250f, numberStamp.transform.position.y, numberStamp.transform.position.z);
    // ALL REST SCALES HAVE TO BE SAME VALUE
    restScale = redStamp.transform.localScale;
    maxScale = new Vector3(redStamp.transform.localScale.x + maxScaleToAdd, redStamp.transform.localScale.y + maxScaleToAdd, redStamp.transform.localScale.z + maxScaleToAdd);
    GI = GameObject.Find("GameData").GetComponent<GameInfo>();
	}

  // RECEIVES FROM GAME CONTROLLER THE NAME OF THE CURRENT STAMP HOLDING, STAMP CONTROLLER NEEDS THIS TO MOVE THE RIGHT STAMPS 
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
  
  // USED WHEN WE ARE HOLDING A STAMP
  public void ScaleStamp(float step)
  {
    stampToMove.transform.localScale = Vector3.Lerp(stampToMove.transform.localScale, maxScale, step);
  }

  // DEACTIVATES STAMPS DEPENDING ON QUESTION TYPE AND SAVES WHICH QUESTION TYPE WAS THE PREVIOUS ONE
  public void DeActivateStamps()
  {
    if (GI.QuestionTypes[GI.CurrentProjectNumber][GI.CurrentQuestionNumber] == 1)
    {
      previousQuestionType = 1;
      numberStamp.tag = "Untagged";
      greenStamp.tag = "Stamp";
      redStamp.tag = "Stamp";
    }
    else
    {
      previousQuestionType = GI.QuestionTypes[GI.CurrentProjectNumber][GI.CurrentQuestionNumber];
      numberStamp.tag = "Stamp";
      greenStamp.tag = "Untagged";
      redStamp.tag = "Untagged";
    }
  }

  // MOVES STAMPS OUT OF IN THE SCREEN TO BE USED
  public void HideUnusedStamps(float step)
  {
    if (GI.QuestionTypes[GI.CurrentProjectNumber][GI.CurrentQuestionNumber] != previousQuestionType)
    {
      if (GI.QuestionTypes[GI.CurrentProjectNumber][GI.CurrentQuestionNumber] == 1 )
      {
        numberStamp.transform.localPosition = Vector3.Lerp(numberStamp.transform.position, numberPosHide, step);
        greenStamp.transform.localPosition = Vector3.Lerp(greenStamp.transform.position, restPosGreenStamp, step);
        redStamp.transform.localPosition = Vector3.Lerp(redStamp.transform.position, restPosRedStamp, step);
      }
      else
      {
        greenStamp.transform.localPosition = Vector3.Lerp(greenStamp.transform.position, greenPosHide, step);
        redStamp.transform.localPosition = Vector3.Lerp(redStamp.transform.position, redPosHide, step);
        numberStamp.transform.localPosition = Vector3.Lerp(numberStamp.transform.position, restPosNumberStamp, step);
      }
    }
    
  }

  // STAMPS ARE DEACTIVATED IN THE BEGINNING AND NOT IN THEIR HIDING POS IF NEEDED, THIS METHOD FIXES THAT
  // DEACTIVATED BECAUSE OTHERWISE IT WOULD LOOK AWFULL
  public void HideFirstTime()
  {
    if (GI.QuestionTypes[GI.CurrentProjectNumber][GI.CurrentQuestionNumber] == 1)
    {
      numberStamp.transform.localPosition = numberPosHide;
    }
    else
    {
      redStamp.transform.localPosition = redPosHide;
      greenStamp.transform.localPosition = greenPosHide;
    }
    greenStamp.SetActive(true);
    redStamp.SetActive(true);
    numberStamp.SetActive(true);
  }

  // CHECKS HARDCODED HOW HIGH THE STAMP IS, WITHOUT THIS SOME ROTATIONS WOULD LOOK AWFULL ON A SPECIFIC HEIGHT
  private float getRotation(GameObject stamp)
  {
    float height = stamp.transform.position.y;
    float rotation = 0f;
    if (height < -110f)
    {
      rotation = -65f;
    }
    if (height >= -110f && height < -65f)
    {
      rotation = -70f;
    }
    if (height >= -65f && height < 0)
    {
      rotation = -80f;
    }
    if (height >= 0f && height < 50)
    {
      rotation = -90f;
    }
    if (height >= 50f)
    {
      rotation = -100f;
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

  // SETS THE POINT WHERE THE STAMP NEEDS TO MOVE TO
  public bool setRaycastHit(RaycastHit hit) {
    hitInfo = hit;
    positionToStamp = new Vector3(hit.point.x, hit.point.y, hit.point.z - 20f);
    if (selectedStamp == "number")
    {
      stampNumber = int.Parse(hit.collider.gameObject.name);
    }
    return true;
  }

  public string SelectedStamp
  {
    get { return selectedStamp; }
    set { selectedStamp = value; }
  }

  public int StampNumber
  {
    get { return stampNumber; }
    set { stampNumber = value; }
  }
}
