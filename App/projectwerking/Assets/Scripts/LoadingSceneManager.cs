using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour {

    public GameObject LoadingScene;

    public void LoadLevel()
    {
        StartCoroutine(LevelCoroutine());
    }
    IEnumerator LevelCoroutine()
    {
        LoadingScene.SetActive(true);
        AsyncOperation async = Application.LoadLevelAsync(2);
        yield return null;
    }
}
