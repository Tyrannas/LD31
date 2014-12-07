using UnityEngine;
using System.Collections;

public class testUI : MonoBehaviour {
	
	//public Transform scrollr;
	// Use this for initialization
	public int test;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.L))
		{
			Debug.Log ("L pushed");
			GetComponent<RectTransform>().sizeDelta = new Vector2(test,GetComponent<RectTransform>().sizeDelta.y);
		}
	}
}
