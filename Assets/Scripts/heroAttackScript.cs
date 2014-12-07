using UnityEngine;
using System.Collections;


public class heroAttackScript : MonoBehaviour {
	
	int pv;
	int puissance;
	public float attackLength;
	float timeLeftAttacking;
	
	levelHeroScript levelScript;
	
	public Transform weaponHero;
	public Animator animhero;
	
	
	bool attacking = false;
	
	void Start()
	{
		levelScript = GameObject.Find("Hero").GetComponent<levelHeroScript>();
	}
	
	public void setPv(int nb)
	{
		pv=nb;
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
		{
			attack();
		}
		if(Input.GetKeyDown(KeyCode.E))
		{
			weaponHero.collider2D.enabled=false;
		}	
	}
	void FixedUpdate()
	{
		proceedAttack();
	}
	
	void attack()
	{
		attacking = true;
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
		Debug.Log (coll.transform.name);
		if(coll.transform.tag=="Monstre")
		{
			
			int pvtoadd =coll.GetComponent<monstreScript>().monsterIsHit(puissance);
			levelScript.addPv(pvtoadd);
		}	
	}

	
	
}