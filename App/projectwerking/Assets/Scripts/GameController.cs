using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PaperController))]
[RequireComponent(typeof(StampController))]
public class GameController : MonoBehaviour {

  // THIS SCRIPT CONTROLS ALL INPUT AND ACTIVATES OTHER SCRIPTS WHEN NEEDED

  public float minSwipeDistY;
  public float minSwipeDistX;
  public float paperSpeed = 0.8f;
  public float stampSpeed = 0.7f;

  // USED TO CHECK SWIPING INPUT
  private Vector2 startPos;

  // MAIN SCRIPTS THAT WILL BE USED BY THIS SCRIPT
  private PaperController pController;
  private StampController sController;

  // ALL BOOLEANS THAT CONTROL THE INPUT AND MOVEMENT OF OBJECTS
  // THESE BOOLEANS ARE STATES OF THE UPDATE
  private bool readyToMovePaper = false;
  private bool isFirstPaper = false;
  private bool firstPaperCreated = false;
  private bool readyToSwipePaper = false;
  private bool readyToCheckStamps = false;
  private bool readyToMoveStampToPaperAndBack = false;
  private bool readyWithStampToPaper = false;
  private bool islastQuestionReached = false;
  private bool isStampScaled = false;
  private bool needToResetStamps = false;

  // MAIN VARIABLE TO MOVE THINGS, GETS RESET EVERYTIME IT REACHES 1
  // USED FOR LERPS 
  private float step = 0f;
  private float dragStep = 0f; // EXTRA STEP BECAUSE OF CONFLICTS WITH MAIN STEP

  //DRAGING OBJECTS
  private float dist;
  private bool dragging = false;
  private Vector3 offset;
  private Transform toDrag;
  private RaycastHit stampPoint; // USED TO REMEMBER STAMP POINT, WHEN IN NEED OF NUMBER INPUT, SO IT CAN BE PASSED AS PARAMETER AT LAST

  void Start () {
    pController = GetComponent<PaperController>();
    sController = GetComponent<StampController>();
	}

  // THE MAIN INPUT METHOD
  void Update()
  {
    //WAIT TILL QUESTIONS ARE LOADED
    if (pController.ListIsReady)
    {
      //SPAWN FIRST OBJECT
      if (!firstPaperCreated)
      {
        pController.createNewPaper();
        firstPaperCreated = true;
        isFirstPaper = true;
        sController.DeActivateStamps();
        sController.HideFirstTime();
      }

      //MOVE FIRST OBJECT
      if (isFirstPaper)
      {
        step += paperSpeed * Time.deltaTime;
        pController.moveNewPaper(step);
        pController.setCurrentPaper();

        if (step >= 1f)
        {
          isFirstPaper = false;
          step = 0f;
          readyToCheckStamps = true;
        }
      }

      // MOVE NEW AND FOCUSED PAPER
      // ALSO THE STAMPS DEPENDING ON QUESTION TYPE GET HIDED
      if (readyToMovePaper)
      {
        step += paperSpeed * Time.deltaTime;
        pController.moveNewPaper(step);
        pController.moveFocusPaper(step);
        if (!islastQuestionReached)
        {
          sController.HideUnusedStamps(step);
        }
        if (step >= 1)
        {
          readyToMovePaper = false;
          step = 0;
          pController.DestroyCurrentPaper();
          pController.setCurrentPaper();
          if (!islastQuestionReached)
          {
            sController.DeActivateStamps();
          }
          readyToCheckStamps = true;
        }
      }

      //MOVE SELECTED STAMP WHEN PAPER HAS BEEN HIT BY RAYCAST
      if (readyToMoveStampToPaperAndBack)
      {
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
            readyToSwipePaper = true;
            readyToCheckStamps = true;
            readyToMoveStampToPaperAndBack = false;
            readyWithStampToPaper = false;
          }
        }
      }

