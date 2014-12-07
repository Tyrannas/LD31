using UnityEngine;
using System.Collections;

public class monstreScript : MonoBehaviour {
	
	public int typeMonstre;

	int puissance;
	public int pv;
	public bool grounded;
	public bool stuck;
	bool vide;
	public LayerMask whatIsGround;
	public LayerMask whatIsWall;
	public Transform groundCheck;
	public Transform wallCheck;
	public Transform voidCheck;

	int facingRight;
	float groundRadius = 0.2f;
	float wallRadius = 1f;

    float vitesse;
	int jumpForceMOB;
	Vector3 position_hero;

	// Use this for initialization
	void Start () {
		switch(typeMonstre){
		case 1:
			puissance = 1;
			pv = 2;
			vitesse = 6;
			jumpForceMOB = 5;
			break;
		case 2:
			puissance = 4;
			pv = 4;
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
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		stuck = Physics2D.OverlapCircle (wallCheck.position, wallRadius, whatIsWall);
		vide = (!Physics2D.OverlapCircle (voidCheck.position, groundRadius, whatIsGround)) || (Physics2D.OverlapCircle (voidCheck.position, wallRadius, whatIsWall)) ;

		position_hero = GameObject.Find("Hero").GetComponent<Transform>().position;
	
		switch(typeMonstre){
		case 1:
			if((position_hero.x - transform.position.x) < 0){
			rigidbody2D.velocity = new Vector2(-vitesse, rigidbody2D.velocity.y);}
			else{
			rigidbody2D.velocity = new Vector2(vitesse, rigidbody2D.velocity.y);
			}
			if((Mathf.Abs(position_hero.x - this.transform.position.x) < 0.3) && (Mathf.Abs (position_hero.y - this.transform.position.y) < 10) && grounded){
			jump ();	
			grounded = false;
			}
			if(stuck)
			jump ();
			break;
		case 2:
			if(((position_hero.x - transform.position.x) > -10) && ((position_hero.x - transform.position.x) < 0))
				rigidbody2D.velocity = new Vector2(-vitesse, rigidbody2D.velocity.y);
			else if(((position_hero.x - transform.position.x) < 10) && ((position_hero.x - transform.position.x) > 0))
				rigidbody2D.velocity = new Vector2(vitesse, rigidbody2D.velocity.y);
			else if(stuck)
						Flip();
			else if(vide)
						Flip ();
			else
				rigidbody2D.velocity = new Vector2(facingRight * vitesse, rigidbody2D.velocity.y);
			break;

		case 3:
			if(((position_hero.x - transform.position.x) > -6) && ((position_hero.x - transform.position.x) < 0)){
				rigidbody2D.velocity = new Vector2(-vitesse, rigidbody2D.velocity.y);}
			else if(((position_hero.x - transform.position.x) < 6) && ((position_hero.x - transform.position.x) > 0)){
				rigidbody2D.velocity = new Vector2(vitesse, rigidbody2D.velocity.y);

			}
			break;

		}
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
		pv-=puissancehero;
		if(pv<=0)
		{
			Destroy(this.gameObject);
			return typeMonstre;
		}
		return 0;
	}
}
