using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour {

  public GameObject menuContainer;
  public float maxHeightShown;
  Vector3 menuContainerSlided, menuRestPosition;

  bool isMenuShown = false, isMenuReadyToMove = false;
  float step = 0;
  float speed = 1.5f;

  void Start()
  {
    menuRestPosition = menuContainer.transform.position;
    menuContainerSlided = new Vector3(menuContainer.transform.position.x, menuContainer.transform.position.y - maxHeightShown, menuContainer.transform.position.z);
  }

  public void ShowMenu()
  {
    if (isMenuShown)
    {
      isMenuReadyToMove = true;
      isMenuShown = false;
    }
    else
    {
      isMenuReadyToMove = true;
      isMenuShown = true;
    }
  }

  void Update()
  {
    if (isMenuReadyToMove)
    {
      if (isMenuShown)
      {
        step += speed * Time.deltaTime;
        menuContainer.transform.position = Vector3.Lerp(menuContainer.transform.position, menuContainerSlided, step);
        if (step > 1)
        {
          step = 0;
          isMenuReadyToMove = false;
        }
      }
      else if (!isMenuShown)
      {
        step += speed * Time.deltaTime;
        menuContainer.transform.position = Vector3.Lerp(menuContainer.transform.position, menuRestPosition, step);
        if (step > 1)
        {
          step = 0;
          isMenuReadyToMove = false;
        }
      }
    }
    
  }
}
