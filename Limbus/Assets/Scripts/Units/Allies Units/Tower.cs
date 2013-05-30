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
	
	#region Getters & Setters
	
	public int TowerLevel
	{
		get { return rank.RankValue; }
	}
	
	#endregion
	
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
		
		return Mathf.FloorToInt((price / 2f) * rankModifiers[rank.RankValue + 1]);
	}
	
	public string Promote()
	{
		if (rank.RankValue < 2)
		{
			int newPrice = NewPrice();
			if (gameMaster.Money >=  newPrice)
			{			
				gameMaster.Money -= newPrice;
				
				// higher damage
				//float damage = projectile.GetComponent<Missile>().damage;
				//projectile.GetComponent<Missile>().damage = damage * rankModifiers[rank.RankValue + 1];
				
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
	
	protected override void Fire ()
	{
		// Play fire sound
		if (fireAS && !fireAS.isPlaying)
			fireAS.Play();
		
		// Set timer and fire pause
		fireTimer = Time.time;
		if (fireRatePerMin <= 0)
			fireRatePerMin = 1;
		firePause = 60f / fireRatePerMin;
		
		// Fire missle
		if (projectile)
		{
			int muzzleIndex = Random.Range(0, muzzleTransform.Length);
			GameObject newMissile = Instantiate(projectile, muzzleTransform[muzzleIndex].position, muzzleTransform[muzzleIndex].rotation) as GameObject;
			newMissile.GetComponent<Missile>().MyTarget = myTarget;
			newMissile.GetComponent<Missile>().damage = newMissile.GetComponent<Missile>().damage * rankModifiers[rank.RankValue];
			
			//Fire effect
			if(fireEffect)
			{
				GameObject newFireEffect = Instantiate(fireEffect, muzzleTransform[muzzleIndex].position, muzzleTransform[muzzleIndex].rotation) as GameObject;
				Destroy(newFireEffect,1);
			}
		}
		
		
		
		// Get a hit
		//myTarget.GetComponent<ExtendedUnit>().GetHit(power);
	}
}
