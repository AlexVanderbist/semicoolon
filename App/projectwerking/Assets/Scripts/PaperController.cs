using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PaperController : MonoBehaviour {

  public GameObject paper;
  public Text paperText;
  public Transform startposition, focusposition, endposition;

  Rigidbody currentPaper;
  int currentQuestionNr = 0;
  string[] paperTextArray;
  
	// Use this for initialization
	void Start () {
    paperText.text = paperTextArray[0];
    currentPaper = (Rigidbody)Instantiate(paper, focusposition.position, transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
	  
	}

  void setNextPaperText() {
    currentQuestionNr++;
    paperText.text = paperTextArray[currentQuestionNr];
  }

  void setPreviousPaperText() {
    currentQuestionNr--;
    paperText.text = paperTextArray[currentQuestionNr];
  }

  public void SwipeNext() {

  }

  public void SwipePrevious() {

  }
}
