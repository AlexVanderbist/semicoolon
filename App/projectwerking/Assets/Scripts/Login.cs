using UnityEngine;
using System.Collections;
using LitJson;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(GameInfo))]
public class Login : MonoBehaviour {

    string email = "";
    string password = "";
    public Text errorMessage;
    public string sceneToLoad = "ProjectsScene";
    public string CreateAccountUrl = "http://semicolon.multimediatechnology.be";
    public string LoginUrl = "http://semicolon.multimediatechnology.be/api/v1/authenticate";

    public InputField inputEmail, inputPassword;

    private string invalidString = "invalid_credentials";

    JsonData textdata;
    GameInfo GI;
    public Toggle rememberMeCheckbox;

	// Use this for initialization
	void Start () {
        GI = GetComponent<GameInfo>();
        errorMessage.enabled = false;
        if (PlayerPrefs.HasKey("username") && PlayerPrefs.HasKey("password"))
        {
            email = PlayerPrefs.GetString("username");
            inputEmail.text = email;
            password = PlayerPrefs.GetString("password");
            inputPassword.text = password;
            Debug.Log(email + " " + password);
        }
        else
        {
            Debug.Log("can't find keys");
        }
	}


  public void GoToCreateAccount(bool createAccount) {
    if (createAccount)
    {
      LinkToCreateAccountURL();
    }
  }

  public void SetEmail(string initEmail)
  {
    email = initEmail;
  }

  public void SetPassword(string initPassword)
  {
    password = initPassword;
  }

  public void LoginOnClick(bool initLogin)
  {
    if (initLogin && email != "" && password != "")
    {
      StartCoroutine(LoginAccount());
      errorMessage.enabled = false;
    }
    else
    {
      errorMessage.text = "Vul beide velden in";
      errorMessage.enabled = true;
    }

  }

  IEnumerator LoginAccount() {

        WWWForm Form = new WWWForm();

        Form.AddField("email", email);
        Form.AddField("password", password);

        WWW LoginAccountWWW = new WWW(LoginUrl, Form);

        yield return LoginAccountWWW;

    if (LoginAccountWWW.error == null)
    {
      textdata = JsonMapper.ToObject(LoginAccountWWW.text);

      //SAVE TOKEN
      if (textdata["token"].ToString() != "")
      {
          if (rememberMeCheckbox.isOn)
          {
              PlayerPrefs.SetString("username", email);
              PlayerPrefs.SetString("password", password);
          }
          errorMessage.enabled = false;
          GI.Token = textdata["token"].ToString();
          SceneManager.LoadScene(sceneToLoad);
      }
      //CHECK ERROR STRING
      else if(textdata[0]["error"].ToString() == invalidString)
      {
          errorMessage.text = "Verkeerde email of passwoord";
          errorMessage.enabled = true;
      }
    }
    //ERROR! CANT LOGIN
    else
    {
      errorMessage.text = "Verkeerde email of passwoord";
      errorMessage.enabled = true;
      Debug.LogError("Cannot connect to Login");
      Debug.Log(LoginAccountWWW.error.ToString());
    }
    }

    void LinkToCreateAccountURL() {
        Application.OpenURL(CreateAccountUrl);
    }
}
