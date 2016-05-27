﻿using UnityEngine;
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
    public string createAccountUrl = "http://semicolon.multimediatechnology.be/register";
    public string loginUrl = "http://semicolon.multimediatechnology.be/api/v1/authenticate";

    public InputField inputEmail, inputPassword;

    private string invalidString = "invalid_credentials";

    JsonData textData;
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
        }
        else
        {
            Debug.Log("can't find keys");
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

  IEnumerator LoginAccount() {

        WWWForm Form = new WWWForm();

        Form.AddField("email", email);
        Form.AddField("password", password);

        WWW LoginAccountWWW = new WWW(loginUrl, Form);

        yield return LoginAccountWWW;

    if (LoginAccountWWW.error == null)
    {
      textData = JsonMapper.ToObject(LoginAccountWWW.text);

      //SAVE TOKEN
      if (textData["token"].ToString() != "")
      {
          if (rememberMeCheckbox.isOn)
          {
              PlayerPrefs.SetString("username", email);
              PlayerPrefs.SetString("password", password);
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
      errorMessage.text = "Verkeerde email of passwoord";
      errorMessage.enabled = true;
      Debug.LogError("Cannot connect to Login");
      Debug.Log(LoginAccountWWW.error.ToString());
    }
    }

    public void LinkToCreateAccountURL() {
        Application.OpenURL(createAccountUrl);
    }
}
