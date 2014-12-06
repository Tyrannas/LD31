using UnityEngine;
using System.Collections;

public class MoveHeroScript : MonoBehaviour {
	
	public float moveSpeed;
	public float jumpForce;
	
	public bool grounded;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		//deplacement horizontal du personnage
		float inputX;
		inputX = Input.GetAxisRaw ("Horizontal");
		rigidbody2D.velocity = new Vector2(inputX*moveSpeed,rigidbody2D.velocity.y);
		
		
		//saut du personnage
		if(Input.GetButtonDown("Jump") && grounded)
		{
			rigidbody2D.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);
		}	
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
