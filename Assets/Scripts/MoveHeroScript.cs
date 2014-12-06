using UnityEngine;
using System.Collections;

public class MoveHeroScript : MonoBehaviour {
	
	public float moveSpeed;
	public float jumpForce;
	public float attackRange;
	public float attackLength;
	float timeLeftAttacking;
	
	bool grounded;
	bool attacking = false;
	bool facingRight = true;
	
	int level;
	int pv;
	int puissance;
	
	//caractéristiques dépendant des niveaux
	const int level1pv=10;
	const int level2pv=15;
	const int level3pv=20;
	const int level1puissance=1;
	const int level2puissance=2;
	const int level3puissance=3;
	
	
	public Transform weapon;
	public Animator animhero;
	
	// Use this for initialization
	void Start () {
		level = 1;
		pv = level1pv;
		puissance = level1puissance;
		animhero = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
		//deplacement horizontal du personnage
		float inputX;
		inputX = Input.GetAxisRaw ("Horizontal");
		rigidbody2D.velocity = new Vector2(inputX*moveSpeed,rigidbody2D.velocity.y);
		
		if (inputX > 0 && !facingRight)
		{
			Flip_x();
		}
		else if (inputX < 0 && facingRight)
			Flip_x();
		
		//saut du personnage
		if(Input.GetButtonDown("Jump") && grounded)
		{
			rigidbody2D.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);
		}	
		
		//attaque du personnage
		if(Input.GetKeyDown(KeyCode.Z) && !attacking)
		{
			attack();
		}
	}
	
	void FixedUpdate()
	{	
		proceedAttack();
	}
	
	
	
	//gestion de l'orientation gauche droite du perso
	void Flip_x(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	//functions dedicated to the attack
	void attack()
	{
		attacking = true;
		//weapon.collider2D.enabled=true;
		animhero.Play ("attackHero");
		timeLeftAttacking = attackLength;
		
	}
	void proceedAttack()
	{
		timeLeftAttacking -= Time.deltaTime;
		if(timeLeftAttacking <= 0)
		{
			animhero.Play ("idleHero");
			attacking=false;
		}
		//weapon.collider2D.enabled=false;
	}

	public void addPv(int nb)
	{
		int pvtemp = pv+nb;
		if(level==1 && pvtemp>=level2pv)
			levelUp(2);
		else if(level==2 && pvtemp>=level3pv)
			levelUp(3);
		else if(level==3 && pvtemp>=level3pv)
			pv=level3pv;
		else
			pv = pvtemp;
	}
	
	void levelUp(int futurlevel)
	{
		level= futurlevel;
		if(level==2)
		{
			Debug.Log ("level2");
			pv=level2pv;
			puissance=level2puissance;
			this.transform.localScale = new Vector2(2,1.5f);
		}
		else if(level==3)
		{	
			Debug.Log ("level3");
			pv=level3pv;
			puissance=level3puissance;	
			this.transform.localScale = new Vector2(4,3);
			
		}

	}
	
	public int getPuissance()
	{
		return puissance;
	}
	
	public bool getAttacking()
	{
		return attacking;
	}
	
			
	//avec ces trois fonctions on checke quand le hero touche le sol.
	void OnCollisionEnter2D(Collision2D collision)
	{
		foreach (ContactPoint2D c in collision.contacts)
		{
			if(c.otherCollider.name == "heroGroundCheck" )
			{
				grounded = true;
			}
		}
		
	}
	void OnTriggerEnter2D(Collider2D coll)
	{
		Debug.Log (coll.transform.name);
		if(coll.transform.name=="Monstre")
		{
				
				int pvtoadd =coll.GetComponent<monstreScript>().monsterIsHit(puissance);
				addPv(pvtoadd);
		}	
	}
	void OnCollisionStay2D(Collision2D collision)
	{
		foreach (ContactPoint2D c in collision.contacts)
		{
			if(c.otherCollider.name == "heroGroundCheck" )
			{
				grounded = true;
			}
		}
		
	}
	void OnCollisionExit2D( Collision2D collision)
	{
		foreach (ContactPoint2D c in collision.contacts)
		{
			if(c.otherCollider.name == "heroGroundCheck")
				grounded = false;
			
		}
		
	}
}
