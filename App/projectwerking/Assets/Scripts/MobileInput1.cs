using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PaperController))]
[RequireComponent(typeof(StampController))]
public class MobileInput1 : MonoBehaviour {

  public float minSwipeDistY;
  public float minSwipeDistX;
  public float speed = 0.5f;
  public float stampSpeed = 0.7f;

  private Vector2 startPos;
  private PaperController pController;
  private StampController sController;

  //ALL BOOLEANS THAT CONTROL THE INPUT AND MOVEMENT OF OBJECTS
  private bool movePapers = false;
  private bool firstPaper = false;
  private bool firstPaperCreated = false;
  private bool receiveInput = false;
  private bool readyToCheckStamps = false;
  private bool readyToMoveStampToPaper = false;
  private bool readyWithStampToPaper = false;
  private bool stampSelected = false;
  private bool extraStampAnimationReady = false;

  //MAIN VARIABLE TO MOVE THINGS, GETS RESET EVERYTIME IT REACHES 1
  private float step = 0f;
  private float extraStep = 0f;

  //DRAGING OBJECTS
  private float dist;
  private bool dragging = false;
  private Vector3 offset;
  private Transform toDrag;

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
        pController.SetBeginValues();
        pController.createNewPaper();
        firstPaperCreated = true;
        firstPaper = true;
      }

      //Move First Object
      if (firstPaper)
      {
        step += speed * Time.deltaTime;
        pController.moveNewPaper(step);
        pController.setCurrentPaper();

        if (step >= 1f)
        {
          firstPaper = false;
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
          movePapers = true;
          receiveInput = false;
        }
      }

      //Move New and Focused Paper
      if (movePapers)
      {
        step += speed * Time.deltaTime;
        pController.moveNewPaper(step);
        pController.moveFocusPaper(step);
        if (step >= 1)
        {
          movePapers = false;
          step = 0;
          pController.DestroyCurrentPaper();
          pController.setCurrentPaper();
          readyToCheckStamps = true;
        }
      }

      //Move selected stamp when paper has been hit
      if (readyToMoveStampToPaper)
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
            receiveInput = true;
            readyToMoveStampToPaper = false;
            readyWithStampToPaper = false;
          }
        }
      }

      //  //Evrything that checks and handles the stamps
      //  if (readyToCheckStamps)
      //  {
      //    RaycastHit hitInfo = new RaycastHit();
      //    bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
      //    if (Input.touchCount > 0)
      //    {
      //      if (stampSelected)
      //      {
      //        if (hitInfo.collider.gameObject.name == "Paper")
      //        {
      //          readyToMoveStampToPaper = sController.CheckPaper(hitInfo.collider.gameObject);
      //        }
      //      }

      //      if (hit && !readyToMoveStampToPaper)
      //      {
      //        stampSelected = sController.CheckStamp(hitInfo.collider.gameObject.name);
      //      }

      //      if (readyToMoveStampToPaper)
      //      {
      //        readyToCheckStamps = false;
      //        stampSelected = false;
      //      }
      //    }
      //  }

      //DRAG STAMPS
      if (readyToCheckStamps)
      {
        Vector3 v3;

        if (Input.touchCount != 1)
        {
          dragging = false;
          return;
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
          LayerMask mask = 1 << LayerMask.NameToLayer("Paper");
          RaycastHit hitInfo = new RaycastHit();
          bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, Mathf.Infinity, mask);
          if (hitInfo.collider.gameObject.name == "Paper")
          {
            gameObject.SendMessage("PrintStamp");
            stampSelected = sController.CheckStamp(toDrag.name);
            readyToMoveStampToPaper = sController.setRaycastHit(hitInfo);
            readyToCheckStamps = false;
          }
          else
          {
            sController.resetStamps();
          }

          

          dragging = false;
        }
      }
    }

    //RightSwipe when stampController is ready
    if (receiveInput)
    {
      //OLDCODE
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
                movePapers = true;
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
