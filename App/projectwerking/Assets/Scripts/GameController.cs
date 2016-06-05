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
  public float speedMultiplier = 1.5f;
  public GameObject[] numberHoverPrefabs;

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
  private bool readyToMoveNumberPaper = false;
  private bool readyToRemoveNumberPaper = false;
  private bool isNumberStampSelected = false;
  private bool isPaperException = false;

  // MAIN VARIABLE TO MOVE THINGS, GETS RESET EVERYTIME IT REACHES 1
  // USED FOR LERPS 
  private float step = 0f;
  private float dragStep = 0f; // EXTRA SIDE STEPS BECAUSE OF CONFLICTS WITH MAIN STEP
  private float numberPaperStep = 0f;

  //DRAGING OBJECTS
  private float dist;
  private bool dragging = false;
  private Vector3 offset;
  private Transform toDrag;

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
        pController.CreateNewPaper();
        firstPaperCreated = true;
        isFirstPaper = true;
        sController.DeActivateStamps();
        sController.HideFirstTime();
      }

      //MOVE FIRST OBJECT
      if (isFirstPaper)
      {
        step += paperSpeed * Time.deltaTime * speedMultiplier;
        pController.MoveNewPaper(step, "normalPaper");
        pController.SetCurrentPaper();

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
        step += paperSpeed * Time.deltaTime * speedMultiplier;
        pController.MoveNewPaper(step, "normalPaper");
        pController.MoveFocusPaper(step, "currentPaper");
        if (!islastQuestionReached)
        {
          sController.HideUnusedStamps(step);
        }
        if (step >= 1)
        {
          readyToMovePaper = false;
          step = 0;
          pController.DestroyCurrentPaper();
          pController.SetCurrentPaper();
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
          step += stampSpeed * Time.deltaTime * speedMultiplier;
          sController.MoveStampToPaper(step);
          if (step >= 1)
          {
            step = 0;
            readyWithStampToPaper = true;
          }
        }
        else
        {
          step += stampSpeed * Time.deltaTime * speedMultiplier;
          sController.MoveStampBackToRestPosition(step);
          if (step >= 1)
          {
            step = 0;
            readyToSwipePaper = true;
            readyToCheckStamps = true;
            readyToMoveStampToPaperAndBack = false;
            readyWithStampToPaper = false;
            if (isNumberStampSelected)
            {
              readyToRemoveNumberPaper = true;
            }
          }
        }
      }

      if (isNumberStampSelected)
      {
        if (readyToMoveNumberPaper)
        {
          step += paperSpeed * Time.deltaTime * speedMultiplier;
          pController.MoveNewPaper(step, "numberPaper");
          if (step >= 1)
          {
            step = 0;
            readyToMoveNumberPaper = false;
          }
        }

        if (readyToRemoveNumberPaper)
        {
          numberPaperStep += paperSpeed * Time.deltaTime * speedMultiplier;
          pController.MoveFocusPaper(numberPaperStep, "numberPaper");
          if (numberPaperStep >= 1)
          {
            numberPaperStep = 0;
            readyToRemoveNumberPaper = false;
            isNumberStampSelected = false;
            pController.DestroyCurrentNumberPaper();
          }
        }
      }

      if (dragging && !readyToRemoveNumberPaper &&!isPaperException)
      {
        if (sController.SelectedStamp == "number")
        {
          LayerMask mask = 1 << LayerMask.NameToLayer("Paper");
          RaycastHit hitInfo = new RaycastHit();
          bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, mask);
          if (hit)
          {
            bool isInteger = false;
            int numberName = 0;
            isInteger = int.TryParse(hitInfo.collider.gameObject.name, out numberName);
            if (isInteger && 1 <= numberName && numberName <= 5)
            {
              for (int i = 0; i < numberHoverPrefabs.Length; i++)
              {

                if (numberHoverPrefabs[i].name == ("Hover" + hitInfo.collider.gameObject.name))
                {
                  numberHoverPrefabs[i].SetActive(true);
                  pController.GetCurrentNumberPaper.transform.FindChild(numberHoverPrefabs[i].name).gameObject.SetActive(true);
                }
                else
                {
                  numberHoverPrefabs[i].SetActive(false);
                  pController.GetCurrentNumberPaper.transform.FindChild(numberHoverPrefabs[i].name).gameObject.SetActive(false);
                }
              }
            }
            else
            {
              for (int i = 0; i < numberHoverPrefabs.Length; i++)
              {
                numberHoverPrefabs[i].SetActive(false);
                pController.GetCurrentNumberPaper.transform.FindChild(numberHoverPrefabs[i].name).gameObject.SetActive(false);
              }
            }
          }
        }
      }

      //DRAG STAMPS
      if (readyToCheckStamps)
      {
        if (Input.touchCount != 1)
        {
          dragging = false;
          return;
        }

        Vector3 v3;
        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        // STATE 1
        if (touch.phase == TouchPhase.Began)
        {
          
          RaycastHit hit;
          Ray ray = Camera.main.ScreenPointToRay(pos);
          if (Physics.Raycast(ray, out hit) && (hit.collider.tag == "Stamp"))
          {
            isPaperException = false;
            toDrag = hit.transform;
            dist = hit.transform.position.z - Camera.main.transform.position.z;
            v3 = new Vector3(pos.x, pos.y, dist);
            v3 = Camera.main.ScreenToWorldPoint(v3);
            offset = toDrag.position - v3;
            dragging = true;
            readyToSwipePaper = false;
            sController.CheckStamp(toDrag.name);
            if (sController.SelectedStamp == "number" && !readyToRemoveNumberPaper)
            {
              pController.CreateNumbersPaper();
              readyToMoveNumberPaper = true;
              isNumberStampSelected = true;
            }
          }

          if (Physics.Raycast(ray, out hit) && (hit.collider.tag == "Paper") && readyToSwipePaper)
          {
            isPaperException = true;
            startPos = touch.position;
            toDrag = hit.transform.parent;
            pController.SetBeginPoint(toDrag);
            dist = hit.transform.position.z - Camera.main.transform.position.z;
            v3 = new Vector3(pos.x, pos.y, dist);
            v3 = Camera.main.ScreenToWorldPoint(v3);
            offset = toDrag.position - v3;
            dragging = true;
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
          if (!isPaperException)
          {
            LayerMask mask = 1 << LayerMask.NameToLayer("Paper") | 1 << LayerMask.NameToLayer("Background");
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, mask);
            if (hit)
            {
              if (sController.SelectedStamp != "number")
              {
                if (hitInfo.collider.gameObject.name == "Paper")
                {
                  readyToMoveStampToPaperAndBack = sController.setRaycastHit(hitInfo);
                  gameObject.SendMessage("PrintStamp", hitInfo);
                }
                else
                {
                  needToResetStamps = true;
                }
              }
              else
              {
                bool isInteger = false;
                int numberName = 0;
                isInteger = int.TryParse(hitInfo.collider.gameObject.name, out numberName);
                if (isInteger && 1 <= numberName && numberName <= 5)
                {
                  readyToMoveStampToPaperAndBack = sController.setRaycastHit(hitInfo);
                  gameObject.SendMessage("PrintStamp", hitInfo);
                }
                else
                {
                  needToResetStamps = true;
                  readyToRemoveNumberPaper = true;
                }
              }
            }
            else
            {
              needToResetStamps = true;
            }
            readyToCheckStamps = false;
            dragging = false;
            isStampScaled = false;
          }
          else
          {
            float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
            float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
            pController.SetEndPointPaper(toDrag);
            if (swipeDistHorizontal > minSwipeDistX && swipeDistVertical > minSwipeDistY)
            {
              float swipeHValue = Mathf.Sign(touch.position.x - startPos.x);
              float swipeVValue = Mathf.Sign(touch.position.y - startPos.y);

              if (swipeHValue > 0 || swipeVValue > 0 || swipeHValue < 0 || swipeVValue < 0)  //RIGHT SWIPE DETECTED
              {
                if (pController.CreateNewPaper())
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
          }  
        }

        // SCALE WHILE DRAGGING
        if (dragging && !isStampScaled && !isPaperException)
        {
          dragStep += 0.5f * Time.deltaTime;
          sController.ScaleStamp(dragStep);
          if (dragStep >= 1)
          {
            dragStep = 0;
            isStampScaled = true;
          }
        }
      }
    }

    // IF THE PAPER HAS NOT BEEN HIT, THE STAMPS NEED TO GO BACK TO THEIR DEFAULT POSITION AND SCALE
    if (needToResetStamps)
    {
      step += stampSpeed * Time.deltaTime * speedMultiplier;
      sController.MoveStampBackToRestPosition(step);
      if (step >= 1)
      {
        step = 0;
        needToResetStamps = false;
        readyToCheckStamps = true;
      }
    }

    // WHEN LAST QUESTION IS REACHED, AN OTHER TYPE OF PANEL IS SHOWN WHICH NEEDS TO CHECK IF THE DONE SIGN HAS BEEN HIT
    if (islastQuestionReached)
    {
      if (dragStep <= 1)
      {
        dragStep *= stampSpeed * Time.deltaTime * speedMultiplier;
        sController.hideAllStamps(dragStep);
      }

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
}
