using UnityEngine;
using System.Collections;

public class MoveHeroScript : MonoBehaviour {
	
	public float moveSpeed;
	public float jumpForce;

	bool facingRight = true;
	bool grounded;
	heroAttackScript is_hit;
	soundsScriptHero soundHero;
	levelHeroScript levelhero;
	bool pushed;
	heroUIScript heroUI;
	Animator animsprite;

	void Start()
	{
		soundHero = GameObject.Find("Hero").GetComponent<soundsScriptHero>();
		heroUI = GameObject.Find("Hero").GetComponent<heroUIScript>();
		is_hit = GameObject.Find ("Hero").GetComponent<heroAttackScript>();
		animsprite = GameObject.Find ("heroSprite").GetComponent<Animator>();
		levelhero = GameObject.Find ("Hero").GetComponent<levelHeroScript>();

	}
	
	public void setJumpForce(int nb)
	{
		jumpForce=nb;
	}
	void Update () {
		
		//deplacement horizontal du personnage
		float inputX;
		pushed = is_hit.IsPushed();

		if(!pushed){
		inputX = Input.GetAxisRaw ("Horizontal");
		rigidbody2D.velocity = new Vector2(inputX*moveSpeed,rigidbody2D.velocity.y);

		animsprite.SetFloat ("speed", Mathf.Abs(rigidbody2D.velocity.x));
		animsprite.SetBool ("grounded", grounded);

		if(Mathf.Abs(inputX)!=0 && grounded)
			soundHero.playFootsteps();
		
		if (inputX > 0 && !facingRight)
		{
			Flip_x();
		}
		else if (inputX < 0 && facingRight)
			Flip_x();
		
		//saut du personnage
		if(Input.GetButtonDown("Jump") && grounded)
		{
			Debug.Log ("why");
			rigidbody2D.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);
		}
		}

	}
	
	
	public bool getFacingRight()
	{
		return facingRight;
	}
	public bool getGrounded(){
		return grounded;
	}
	//gestion de l'orientation gauche droite du perso
	public void Flip_x(){
		facingRight = !facingRight;
		heroUI.preventFlip();
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.name == "death")
		{
			levelhero.Die();
		}
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