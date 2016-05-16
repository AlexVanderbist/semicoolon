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
  private bool movePapers = false;
  private bool firstPaper = false;
  private bool firstPaperCreated = false;
  private bool receiveInput = false;
  private bool readyToCheckStamps = false;
  private bool readyToMoveStampToPaper = false;
  private bool readyWithStampToPaper = false;
  private bool stampSelected = false;

  //MAIN VARIABLE TO MOVE THINGS, GETS RESET EVERYTIME IT REACHES 1
  private float step = 0f;

  //DRAGING OBJECTS
  private float dist;
  private bool dragging = false;
  private Vector3 offset;
  private Transform toDrag;

  // Use this for initialization
  void Start () {
    pController = GetComponent<PaperController>();
    sController = GetComponent<StampController>();
    StartCoroutine(Loop());
	}

  // Update is called once per frame
  IEnumerator Loop()
  {
    while (true)
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
              if (sController.SelectedStamp == "number")
              {
                Debug.Log("hej");
              }
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

        //DRAG STAMPS
        if (readyToCheckStamps)
        {
          Vector3 v3;

          if (Input.touchCount != 1)
          {
            dragging = false;
            yield return null;
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
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
              if (hitInfo.collider.gameObject.name == "Paper")
              {
                readyToMoveStampToPaper = sController.CheckPaper(hitInfo);
                readyToCheckStamps = false;
              }
              dragging = false;
            }
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
}
