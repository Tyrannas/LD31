using UnityEngine;
using System.Collections;

public class monstreSoundsScript : MonoBehaviour {


	public AudioClip attack3;
	public AudioClip hit3;
	public AudioClip attack1;
	public AudioClip hit1;
	
	
	public void playAttack(int type)
	{
		if(type == 3)
			audio.PlayOneShot(attack3);
		else
			audio.PlayOneShot(attack1);
	}
	public void playHit(int type)
	{
		if(type == 3)
			audio.PlayOneShot(hit3);
		else
			audio.PlayOneShot(hit1);
	}
}
