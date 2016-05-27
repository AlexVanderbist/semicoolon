﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProjectPanelController : MonoBehaviour {

  public GameObject panelDone, panelToDo, panelProfile, buttonToDoGO, buttonDoneGO;
  public Button buttonToDo, buttonDone, buttonUserBack;
  public string hexStringBlue, hexStringGrey;
  public Sprite iconBack, iconUser;
  public Text titleText;

  bool isPanelToDoActive = true, isProfilePanelShown = false;
  Color blueColor = new Color();
  Color greyColor = new Color();

  void Start()
  {
    ColorUtility.TryParseHtmlString(hexStringBlue, out blueColor);
    ColorUtility.TryParseHtmlString(hexStringGrey, out greyColor);
  }

  // set profile panel active and other panels off
  public void ShowProfile()
  {
    if (isProfilePanelShown)
    {
      if (isPanelToDoActive)
      {
        panelToDo.SetActive(true);
      }
      else
      {
        panelDone.SetActive(true);
      }
      panelProfile.SetActive(false);
      isProfilePanelShown = false;
      buttonDoneGO.SetActive(true);
      buttonToDoGO.SetActive(true);
      buttonUserBack.GetComponent<Image>().sprite = iconUser;
      titleText.text = "Projecten";
    }
    else
    {
      if (isPanelToDoActive)
      {
        panelToDo.SetActive(false);
      }
      else
      {
        panelDone.SetActive(false);
      }
      panelProfile.SetActive(true);
      isProfilePanelShown = true;
      buttonDoneGO.SetActive(false);
      buttonToDoGO.SetActive(false);
      buttonUserBack.GetComponent<Image>().sprite = iconBack;
      titleText.text = "Profiel";
    }
  }

  public void ChangePanel()
  {
    if (isPanelToDoActive)
    {
      buttonDone.image.color = blueColor;
      buttonToDo.image.color = greyColor;
      buttonDone.GetComponentInChildren<Text>().color = Color.white;
      buttonToDo.GetComponentInChildren<Text>().color = Color.black;
      panelToDo.SetActive(false);
      panelDone.SetActive(true);
      isPanelToDoActive = false;
    }
    else
    {
      buttonToDo.image.color = blueColor;
      buttonDone.image.color = greyColor;
      buttonDone.GetComponentInChildren<Text>().color = Color.black;
      buttonToDo.GetComponentInChildren<Text>().color = Color.white;
      panelToDo.SetActive(true);
      panelDone.SetActive(false);
      isPanelToDoActive = true;
    }
  }

}