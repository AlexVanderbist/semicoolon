using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PaperController))]
public class MobileInput : MonoBehaviour {

  public float minSwipeDistY;
  public float minSwipeDistX;
  public float speed = 0.5f;

  private Vector2 startPos;
  private PaperController pController;
  private bool moveObjects = false;
  private bool firstObject = false;
  private bool firstObjectCreated = false;
  private bool receiveInput = false;
  private float step = 0f;

  // Use this for initialization
  void Start () {
    pController = GetComponent<PaperController>();
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
          receiveInput = true;
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
            pController.DestroyCurrentPaper();
            pController.setCurrentPaper();
            receiveInput = true;
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
