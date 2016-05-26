using UnityEngine;
using System.Collections;
using LitJson;

public class TokenReceiver : MonoBehaviour {

  // THIS CLASS IS USED TO GET A NEW TOKEN WHEN THE OLD ONE EXPIRED

  public string LoginUrl = "http://semicolon.multimediatechnology.be/api/v1/authenticate";
  string email = "";
  string password = "";

  private string invalidString = "invalid_credentials";

  JsonData textdata;
  GameInfo GI;

  void Start()
  {
    GI = GameObject.Find("GameData").GetComponent<GameInfo>();
  }


  public void StartReceivingNewToken(string methodToActivate)
  {
    StartCoroutine(ReceiveToken(methodToActivate));
  }

  IEnumerator ReceiveToken(string methodToActivate)
  {
    GI.Token = null;
    WWWForm Form = new WWWForm();

    Form.AddField("email", PlayerPrefs.GetString("username"));
    Form.AddField("password", PlayerPrefs.GetString("password"));

    WWW LoginAccountWWW = new WWW(LoginUrl, Form);

    yield return LoginAccountWWW;

    if (LoginAccountWWW.error == null)
    {
      textdata = JsonMapper.ToObject(LoginAccountWWW.text);

      //SAVE TOKEN
      if (textdata["token"].ToString() != "")
      {
        GI.Token = textdata["token"].ToString();
        gameObject.SendMessage(methodToActivate);
        Debug.Log("new token is:" + GI.Token);
      }
      //CHECK ERROR STRING
      else if (textdata[0]["error"].ToString() == invalidString)
      {
        Debug.Log("fout email of pass");
      }
    }
    //ERROR! CANT LOGIN
    else
    {
      Debug.LogError("Cannot connect to Login");
      Debug.Log(LoginAccountWWW.error.ToString());
    }
  }
}
