using UnityEngine;
using System.Collections;

public class Base : DefaultUnit {
	
	private GameMaster gameMaster;
	
	// Use this for initialization
	protected override void Start ()
	{
		gameMaster = GameObject.FindObjectOfType(typeof(GameMaster)) as GameMaster;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "GroundEnemy" || other.tag == "WaterEnemy" || other.tag == "AirEnemy")
		{
        	Destroy(other.gameObject);
			gameMaster.lifes--;
			gameMaster.UpdateGUI();
		}
		
		if (gameMaster.lifes <= 0)
		{
			print(@"********GAME OVER************");
		}
    }
}