using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneController : MonoBehaviour {

  public void LoadScene(string sceneName)
  {
    SceneManager.LoadScene(sceneName);
  }
}
