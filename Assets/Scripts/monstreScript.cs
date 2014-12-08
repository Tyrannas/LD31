using UnityEngine;
using System.Collections;

public class monstreScript : MonoBehaviour {
	
	public int typeMonstre;

	public int puissance;
	bool push;
	public int pv;
	public bool grounded;
	public bool stuck;
	public float compteur;
	bool vide;
	public LayerMask whatIsGround;
	public LayerMask whatIsWall;
	public Transform groundCheck;
	public Transform wallCheck;
	public Transform voidCheck;
	public GameObject bloodParticle;
	monstreSoundsScript soundmonstre;
	public float position_hm;
	int facingRight;
	bool focus;
	float focus_range = 25;
	float groundRadius = 0.2f;
	float wallRadius = 0.9f;
	MoveHeroScript direction_epee;
    float vitesse;
	int jumpForceMOB;
	Vector3 position_hero;

	// Use this for initialization
	public void Initialise () {
		soundmonstre = GetComponent<monstreSoundsScript>();

		switch(typeMonstre){
		case 1:
			puissance = 3;
			pv = 3;
			vitesse = 5;
			jumpForceMOB = 3;
			break;
		case 2:
			puissance = 7;
			pv = 10;
			vitesse = 4;
			jumpForceMOB = 5;
			break;
		case 3:
			puissance = 10;
			pv = 15;
			vitesse = 3;
			jumpForceMOB = 0;
			break;
		}
		facingRight = -1;
		compteur = -1;
		focus = false;

		direction_epee = GameObject.Find("Hero").GetComponent<MoveHeroScript>();
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		stuck = Physics2D.OverlapCircle (wallCheck.position, wallRadius, whatIsWall);
		vide = (!Physics2D.OverlapCircle (voidCheck.position, groundRadius, whatIsGround)) || (Physics2D.OverlapCircle (voidCheck.position, wallRadius, whatIsWall)) ;

		position_hero = GameObject.Find("Hero").GetComponent<Transform>().position;
		position_hm = position_hero.y - transform.position.y;

		if(push){
			compteur = 20;
			push = false;
		}


		if(compteur < 0){
		compteur = -1;
		switch(typeMonstre){
		case 1:
			if(focus){
			if((position_hero.x - transform.position.x) < 0){
			rigidbody2D.velocity = new Vector2(-vitesse, rigidbody2D.velocity.y);
				if(facingRight > 0)
						Flip ();
				}
				else if((position_hero.x - transform.position.x) > 1){
			rigidbody2D.velocity = new Vector2(vitesse, rigidbody2D.velocity.y);
			if(facingRight < 0)
						Flip ();
			}
			if((Mathf.Abs(position_hero.x - this.transform.position.x) < 0.3) && (Mathf.Abs (position_hero.y - this.transform.position.y) < 10) && grounded){
			jump ();	
			grounded = false;
			}
			if(stuck)
			 jump ();
				}
				else if(Mathf.Abs(position_hero.x - transform.position.x) <= focus_range)
					focus = true;
			break;
		case 2:
			if(((position_hero.x - transform.position.x) > -10) && ((position_hero.x - transform.position.x) < 0) && ((position_hm < 3f) && (position_hm > -3f))){
				rigidbody2D.velocity = new Vector2(-vitesse, rigidbody2D.velocity.y);
				if(facingRight > 0)
						Flip ();
				}
			else if(((position_hero.x - transform.position.x) < 10) && ((position_hero.x - transform.position.x) > 1) && ((position_hm < 3f) && (position_hm > -3f))){
				rigidbody2D.velocity = new Vector2(vitesse, rigidbody2D.velocity.y);
				if(facingRight < 0)
						Flip ();
			}
			else if(stuck)
						Flip();
			else if(vide)
						Flip ();
			else
				rigidbody2D.velocity = new Vector2(facingRight * vitesse, rigidbody2D.velocity.y);
			break;

		case 3:
			if(((position_hero.x - transform.position.x) > -6) && ((position_hero.x - transform.position.x) < -1)){
				rigidbody2D.velocity = new Vector2(-vitesse, rigidbody2D.velocity.y);
				if(facingRight > 0)
						Flip ();
				}
			else if(((position_hero.x - transform.position.x) < 6) && ((position_hero.x - transform.position.x) > 1)){
				rigidbody2D.velocity = new Vector2(vitesse, rigidbody2D.velocity.y);
				if(facingRight < 0)
						Flip ();

			}
			break;

		}
	}
	}

	void FixedUpdate(){
		if(compteur >= 0)
			compteur--;
	}

	public void jump(){
		rigidbody2D.AddForce(new Vector2(0,jumpForceMOB),ForceMode2D.Impulse);
	}

	public void Flip(){
			facingRight = - facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
	}


	public int monsterIsHit(int puissancehero)
	{	
		push = true;
		spillBlood();
		soundmonstre.playHit(getMType());	
		if(direction_epee.getFacingRight()){
			rigidbody2D.AddForce(new Vector2(puissancehero*5 - puissance/3, puissancehero*5 - puissance/3),ForceMode2D.Impulse);
		}
		else{
			rigidbody2D.AddForce(new Vector2(-puissancehero*5 - puissance/3, puissancehero*5 - puissance/3),ForceMode2D.Impulse);
		}
		pv-=puissancehero;
		if(pv<=0)
		{
			Destroy(this.gameObject);
			return typeMonstre;
		}
		return 0;
	}
	void spillBlood()
	{
		GameObject blood = (GameObject) Instantiate(bloodParticle,this.transform.position, Quaternion.identity);
		Destroy(blood,2);
	}
	public int getMType(){
		return typeMonstre;
	}
	public int getFacingRight()
	{
		return facingRight;
	}
	public int getPower(){		
		return puissance;
	}
}
