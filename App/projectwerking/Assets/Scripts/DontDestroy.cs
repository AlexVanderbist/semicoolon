using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

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
