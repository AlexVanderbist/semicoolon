using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PaperController))]
[RequireComponent(typeof(StampController))]
public class GameController : MonoBehaviour {

  public float minSwipeDistY;
  public float minSwipeDistX;
  public float paperSpeed = 0.8f;
  public float stampSpeed = 0.7f;

  private Vector2 startPos;
  private PaperController pController;
  private StampController sController;

  //ALL BOOLEANS THAT CONTROL THE INPUT AND MOVEMENT OF OBJECTS
  private bool readyToMovePaper = false;
  private bool firstPaper = false;
  private bool firstPaperCreated = false;
  private bool readyToSwipePaper = false;
  private bool readyToCheckStamps = false;
  private bool readyToMoveStampToPaperAndBack = false;
  private bool readyWithStampToPaper = false;
  private bool lastQuestionReached = false;
  private bool inNeedForNumberInput = false;
  private bool numberIsSet = false;
  private bool stampIsScaled = false;

  //MAIN VARIABLE TO MOVE THINGS, GETS RESET EVERYTIME IT REACHES 1
  private float step = 0f;
  private float dragStep = 0f; // EXTRA STEP BECAUSE OF CONFLICTS

  //DRAGING OBJECTS
  private float dist;
  private bool dragging = false;
  private Vector3 offset;
  private Transform toDrag;
  private RaycastHit stampPoint; // Used to remember stamppoint if there is a need of number input.

  // Use this for initialization
  void Start () {
    pController = GetComponent<PaperController>();
    sController = GetComponent<StampController>();
	}

  // Update is called once per frame
  void Update()
  {
    //Wait till questions are loaded
    if (pController.ListIsReady)
    {
      //Spawn First Object
      if (!firstPaperCreated)
      {
        pController.createNewPaper();
        firstPaperCreated = true;
        firstPaper = true;
        sController.DeActivateStamps();
      }

      //Move First Object
      if (firstPaper)
      {
        step += paperSpeed * Time.deltaTime;
        pController.moveNewPaper(step);
        pController.setCurrentPaper();

        if (step >= 1f)
        {
          firstPaper = false;
          step = 0f;
          readyToCheckStamps = true;
        }
      }

      //Move New and Focused Paper
      if (readyToMovePaper)
      {
        step += paperSpeed * Time.deltaTime;
        pController.moveNewPaper(step);
        pController.moveFocusPaper(step);
        if (step >= 1)
        {
          readyToMovePaper = false;
          step = 0;
          pController.DestroyCurrentPaper();
          pController.setCurrentPaper();
          readyToCheckStamps = true;
        }
      }

      //Move selected stamp when paper has been hit
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

        if (dragging && !stampIsScaled)
        {
          dragStep += 0.5f * Time.deltaTime;
          sController.ScaleStamp(dragStep);
          if (dragStep >= 1)
          {
            dragStep = 0;
            stampIsScaled = true;
          }
        }

        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

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
        if (dragging && touch.phase == TouchPhase.Moved)
        {
          v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
          v3 = Camera.main.ScreenToWorldPoint(v3);
          toDrag.position = v3 + offset;
        }
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
                inNeedForNumberInput = true;
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
              sController.ResetStamps();
            }
            dragging = false;
            stampIsScaled = false;
          }
        }
      }
    }

    //RightSwipe when stampController is ready
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

              if (swipeHValue > 0)//right swipe
              {
                if (pController.createNewPaper())
                {
                  sController.DeActivateStamps();
                  readyToCheckStamps = false;
                  readyToSwipePaper = false;
                }
                else
                {
                  lastQuestionReached = true;
                  readyToSwipePaper = false;
                  readyToCheckStamps = false;
                }
                gameObject.SendMessage("StartSendingAnswer");
                readyToMovePaper = true;
              }
            }
            break;
        }
      }
    }

    if (inNeedForNumberInput)
    {
      gameObject.SendMessage("ShowPanel");
      inNeedForNumberInput = false;
    }

    if (numberIsSet)
    {
      readyToMoveStampToPaperAndBack = sController.setRaycastHit(stampPoint);
      gameObject.SendMessage("PrintStamp", stampPoint);
      readyToMoveStampToPaperAndBack = true;
      numberIsSet = false;
    }

    if (lastQuestionReached)
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

  public void StopNumberInput()
  {
    Debug.Log("stop number input");
    numberIsSet = true;
  }
}
