using UnityEngine;
using System.Collections;

public class monstreAttackScript : MonoBehaviour {

	bool ok_baby;
	public int delta_time;
	int puissance;
	public float attackLength;
	float timeLeftAttacking;
	levelHeroScript levelScript;
	public Transform weapon;
	public Animator animmonster;
	Vector3 position_hero;
	public Vector3 test;
	bool attacking = false;
	int mtype;
	
	void Start()
	{
		levelScript = GameObject.Find("Hero").GetComponent<levelHeroScript>();
		puissance = GetComponent<monstreScript>().getPower();
		mtype = GetComponent<monstreScript>().getMType();
		ok_baby = true;
	}

	void Update () {
		//attaque du personnage
		position_hero = GameObject.Find("Hero").GetComponent<Transform>().position;
		test = position_hero-transform.position;
		if((Mathf.Abs (position_hero.x - transform.position.x) < 3f) && ((Mathf.Abs(position_hero.y - transform.position.y) < 2f))){
		if(ok_baby){
				attack();
				switch(mtype){
				case 1:
					delta_time = 70;
					break;
				case 2:
					delta_time = 130;
					break;
				case 3:
					delta_time = 200;
					break;
				}
					ok_baby = false;
			}
		}
		if(!ok_baby)
			delta_time--;
		if(delta_time < 0)
			ok_baby = true;

	}

	void FixedUpdate()
	{
		proceedAttack();
	}
	
	void attack()
	{

		Debug.Log("Attack");
		attacking = true;
		weapon.collider2D.enabled=true;
		timeLeftAttacking = attackLength;
		animmonster.Play ("attackMonster");	
	}
	
	void proceedAttack()
	{
		timeLeftAttacking -= Time.deltaTime;
		if(timeLeftAttacking <= 0)
		{
			animmonster.Play ("idleMonster");
			attacking=false;
			weapon.collider2D.enabled=false;
		}
	}
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		Debug.Log (coll.transform.name);
		if(coll.transform.tag=="Hero")
		{
			coll.GetComponent<heroAttackScript>().HeroIsHit(puissance, this.gameObject);
		}	
	}
	
	
	
}