using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public GameObject nuages;
	public Vector2 endNuages;
	public float speedNuages;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown)
		{
			Application.LoadLevel(1);
		}
		nuages.transform.position = Vector2.MoveTowards(nuages.transform.position,endNuages,speedNuages);
	}
}
