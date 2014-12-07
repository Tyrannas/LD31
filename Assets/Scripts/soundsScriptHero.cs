using UnityEngine;
using System.Collections;

public class soundsScriptHero : MonoBehaviour {

	public AudioClip attackSound;
	public AudioSource audiosc;
	// Use this for initialization
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.E))
			playAttack();
	}
	
	public void playAttack()
	{
		audio.PlayOneShot(attackSound);
	}
}
