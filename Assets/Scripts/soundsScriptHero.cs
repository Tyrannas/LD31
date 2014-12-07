using UnityEngine;
using System.Collections;

public class soundsScriptHero : MonoBehaviour {

	public AudioClip attackSound;
	public AudioClip attackedSound;
	public AudioClip levelUpSound;
	public AudioClip footstepsSound;
	public AudioClip levelDownSound;
	public AudioClip lifeUpSound;
	
	float timeSinceStep=0;
	public float walkTime;
	
	void Update()
	{
		timeSinceStep += Time.deltaTime;
	}
	
	public AudioSource audiosc;
	// Use this for initialization
	
	public void playAttack()
	{
		audiosc.PlayOneShot(attackSound);
	}
	public void playAttacked()
	{
		audiosc.PlayOneShot(attackedSound);
	}
	public void playLevelUp()
	{
		audiosc.PlayOneShot(levelUpSound);
	}
	public void playFootsteps()
	{
		if(timeSinceStep>=walkTime)
		{
			timeSinceStep=0;
			audiosc.PlayOneShot(footstepsSound,0.3f);
		}
	}
	public void playLevelDown()
	{
		audiosc.PlayOneShot(levelDownSound);
	}
	public void playLifeUp()
	{
		audiosc.PlayOneShot(lifeUpSound);
	}
}
