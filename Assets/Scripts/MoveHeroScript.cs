using UnityEngine;
using System.Collections;

public class MoveHeroScript : MonoBehaviour {
	
	public float moveSpeed;
	public float jumpForce;
	
	bool facingRight = true;
	bool grounded;
	
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
	}
	
	
	public bool getFacingRight()
	{
		return facingRight;
	}
	
	//gestion de l'orientation gauche droite du perso
	public void Flip_x(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
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