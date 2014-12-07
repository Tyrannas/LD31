using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class heroUIScript : MonoBehaviour {
	
	public Transform affichage;
	public Transform affCanvas;
	
	public void setAffichagePV(int pv)
	{
		affichage.GetComponent<Text>().text = "Pv = "+pv;
	}
	
	public void detachCanvas()
	{
		//affCanvas.SetParent(null,true);
	}
	public void attachCanvas()
	{
		//affCanvas.SetParent(transform,true);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
