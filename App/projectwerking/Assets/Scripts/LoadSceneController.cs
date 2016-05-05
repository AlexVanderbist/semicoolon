using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneController : MonoBehaviour {

  public void LoadScene(string scenename)
  {
    SceneManager.LoadScene(scenename);
  }
}
