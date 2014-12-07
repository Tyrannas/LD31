using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class heroUIScript : MonoBehaviour {
	
	public Transform affichage;
	
	public void setAffichagePV(int pv)
	{
		affichage.GetComponent<Text>().text = "Pv = "+pv;
	}
	
	public void preventFlip(bool facingRight)
	{
		
	}
	// Update is called once per frame
	void Update () {
	
	}
}
