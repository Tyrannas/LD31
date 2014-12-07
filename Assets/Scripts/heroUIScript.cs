using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class heroUIScript : MonoBehaviour {
	
	public Transform affichage;
	public Transform affCanvas;
	
	public void setAffichagePV(int pv)
	{
		affichage.GetComponent<RectTransform>().sizeDelta = new Vector2(pv,affichage.GetComponent<RectTransform>().sizeDelta.y);
	}
	
	public void preventFlip()
	{
		affCanvas.localScale= new Vector3(affCanvas.localScale.x*-1,affCanvas.localScale.y,affCanvas.localScale.z);
	}

}
