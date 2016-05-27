using UnityEngine;
using System.Collections;

public class WebpageLoader : MonoBehaviour
{
  public void LoadWebpage(string url)
  {
    Application.OpenURL(url);
  }
}
