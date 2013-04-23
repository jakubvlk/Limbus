using UnityEngine;
using System.Collections;

public class Base : DefaultUnit 
{
	
	protected virtual void OnTriggerEnter(Collider other)
	{
		if (other.tag == "GroundEnemy" || other.tag == "WaterEnemy")
		{
			gameMaster.NumOfActiveUnits--;
        	Destroy(other.gameObject);
			gameMaster.Lifes--;
		}
		
		Invoke("CheckLooser", 3);
    }
	
	protected void CheckLooser()
	{
		if (gameMaster.Lifes <= 0)
		{
			gameMaster.Looser = true;
		}
	}
}