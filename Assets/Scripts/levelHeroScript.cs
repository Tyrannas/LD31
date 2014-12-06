using UnityEngine;
using System.Collections;

public class levelHeroScript : MonoBehaviour {
	
	heroAttackScript attackScript;
	MoveHeroScript moveScript;
	
	int levelHero;
	
	const int level1pv=10;
	const int level2pv=15;
	const int level3pv=20;
	const int level1puissance=1;
	const int level2puissance=2;
	const int level3puissance=3;
	public Sprite sprite1;
	public Sprite sprite2;
	public Sprite sprite3;
	
	void Start()
	{
		attackScript = GameObject.Find("Hero").GetComponent<heroAttackScript>();
		moveScript = GameObject.Find("Hero").GetComponent<MoveHeroScript>();
		SetLevelHero(1);
	}
	public void SetLevelHero(int levelvoulu)
	{
		Sprite sprtTemp;
		int coefFacingRight=-1;
		if(moveScript.getFacingRight())
		{
			coefFacingRight=1;
		}
		if(levelvoulu == 1)
		{
			levelHero=1;
			attackScript.setPv(level1pv);
			attackScript.setPuissance(level1puissance);
			this.transform.localScale = new Vector2(1*coefFacingRight,1);
			sprtTemp = sprite1;		
			
		}
		else if(levelvoulu == 2)
		{
			levelHero=2;
			attackScript.setPv(level2pv);
			attackScript.setPuissance(level2puissance);
			this.transform.localScale = new Vector2(2*coefFacingRight,1.5f);
			sprtTemp = sprite2;
		}
		else
		{
			levelHero=3;
			attackScript.setPv(level3pv);
			attackScript.setPuissance(level3puissance);
			this.transform.localScale = new Vector2(4*coefFacingRight,3);
			sprtTemp = sprite3;
		}

			GameObject.Find("heroSprite").GetComponent<SpriteRenderer>().sprite = sprtTemp;

			
	}

}