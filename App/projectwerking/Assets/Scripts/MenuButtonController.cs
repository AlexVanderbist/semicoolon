using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour {

  public GameObject MenuContainer;
  public float maxHeightShown = 300f;
  Vector3 MenuContainerSlided, MenuRestPosition;

  bool menuIsShown = false, moveMenu = false;
  float step = 0;
  float speed = 1.5f;

  void Start()
  {
    MenuRestPosition = MenuContainer.transform.position;
    MenuContainerSlided = new Vector3(MenuContainer.transform.position.x, MenuContainer.transform.position.y - maxHeightShown, MenuContainer.transform.position.z);
  }

  public void ShowMenu()
  {
    if (menuIsShown)
    {
      moveMenu = true;
      menuIsShown = false;
    }
    else
    {
      moveMenu = true;
      menuIsShown = true;
    }
  }

  void Update()
  {
    if (moveMenu)
    {
      if (menuIsShown)
      {
        step += speed * Time.deltaTime;
        MenuContainer.transform.position = Vector3.Lerp(MenuContainer.transform.position, MenuContainerSlided, step);
        if (step > 1)
        {
          step = 0;
          moveMenu = false;
        }
      }
      else if (!menuIsShown)
      {
        step += speed * Time.deltaTime;
        MenuContainer.transform.position = Vector3.Lerp(MenuContainer.transform.position, MenuRestPosition, step);
        if (step > 1)
        {
          step = 0;
          moveMenu = false;
        }
      }
    }
    
  }
}
