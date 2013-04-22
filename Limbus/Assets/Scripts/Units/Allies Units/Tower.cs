using UnityEngine;
using System.Collections;

public class Tower : ExtendedUnit
{	
	public int price;
	
	public float[] rankModifiers;
		
	private Rank rank;
	private float upgradePrice;
	
	protected override void Start()
	{
		base.Start();
		rank = gameObject.GetComponentInChildren<Rank>();
		rankModifiers = new float[3];
		rankModifiers[0] = 1f;
		rankModifiers[1] = 1.15f;
		rankModifiers[2] = 1.3f;
	}
	
	public string Promote()
	{
		if (rank.RankValue < 2)
		{
			int newPrice = Mathf.FloorToInt(price * rankModifiers[rank.RankValue + 1]);
			if (gameMaster.Money >=  newPrice)
			{			
				gameMaster.Money -= newPrice;
				
				// better damage
				projectile.GetComponent<Missile>().damage = projectile.GetComponent<Missile>().damage * rankModifiers[rank.RankValue + 1];
				
				// more health
				maxHealth = maxHealth * rankModifiers[rank.RankValue + 1];
				currHealth = maxHealth;
				
				// Bigger radius
				GetComponent<SphereCollider>().radius =  GetComponent<SphereCollider>().radius * rankModifiers[rank.RankValue + 1]; 
				
				// It has to be like last!!! (coz RankValue + 1)
				rank.Promote();
				
				return string.Empty;
			}
			else
			{
				return InGameGUI.NOT_ENOUGH_MONEY;
			}
		}
		else
		{
			return InGameGUI.UPGRADE_MAX_LVL;
		}
	}
}
