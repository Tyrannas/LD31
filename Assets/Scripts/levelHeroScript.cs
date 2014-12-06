using UnityEngine;
using System.Collections;

public class levelHeroScript : MonoBehaviour {
	
	heroAttackScript attackScript;
	
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
		SetLevelHero(1);
	}
	
	public void SetLevelHero(int levelvoulu)
	{
		if(levelvoulu == 1)
		{
			levelHero=1;
			attackScript.setPv(level1pv);
			attackScript.setPuissance(level1puissance);
			this.transform.localScale = new Vector2(1,1);
			GameObject.Find("heroSprite").GetComponent<SpriteRenderer>().sprite = sprite1;
			
		}
		else if(levelvoulu == 2)
		{
			levelHero=2;
			attackScript.setPv(level2pv);
			attackScript.setPuissance(level2puissance);
			this.transform.localScale = new Vector2(2,1.5f);
			GameObject.Find("heroSprite").GetComponent<SpriteRenderer>().sprite = sprite2;
		}
		else if(levelvoulu == 3)
		{
			levelHero=3;
			attackScript.setPv(level3pv);
			attackScript.setPuissance(level3puissance);
			this.transform.localScale = new Vector2(4,3);
			GameObject.Find("heroSprite").GetComponent<SpriteRenderer>().sprite = sprite3;
		}
	}



}