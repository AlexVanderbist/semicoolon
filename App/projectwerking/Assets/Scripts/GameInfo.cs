using UnityEngine;
using System.Collections;

public class GameInfo : MonoBehaviour {

  private static string token;
  private static int currentProjectNumber;

  public int CurrentProjectNumber
  {
    get { return currentProjectNumber; }
    set { currentProjectNumber = value; }
  }

  public string Token
  {
    get { return token; }
    set { token = value; }
  }
}
