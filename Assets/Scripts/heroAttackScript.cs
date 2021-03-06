﻿using UnityEngine;
using System.Collections;


public class heroAttackScript : MonoBehaviour {
	
	public int pv;
	int puissance;
	public float attackLength;
	float timeLeftAttacking;
	bool push;
	
	levelHeroScript levelScript;
	MoveHeroScript heroMove;
	heroUIScript heroUI;
	soundsScriptHero soundHero;
	
	public Transform weaponHero;
	public Animator animhero;
	public GameObject bloodParticle;
	Animator animsprite;
	bool attacking = false;
	bool grounded;

	public void Initialise()
	{
		levelScript = GameObject.Find("Hero").GetComponent<levelHeroScript>();
		soundHero = GameObject.Find("Hero").GetComponent<soundsScriptHero>();
		heroUI = GameObject.Find("Hero").GetComponent<heroUIScript>();
		heroMove = GameObject.Find("Hero").GetComponent<MoveHeroScript>();
		animsprite = GameObject.Find ("heroSprite").GetComponent<Animator>();

		push = false;
	}
	
	public void setPv(int nb)
	{
		pv=nb;
		heroUI.setAffichagePV(pv);
	}
	public int getPv()
	{
		return pv;
	}
	public void setPuissance(int nb)
	{
		puissance=nb;
	}
	
	void Update () {
		//attaque du personnage
		if(Input.GetKeyDown(KeyCode.Z) && !attacking)
			attack();

		grounded = heroMove.getGrounded();
		if(grounded)
			push = false;

		animsprite.SetBool ("attack", attacking);

	}
	void FixedUpdate()
	{
		proceedAttack();
	}
	
	void attack()
	{
		attacking = true;
		soundHero.playAttack();
		weaponHero.collider2D.enabled=true;
		timeLeftAttacking = attackLength;
		animhero.Play ("attackHero");	
	}
	
	void proceedAttack()
	{
		timeLeftAttacking -= Time.deltaTime;
		if(timeLeftAttacking <= 0)
		{
			animhero.Play ("idleHero");
			attacking=false;
			weaponHero.collider2D.enabled=false;
		}
	}
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.transform.tag == "Monstre")
		{
			int pvtoadd = coll.GetComponent<monstreScript>().monsterIsHit(puissance);
			levelScript.addPv(pvtoadd);
		}	
	}
	public bool IsPushed()
	{
		return push;
	}
	public void HeroIsHit(int puissanceMonstre, GameObject monstre)
	{	
		push = true;
		soundHero.playAttacked();
		spillBlood ();
		int coeff = monstre.GetComponent<monstreScript>().getFacingRight();
		rigidbody2D.AddForce(new Vector2(coeff * (puissanceMonstre*2 - puissance/3), Mathf.Abs (puissanceMonstre - puissance/3)),ForceMode2D.Impulse);
		levelScript.subPv(puissanceMonstre);
	}
	
	void spillBlood()
	{
		GameObject blood = (GameObject) Instantiate(bloodParticle,this.transform.position, Quaternion.identity);
		blood.transform.SetParent(transform);
		Destroy(blood,2);
	}

	
}