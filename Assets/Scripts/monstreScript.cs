using UnityEngine;
using System.Collections;

public class monstreScript : MonoBehaviour {
	
	public int typeMonstre;

	int puissance;
	int pv;
	public bool grounded;
	public bool stuck;
	public LayerMask whatIsGround;
	public LayerMask whatIsWall;
	public Transform groundCheck;
	public Transform wallCheck;

	float groundRadius = 0.2f;
	float wallRadius = 1f;

    float vitesse;
	public float vitesse2;
	public float test;
	int jumpForceMOB;
	Vector3 position_hero;

	// Use this for initialization
	void Start () {
		if(typeMonstre == 1)
		{
			puissance =1;
			pv = 4;
			vitesse = 3;
			jumpForceMOB = 5;
		}
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		stuck = Physics2D.OverlapCircle (wallCheck.position, wallRadius, whatIsWall);

		position_hero = GameObject.Find("Hero").GetComponent<Transform>().position;
		vitesse2 = rigidbody2D.velocity.x;

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



	}

	void jump(){
		rigidbody2D.AddForce(new Vector2(0,jumpForceMOB),ForceMode2D.Impulse);
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
