using UnityEngine;
using System.Collections;

public class Tower : ExtendedUnit
{	
	public int price;
	
	public float rankModifier = 1.5f;
	public float updatePrice;
		
	private Rank rank;
	
	private void Promote()
	{
		rank.Promote();
	}
}
