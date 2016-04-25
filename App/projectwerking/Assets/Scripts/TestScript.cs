using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestScript : MonoBehaviour {

  public GameObject cubeTestPrefab;
  public float speed;
  List<GameObject> list = new List<GameObject>();
  public Transform focusposition, startposition, endposition;
  GameObject currentCube, newCube;
  float step;

  bool moveObjects = false;


	// Use this for initialization
	void Start () {
    moveNewCube();
	}
	
	// Update is called once per frame
	void Update () {
    if (Input.GetButtonDown("Jump")) {
      createNewCube();
      moveObjects = true;
    }

    if (moveObjects) {
      step = speed * Time.deltaTime;
      moveCurrentCube();
      moveNewCube();
      if (step > 1) {
        moveObjects = false;
        step = 0;
        Destroy(currentCube);

      }
    }
	}

  void moveCurrentCube() {
    currentCube.transform.localPosition = Vector3.Lerp(currentCube.transform.position, endposition.position, step);
  }

  void createNewCube() {
    newCube = (GameObject)Instantiate(cubeTestPrefab, startposition.position, startposition.rotation);
    currentCube = newCube;
  }

  void moveNewCube() {
    newCube.transform.position = Vector3.Lerp(newCube.transform.position, focusposition.position, step);
  }
}
