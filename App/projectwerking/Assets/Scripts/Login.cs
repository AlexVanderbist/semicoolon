using UnityEngine;
using System.Collections;
using LitJson;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(GameInfo))]
public class Login : MonoBehaviour {

  //USES EMAIL AND PASS IF THEY ARE SAVED IN PLAYERPREFS
  //USED TO GET TOKEN
  //TOKEN IS VITAL TO ALL ACTIONS

    public Toggle rememberMeCheckbox;
    public InputField inputEmail, inputPassword;
    public Text errorMessage;
    public string sceneToLoad = "ProjectsScene";
    public string createAccountUrl = "http://semicolon.multimediatechnology.be/register";
    public string loginUrl = "http://semicolon.multimediatechnology.be/api/v1/authenticate";

    private string invalidString = "invalid_credentials";
    private string email = "";
    private string password = "";

    JsonData textData;
    GameInfo GI;
 
	  // LOAD PLAYERPREFS
	  void Start () {
          GI = GetComponent<GameInfo>();
          errorMessage.enabled = false;
          if (PlayerPrefs.HasKey("username") && PlayerPrefs.HasKey("password"))
          {
              email = PlayerPrefs.GetString("username");
              inputEmail.text = email;
              password = PlayerPrefs.GetString("password");
              inputPassword.text = password;
          }
          else
          {
              Debug.Log("can't find keys");
          }
	  }
    
    // INPUTFIELD
    public void SetEmail(string initEmail)
    {
      email = initEmail;
    }
    
    //INPUTFIELD
    public void SetPassword(string initPassword)
    {
      password = initPassword;
    }
    
    // LOGIN BUTTON, STARTS RECEIVING TOKEN
    public void LoginOnClick()
    {
      if (email != "" && password != "")
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
    
    // THE ACTUAL RECEIVING OF TOKEN, BASED ON EMAIL AND PASSWORD
    IEnumerator LoginAccount() {

      WWWForm Form = new WWWForm();

      Form.AddField("email", email);
      Form.AddField("password", password);

      WWW LoginAccountWWW = new WWW(loginUrl, Form);
      yield return LoginAccountWWW;
      textData = JsonMapper.ToObject(LoginAccountWWW.text);

      if (LoginAccountWWW.error == null)
      {
        //SAVE TOKEN
        if (textData["token"].ToString() != "")
        {
            if (rememberMeCheckbox.isOn)
            {
                PlayerPrefs.SetString("username", email);
                GI.Email = email;
                PlayerPrefs.SetString("password", password);
                GI.Password = password;
            }
            errorMessage.enabled = false;
            GI.Token = textData["token"].ToString();
            SceneManager.LoadScene(sceneToLoad);
        }
        //CHECK ERROR STRING
        else if(textData[0]["error"].ToString() == invalidString)
        {
            errorMessage.text = "Verkeerde email of passwoord";
            errorMessage.enabled = true;
        }
      }
      //ERROR! CANT LOGIN
      else
      {
        errorMessage.text = "Verkeerde gegevens of geen internet.";
        errorMessage.enabled = true;
        Debug.LogError("Cannot connect to Login");
        Debug.Log(LoginAccountWWW.error.ToString());
      }
    }
    
    public void LinkToCreateAccountURL() {
          Application.OpenURL(createAccountUrl);
    }
}
