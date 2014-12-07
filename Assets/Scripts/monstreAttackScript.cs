using UnityEngine;
using System.Collections;

public class monstreAttackScript : MonoBehaviour {
	
	int puissance;
	public float attackLength;
	float timeLeftAttacking;
	levelHeroScript levelScript;
	public Transform weapon;
	public Animator animmonster;
	Vector3 position_hero;
	public Vector3 test;

	bool attacking = false;
	
	void Start()
	{
		levelScript = GameObject.Find("Hero").GetComponent<levelHeroScript>();
		position_hero = GameObject.Find("Hero").GetComponent<Transform>().position;

	}

	void Update () {
		//attaque du personnage
		test = position_hero - transform.position;

		if((Mathf.Abs (position_hero.x - transform.position.x) < 3f) && ((Mathf.Abs(position_hero.y - transform.position.y) < 2f)))
			attack();
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
			
			coll.GetComponent<heroAttackScript>().HeroIsHit(puissance);
		}	
	}
	
	
	
}