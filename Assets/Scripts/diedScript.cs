using UnityEngine;
using System.Collections;

public class diedScript : MonoBehaviour {
	
	bool dead=false;
	

	public void Die () {
		dead=true;
		transform.position = GameObject.Find("Main Camera").transform.position;
		transform.position = new Vector3(transform.position.x,transform.position.y,0);
	}
	
	
	void Update () {
		if(dead && Input.anyKeyDown)
		{
			Application.LoadLevel(0);
		}
	}
}
