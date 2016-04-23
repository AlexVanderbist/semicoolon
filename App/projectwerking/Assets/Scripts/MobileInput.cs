using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PaperController))]
public class MobileInput : MonoBehaviour {

  public float minSwipeDistY;
  public float minSwipeDistX;

  private Vector2 startPos;
  private PaperController pController;

  // Use this for initialization
  void Start () {
	  pController = GetComponent<PaperController>();
    pController.setFirstPaper();
	}
	
	// Update is called once per frame
	void Update () {
    if (Input.GetButtonDown("Jump")) {
      pController.SwipeNext();
      Debug.Log("Spawn");
    }

    if (Input.touchCount > 0)
    {
      Touch touch = Input.touches[0];

      switch (touch.phase)
      {

        case TouchPhase.Began:

          startPos = touch.position;
          break;

          case TouchPhase.Ended:
          /*
          float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;

          if (swipeDistVertical > minSwipeDistY)
          {

            float swipeVertValue = Mathf.Sign(touch.position.y - startPos.y);

            if (swipeVertValue > 0)//Up SWIPE
            {
              
            }
            else if (swipeVertValue < 0)//down swipe
            {
              
            }
          }*/

          float swipeDistHorizontal = (new Vector3(touch.position.x, 0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;

          if (swipeDistHorizontal > minSwipeDistX)
          {

            float swipeHValue = Mathf.Sign(touch.position.x - startPos.x);

            if (swipeHValue > 0)//right swipe
            {
              pController.SwipeNext();

            }

            else if (swipeHValue < 0)//left swipe
            {
              

            }
          }
          break;
      }
    }
  }


}
