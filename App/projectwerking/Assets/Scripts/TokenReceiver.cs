using UnityEngine;
using System.Collections;
using LitJson;

public class TokenReceiver : MonoBehaviour {

  // THIS CLASS IS USED TO GET A NEW TOKEN WHEN THE OLD ONE EXPIRED

  public string loginUrl = "http://semicolon.multimediatechnology.be/api/v1/authenticate";

  //private string email = "";
  private string password = "";
  private string invalidString = "invalid_credentials";

  JsonData textData;
  GameInfo GI;

  void Start()
  {
    GI = GameObject.Find("GameData").GetComponent<GameInfo>();
  }

  //LISTENS TO DATA SENDER OR DATA OBTAINER
  public void StartReceivingNewToken(string methodToActivate)
  {
    StartCoroutine(ReceiveToken(methodToActivate));
  }

  IEnumerator ReceiveToken(string methodToActivate)
  {
    GI.Token = null;
    WWWForm Form = new WWWForm();

    Form.AddField("email", GI.Email);
    Form.AddField("password", GI.Password);

    WWW LoginAccountWWW = new WWW(loginUrl, Form);

    yield return LoginAccountWWW;

    if (LoginAccountWWW.error == null)
    {
      textData = JsonMapper.ToObject(LoginAccountWWW.text);

      //SAVE TOKEN
      if (textData["token"].ToString() != "")
      {
        GI.Token = textData["token"].ToString();
        gameObject.SendMessage(methodToActivate);
        Debug.Log("new token is:" + GI.Token);
      }
      //CHECK ERROR STRING
      else if (textData[0]["error"].ToString() == invalidString)
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
