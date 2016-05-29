using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

  // USED SO GAME DATA IS KNOWN IN ALL SCENES

  private static DontDestroy instance;
  void Awake()
  {
    DontDestroyOnLoad(gameObject);
   
    if (instance == null)
    {
      instance = this;
    }
    else {
      DestroyObject(gameObject);
    }
  }
}
