using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProjectPanelController : MonoBehaviour {

  public GameObject panelDone, panelToDo;
  public Button buttonToDo, buttonDone;
  public string hexStringBlue, hexStringGrey;
  bool panelToDoActive = true;
  Color blueColor = new Color();
  Color greyColor = new Color();

  void Start()
  {
    ColorUtility.TryParseHtmlString(hexStringBlue, out blueColor);
    ColorUtility.TryParseHtmlString(hexStringGrey, out greyColor);
  }

  public void ChangePanel()
  {
    if (panelToDoActive)
    {
      buttonDone.image.color = blueColor;
      buttonToDo.image.color = greyColor;
      buttonDone.GetComponentInChildren<Text>().color = Color.white;
      buttonToDo.GetComponentInChildren<Text>().color = Color.black;
      panelToDo.SetActive(false);
      panelDone.SetActive(true);
      panelToDoActive = false;
    }
    else
    {
      buttonToDo.image.color = blueColor;
      buttonDone.image.color = greyColor;
      buttonDone.GetComponentInChildren<Text>().color = Color.black;
      buttonToDo.GetComponentInChildren<Text>().color = Color.white;
      panelToDo.SetActive(true);
      panelDone.SetActive(false);
      panelToDoActive = true;
    }
  }

}
