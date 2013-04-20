using UnityEngine;
using System.Collections;

public class Base : DefaultUnit {
	
	protected GameMaster gameMaster;
	
	// Use this for initialization
	protected override void Start ()
	{
		gameMaster = GameObject.FindObjectOfType(typeof(GameMaster)) as GameMaster;
	}
	
	protected virtual void OnTriggerEnter(Collider other)
	{
		if (other.tag == "GroundEnemy" || other.tag == "WaterEnemy")
		{
			gameMaster.NumOfActiveUnits--;
        	Destroy(other.gameObject);
			gameMaster.Lifes--;
		}
		
		if (gameMaster.Lifes <= 0)
		{
			print(@"********GAME OVER************");
		}
    }
}