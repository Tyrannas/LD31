using UnityEngine;
using System.Collections;

public class monstreSoundsScript : MonoBehaviour {


	public AudioClip attack3;
	public AudioClip hit3;
	public AudioClip attack1;
	public AudioClip hit1;
	
	
	public void playAttack(int type)
	{
		if(type == 1)
			audio.PlayOneShot(attack1);
		else
			audio.PlayOneShot(attack3);
	}
	public void playHit(int type)
	{
		if(type == 1)
			audio.PlayOneShot(hit1);
		else
			audio.PlayOneShot(hit3);
	}
}
