using UnityEngine;
using System.Collections;

public class monstreScript : MonoBehaviour {
	
	public int typeMonstre;
	
	int puissance;
	int pv;
	
	// Use this for initialization
	void Start () {
		if(typeMonstre == 1)
		{
			puissance =1;
			pv = 4;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public int monsterIsHit(int puissancehero)
	{	
		pv-=puissancehero;
		if(pv<=0)
		{
			Destroy(this.gameObject);
			return typeMonstre;
		}
		return 0;
	}
}
