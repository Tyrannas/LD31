﻿using UnityEngine;
using System.Collections;

public class levelHeroScript : MonoBehaviour {
	
	heroAttackScript attackScript;
	MoveHeroScript moveScript;
	soundsScriptHero soundHero;
	
	int levelHero;
	
	const int level1pv=10;
	const int level2pv=12;
	const int level3pv=14;
	const int level1puissance=1;
	const int level2puissance=2;
	const int level3puissance=3;
	public int level1JumpForce=19;
	public int level2JumpForce=22;
	public int level3JumpForce=26;
	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	
	public GameObject levelParticle;
	public GameObject pvParticle;
	
	void Start()
	{
		attackScript = GameObject.Find("Hero").GetComponent<heroAttackScript>();
		soundHero = GameObject.Find("Hero").GetComponent<soundsScriptHero>();
		moveScript = GameObject.Find("Hero").GetComponent<MoveHeroScript>();
		attackScript.Initialise();
		SetLevelHero(1);
	}
	
	public void addPv(int nb)
	{
		if(nb!=0)
		{
			pvParticleLaunch();
			soundHero.playLifeUp();
		}
		int pv = attackScript.getPv();
		int pvtemp = pv+nb;
		if(levelHero==1 && pvtemp>=level2pv)
		{
			levelParticleLaunch();
			soundHero.playLevelUp();
			SetLevelHero(2);
		}
		else if(levelHero==2 && pvtemp>=level3pv)
		{
			levelParticleLaunch();
			SetLevelHero (3);
			soundHero.playLevelUp();
		}
		else if(levelHero==3 && pvtemp>=level3pv)
		{
			pv=level3pv;
			attackScript.setPv(pv);
		}
		else
		{
			pv = pvtemp;
			attackScript.setPv(pv);
		}
		
	}
	public void subPv(int nb)
	{
		int pv = attackScript.getPv();
		int pvtemp = pv - nb;
		if(pvtemp<=0)
			Debug.Log ("dead");
		else if(levelHero == 2 && pvtemp<=level1pv)
		{
			SetLevelHero(1);
			soundHero.playLevelDown();
		}
		else if(levelHero == 3 && pvtemp<=level2pv)
		{
			SetLevelHero(2);
			soundHero.playLevelDown();
		}
		else
			attackScript.setPv(pvtemp);
			
	}
	
	public void SetLevelHero(int levelvoulu)
	{
		Sprite sprtTemp;
		int coefFacingRight=-1;
		if(moveScript.getFacingRight())
		{
			coefFacingRight=1;
		}
		if(levelvoulu == 1)
		{
			levelHero=1;
			attackScript.setPv(level1pv);
			attackScript.setPuissance(level1puissance);
			moveScript.setJumpForce(level1JumpForce);
			this.transform.localScale = new Vector2(1*coefFacingRight,1);
			sprtTemp = sprite1;		
			
		}
		else if(levelvoulu == 2)
		{
			levelHero=2;
			attackScript.setPv(level2pv);
			attackScript.setPuissance(level2puissance);
			moveScript.setJumpForce(level2JumpForce);
			this.transform.localScale = new Vector2(2*coefFacingRight,1.5f);
			sprtTemp = sprite2;
		}
		else
		{
			levelHero=3;
			attackScript.setPv(level3pv);
			attackScript.setPuissance(level3puissance);
			moveScript.setJumpForce(level3JumpForce);
			this.transform.localScale = new Vector2(4*coefFacingRight,3);
			sprtTemp = sprite3;
		}

			GameObject.Find("heroSprite").GetComponent<SpriteRenderer>().sprite = sprtTemp;	
	}
	void pvParticleLaunch()
	{
		GameObject pvpart = (GameObject) Instantiate(pvParticle,this.transform.position, Quaternion.identity);
		pvpart.transform.SetParent(transform);
		pvpart.transform.Rotate(Vector3.right,-90);
		Destroy(pvpart,2);
	}
	void levelParticleLaunch()
	{
		GameObject lvlpart = (GameObject) Instantiate(levelParticle,this.transform.position, Quaternion.identity);
		lvlpart.transform.SetParent(transform);
		Destroy(lvlpart,2);
	}
	

}