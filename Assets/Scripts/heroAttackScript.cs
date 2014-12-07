using UnityEngine;
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
	public Transform weaponHero;
	public Animator animhero;
	public GameObject bloodParticle;
	bool attacking = false;
	bool grounded;

	void Start()
	{
		levelScript = GameObject.Find("Hero").GetComponent<levelHeroScript>();
		heroUI = GameObject.Find("Hero").GetComponent<heroUIScript>();
		heroMove = GameObject.Find("Hero").GetComponent<MoveHeroScript>();
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
		spillBlood ();
		int coeff = monstre.GetComponent<monstreScript>().getFacingRight();
		rigidbody2D.AddForce(new Vector2(coeff * (puissanceMonstre*3 - puissance/3), puissanceMonstre*3 - puissance/3),ForceMode2D.Impulse);
		levelScript.subPv(puissanceMonstre);
	}
	
	void spillBlood()
	{
		GameObject blood = (GameObject) Instantiate(bloodParticle,this.transform.position, Quaternion.identity);
		blood.transform.SetParent(transform);
		Destroy(blood,2);
	}

	
}