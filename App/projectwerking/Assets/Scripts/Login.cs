using UnityEngine;
using System.Collections;
using LitJson;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {

    public static string Email = "";
    public static string Password = "";
    public string CurrentMenu = "Login";

    public float X, Y, Width, Height;

    private string CreateAccountUrl = "http://www.EigenWebiste.be";
    private string LoginUrl = "http://semicolon.multimediatechnology.be/Login";
    private string antwoord = "";

    JsonData textdata;

	// Use this for initialization
	void Start () {
	
	}


    void OnGUI() {

        if (CurrentMenu == "Login")
        {
            LoginGUI();
        }
        else if (CurrentMenu == "CreateAccount")
        {
            LinkToCreateAccountURL();
        }

    }

    void LoginGUI() {

        GUI.Box(new Rect(33,75,210,300), "Login"); 

        if (GUI.Button(new Rect(85,325,105,30), "Create Account"))
        {
            CurrentMenu = "CreateAccount";
        }
        if (GUI.Button(new Rect(85,285,105,30), "Login"))
        {
            StartCoroutine("LoginAccount");
        }
        GUI.Label(new Rect(50, 120, 180, 35), "Email:");
        Email = GUI.TextField(new Rect(47, 140, 180, 35), Email);

        GUI.Label(new Rect(50, 190, 180, 35), "Wachtwoord:");
        Password = GUI.TextField(new Rect(47, 210, 180, 35), Password);
    
    }

    IEnumerator LoginAccount() {

        WWWForm Form = new WWWForm();

        Form.AddField("Email", Email);
        Form.AddField("Password", Password);

        WWW LoginAccountWWW = new WWW(LoginUrl, Form);

        yield return LoginAccountWWW;

        if (LoginAccountWWW.error != null)
        {
            Debug.LogError("Cannot connect to Login");
        }
        else
        {
            textdata = JsonMapper.ToObject(LoginAccountWWW.text);
            if (textdata[1][antwoord].ToString() == "Juist")
            {
                SceneManager.LoadScene("MainScene");
            }
            else if (textdata[1][antwoord].ToString() == "Fout")
            {
                Debug.Log("Foutieve info gegeven!");
            }
        }
    }

    void LinkToCreateAccountURL() {

        Application.OpenURL(CreateAccountUrl);
        CurrentMenu = "Login";
    }
}
