using UnityEngine;
using System.Collections;

public class Tower : ExtendedUnit
{	
	public int price;
	
	public float[] rankModifiers;
	
	public string name;
	public string info;
		
	private Rank rank;
	private float upgradePrice;
	
	public int TowerLevel
	{
		get { return rank.RankValue; }
	}
	
	protected override void Start()
	{
		base.Start();
		rank = gameObject.GetComponentInChildren<Rank>();
		rankModifiers = new float[3];
		rankModifiers[0] = 1f;
		rankModifiers[1] = 1.15f;
		rankModifiers[2] = 1.3f;
	}
	
	public int NewPrice()
	{
		if (rank.RankValue + 1 == rankModifiers.Length)
			return 0;
		
		return Mathf.FloorToInt(price * rankModifiers[rank.RankValue + 1]);
	}
	
	public string Promote()
	{
		if (rank.RankValue < 2)
		{
			int newPrice = NewPrice();
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
