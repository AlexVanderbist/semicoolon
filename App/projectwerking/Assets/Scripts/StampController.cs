 using UnityEngine;
using System.Collections;

public class StampController : MonoBehaviour {

  public GameObject redStamp, greenStamp;
  Vector3 restStateRedStamp, restStateGreenStamp;
	// Use this for initialization
	void Start () {
    restStateRedStamp = redStamp.transform.position;
    restStateGreenStamp = greenStamp.transform.position;
	}

  public bool CheckHit(string objectToMove) {
    bool readyChecking = false;
      if (objectToMove == "RedStamp")
      {
        redStamp.transform.position = new Vector3(redStamp.transform.position.x, redStamp.transform.position.y + 20, redStamp.transform.position.z);
        greenStamp.transform.position = restStateGreenStamp;
        readyChecking = true;
      }
      else if (objectToMove == "GreenStamp")
      {
        greenStamp.transform.position = new Vector3(greenStamp.transform.position.x, greenStamp.transform.position.y + 20, greenStamp.transform.position.z);
        redStamp.transform.position = restStateRedStamp;
        readyChecking = true;
      }
      else if (objectToMove != "GreenStamp" && objectToMove != "RedStamp")
      {
        greenStamp.transform.position = restStateGreenStamp;
        redStamp.transform.position = restStateRedStamp;
        readyChecking = false;
      }
    return readyChecking;
  }
}
