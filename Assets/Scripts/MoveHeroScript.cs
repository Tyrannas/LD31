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
	
	int level = 1;
	int pv = 10;
	int puissance =1;
	
	
	public Transform weapon;
	public Animator animhero;
	
	// Use this for initialization
	void Start () {
		
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
			Debug.Log ("unity se chie dessus");
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
