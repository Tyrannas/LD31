using UnityEngine;
using System.Collections;

public class lightStoneScript : MonoBehaviour {

	bool isActive=false;

	void Update () {
	
	}
	
	
	void activate()
	{
		GameObject cam = GameObject.Find("Main Camera");
		cam.transform.parent.GetComponent<lightStoneScript>().Deactivate();
		isActive=true;
		cam.transform.parent = this.transform;
		cam.transform.localPosition = new Vector3(0,0,cam.transform.position.z);
	}
	public void Deactivate()
	{
		isActive=false;	
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.name=="Hero" && !isActive)
		{
			activate();	
		}
		
	}
	
	
}
