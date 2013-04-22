using UnityEngine;
using System.Collections;

public class Tower : ExtendedUnit
{	
	public int price;
	
	public float rankModifier = 1.5f;
	public float updatePrice;
		
	private Rank rank;
	
	protected override void Start()
	{
		base.Start();
		rank = gameObject.GetComponentInChildren<Rank>();
	}
	
	public void Promote()
	{
		
		if (rank.RankValue < 2)
			rank.Promote();
	}
}
