using UnityEngine;
using System.Collections;


public class heroAttackScript : MonoBehaviour {
	
	public int pv;
	int puissance;
	public float attackLength;
	float timeLeftAttacking;
	
	levelHeroScript levelScript;
	heroUIScript heroUI;
	
	public Transform weaponHero;
	public Animator animhero;
	bool direction_monstre;

	
	bool attacking = false;
	
	void Start()
	{
		levelScript = GameObject.Find("Hero").GetComponent<levelHeroScript>();
		heroUI = GameObject.Find("Hero").GetComponent<heroUIScript>();
		direction_monstre = true;

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
		if(coll.transform.tag=="Monstre")
		{
			
			int pvtoadd =coll.GetComponent<monstreScript>().monsterIsHit(puissance);
			levelScript.addPv(pvtoadd);
		}	
	}
	public void HeroIsHit(int puissanceMonstre)
	{	
		//push = true;
		
		if(direction_monstre){
			rigidbody2D.AddForce(new Vector2(puissanceMonstre*5 - puissance/3, puissanceMonstre*5 - puissance/3),ForceMode2D.Impulse);
		}
		else{
			rigidbody2D.AddForce(new Vector2(-puissanceMonstre*5 - puissance/3, puissanceMonstre*5 - puissance/3),ForceMode2D.Impulse);

		}
		
		levelScript.subPv(puissanceMonstre);
	}

	
}