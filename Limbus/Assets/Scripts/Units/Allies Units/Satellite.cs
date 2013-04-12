using UnityEngine;
using System.Collections;

public class Satellite : Tower 
{
	
	protected override void OnTriggerStay (Collider other)
	{
		if (!myTarget && (other.tag == "AirEnemy"))
		{
			myTarget = other.transform;
		}
	}
}
