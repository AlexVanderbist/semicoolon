using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProfileLoader : MonoBehaviour {

  public Text userName, email, numberOfStamps;
  GameInfo GI;

	// LOADS WHEN IT RECEIVES THE SENDMESSAGE FROM DATAOBTAINER
	void LoadProfileData () {
    GI = GameObject.Find("GameData").GetComponent<GameInfo>();
    userName.text = userName.text + " " + GI.FirstNamePerson + " " + GI.LastNamePerson;
    email.text = email.text + " " + GI.Email;
  }
}
