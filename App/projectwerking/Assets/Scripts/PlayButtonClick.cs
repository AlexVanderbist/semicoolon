using UnityEngine;
using System.Collections;

public class PlayButtonClick : MonoBehaviour {

    public AudioClip sound;
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

	// Use this for initialization
	void Start () {

        gameObject.AddComponent<AudioSource>();
        source.clip = sound;
        source.playOnAwake = false;
	
	}

    public void PlaySound()
    {
        source.PlayOneShot(sound);
    }
	
}