      //DRAG STAMPS
      if (readyToCheckStamps)
      {
        Vector3 v3;

        if (Input.touchCount != 1)
        {
          dragging = false;
          return;
        }

        // SCALE WHILE DRAGGING
        if (dragging && !isStampScaled)
        {
          dragStep += 0.5f * Time.deltaTime;
          sController.ScaleStamp(dragStep);
          if (dragStep >= 1)
          {
            dragStep = 0;
            isStampScaled = true;
          }
        }

        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        // STATE 1
        if (touch.phase == TouchPhase.Began)
        {
          RaycastHit hit;
          Ray ray = Camera.main.ScreenPointToRay(pos);
          if (Physics.Raycast(ray, out hit) && (hit.collider.tag == "Stamp"))
          {
            Debug.Log("Here");
            toDrag = hit.transform;
            dist = hit.transform.position.z - Camera.main.transform.position.z;
            v3 = new Vector3(pos.x, pos.y, dist);
            v3 = Camera.main.ScreenToWorldPoint(v3);
            offset = toDrag.position - v3;
            dragging = true;
            readyToSwipePaper = false;
            sController.CheckStamp(toDrag.name);
          }
        }
        // STATE 2
        if (dragging && touch.phase == TouchPhase.Moved)
        {
          v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
          v3 = Camera.main.ScreenToWorldPoint(v3);
          toDrag.position = v3 + offset;
        }
        // STATE 3
        if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
          LayerMask mask = 1 << LayerMask.NameToLayer("Paper") | 1 << LayerMask.NameToLayer("Background");
          RaycastHit hitInfo = new RaycastHit();
          bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, mask);
          if (hit)
          {
            Debug.Log(hitInfo.collider.gameObject.name);
            if (hitInfo.collider.gameObject.name == "Paper")
            {
              if (sController.SelectedStamp == "number")
              {
                // TELLS SET STAMP THAT THE NUMBER PANEL NEEDS TO BE SHOWN
                gameObject.SendMessage("ShowPanel");
                stampPoint = hitInfo;
              }
              else
              {
                readyToMoveStampToPaperAndBack = sController.setRaycastHit(hitInfo);
                gameObject.SendMessage("PrintStamp", hitInfo);
              }
              readyToCheckStamps = false;
            }
            else
            {
              needToResetStamps = true;
              readyToCheckStamps = false;
            }
            dragging = false;
            isStampScaled = false;
          }
        }
      }
    }

    // IF THE PAPER HAS NOT BEEN HIT, THE STAMPS NEED TO GO BACK TO THEIR DEFAULT POSITION AND SCALE
    if (needToResetStamps)
    {
      step += stampSpeed * Time.deltaTime;
      sController.MoveStampBackToRestPosition(step);
      if (step >= 1)
      {
        step = 0;
        needToResetStamps = false;
        readyToCheckStamps = true;
      }
    }

    //SWIPE WHEN STAMP IS READY, ABLE TO STAMP AGAIN WHEN ALREADY ABLE TO SWIPE
    if (readyToSwipePaper)
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

              if (swipeHValue > 0) //RIGHT SWIPE DETECTED
              {
                if (pController.createNewPaper())
                {
                  readyToCheckStamps = false;
                  readyToSwipePaper = false;
                }
                else
                {
                  islastQuestionReached = true;
                  readyToSwipePaper = false;
                  readyToCheckStamps = false;
                }
                gameObject.SendMessage("StartSendingAnswer"); // SENDS TO DATASENDER
                readyToMovePaper = true;
              }
            }
            break;
        }
      }
    }

    // WHEN LAST QUESTION IS REACHED, AN OTHER TYPE OF PANEL IS SHOWN WHICH NEEDS TO CHECK IF THE DONE SIGN HAS BEEN HIT
    if (islastQuestionReached)
    {
      if (Input.touchCount > 0)
      {
        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;
          if (touch.phase == TouchPhase.Ended)
          {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            if (Physics.Raycast(ray, out hit) && (hit.collider.tag == "doneSign"))
            {
            SceneManager.LoadScene("ProjectsScene");
            }
        }
      }
    }
  }

  // AS LONG THIS NOT GETS ACTIVATED BY SET STAMP, EVERYTHING IN GAMECONTROLLER IS PAUSED
  public void StopNumberInput()
  {
    readyToMoveStampToPaperAndBack = sController.setRaycastHit(stampPoint);
    gameObject.SendMessage("PrintStamp", stampPoint);
    readyToMoveStampToPaperAndBack = true;
  }
}
