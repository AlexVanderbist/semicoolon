using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PaperController))]
[RequireComponent(typeof(StampController))]
public class MobileInput : MonoBehaviour {

  public float minSwipeDistY;
  public float minSwipeDistX;
  public float speed = 0.5f;
  public float stampSpeed = 0.7f;

  private Vector2 startPos;
  private PaperController pController;
  private StampController sController;

  //ALL BOOLEANS THAT CONTROL THE INPUT AND MOVEMENT OF OBJECTS
  private bool moveObjects = false;
  private bool firstObject = false;
  private bool firstObjectCreated = false;
  private bool receiveInput = false;
  private bool readyToCheckStamps = false;
  private bool readyToMoveStampToPaper = false;
  private bool readyWithStampToPaper = false;
  private bool stampSelected = false;

  private float step = 0f;

  // Use this for initialization
  void Start () {
    pController = GetComponent<PaperController>();
    sController = GetComponent<StampController>();
	}
	
	// Update is called once per frame
	void Update () {
    if (pController.ListIsReady)
    {
      //Spawn First Object
      if (!firstObjectCreated)
      {
        pController.SetBeginValues();
        pController.createNewPaper();
        firstObjectCreated = true;
        firstObject = true;
      }

      //Move First Object
      if (firstObject)
      {
        step += speed * Time.deltaTime;
        pController.moveNewPaper(step);
        pController.setCurrentPaper();

        if (step >= 1f)
        {
          firstObject = false;
          step = 0f;
          readyToCheckStamps = true;
        }
      }

      //PC Input, After first object is created
      if (receiveInput)
      {
        if (Input.GetButtonDown("Jump"))
        {
          pController.createNewPaper();
          moveObjects = true;
          receiveInput = false;
        }
      }

      //Move New and Focused Paper
      if (moveObjects)
      {
          step += speed * Time.deltaTime;
          pController.moveNewPaper(step);
          pController.moveFocusPaper(step);
          if (step >= 1)
          {
            moveObjects = false;
            step = 0;
            pController.DestroyCurrentPaper();
            pController.setCurrentPaper();
            readyToCheckStamps = true;
          }
      }

      if (readyToMoveStampToPaper) {
        if (!readyWithStampToPaper)
        {
          step += stampSpeed * Time.deltaTime;
          sController.MoveStampToPaper(step);
          if (step >= 1)
          {
            step = 0;
            readyWithStampToPaper = true;
          }
        }
        else
        {
          step += stampSpeed * Time.deltaTime;
          sController.MoveStampBackToRestPosition(step);
          if (step >= 1)
          {
            step = 0;
            receiveInput = true;
            readyToMoveStampToPaper = false;
            readyWithStampToPaper = false;
          }
        }
      }

      //Evrything that checks and handles the stamps
      if (readyToCheckStamps)
      {
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
        if (Input.touchCount > 0)
        {
          if (stampSelected)
          {
            if (hitInfo.collider.gameObject.name == "Paper")
            {
              readyToMoveStampToPaper = sController.CheckPaper(hitInfo.collider.gameObject);
            }
          }

          if (hit && !readyToMoveStampToPaper)
          {
            stampSelected = sController.CheckStamp(hitInfo.collider.gameObject.name);
          }

          if (readyToMoveStampToPaper)
          {
            readyToCheckStamps = false;
            stampSelected = false;
          }
         
        }
        Debug.Log(hitInfo.collider.gameObject.name);
      }
    }

    if (receiveInput)
    {
      if (Input.touchCount > 0)
      {
        Touch touch = Input.touches[0];

        switch (touch.phase)
        {

          case TouchPhase.Began:

            startPos = touch.position;
            break;

            case TouchPhase.Ended:

            float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;

            if (swipeDistHorizontal > minSwipeDistX)
            {

              float swipeHValue = Mathf.Sign(touch.position.x - startPos.x);

              if (swipeHValue > 0)//right swipe
              {
                pController.createNewPaper();
                moveObjects = true;
                receiveInput = false;
              }

              else if (swipeHValue < 0)//left swipe
              {
              //

              }
            }
            break;
        }
      }
    }
  }
}
