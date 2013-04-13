using UnityEngine;
using System.Collections;

public class HelicopterTrigger : Base {
	
	protected override void OnTriggerEnter(Collider other)
	{
		if (other.tag == "AirEnemy")
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
