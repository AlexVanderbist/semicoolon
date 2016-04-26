using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PaperController))]
[RequireComponent(typeof(StampController))]
public class MobileInput : MonoBehaviour {

  public float minSwipeDistY;
  public float minSwipeDistX;
  public float speed = 0.5f;

  private Vector2 startPos;
  private PaperController pController;
  private StampController sController;
  private bool moveObjects = false;
  private bool firstObject = false;
  private bool firstObjectCreated = false;
  private bool receiveInput = false;
  private bool readyToCheckStamps = false;
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
      if (!firstObjectCreated)
      {
        pController.SetBeginValues();
        pController.createNewPaper();
        firstObjectCreated = true;
        firstObject = true;
      }
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

      if (receiveInput)
      {
        if (Input.GetButtonDown("Jump"))
        {
          pController.createNewPaper();
          moveObjects = true;
          receiveInput = false;
        }
      }

      if (moveObjects)
        {
          step += speed * Time.deltaTime;
          pController.moveNewPaper(step);
          pController.moveFocusPaper(step);
          if (step >= 1)
          {
            moveObjects = false;
            step = 0;
            readyToCheckStamps = true;
          }
        }

      if (readyToCheckStamps)
      {
        bool readyChecking = false;
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
        readyChecking = sController.CheckHit(hitInfo.collider.gameObject.name);
        Debug.Log(hitInfo.collider.gameObject.name);
        if (Input.touchCount > 0)
        {
          Touch touch = Input.touches[0];
          
          //bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(touch.position), out hitInfo);
          if (hit)
          {
              readyChecking = sController.CheckHit(hitInfo.collider.gameObject.name);
            if (readyChecking)
            {
              pController.DestroyCurrentPaper();
              pController.setCurrentPaper();
              receiveInput = true;
              readyToCheckStamps = false;
            }
          }
         

          }
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
