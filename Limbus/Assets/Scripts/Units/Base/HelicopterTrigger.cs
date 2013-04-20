using UnityEngine;
using System.Collections;

public class HelicopterTrigger : Base {
	
	protected override void OnTriggerEnter(Collider other)
	{
		if (other.tag == "AirEnemy")
		{
        	Destroy(other.gameObject);
			gameMaster.Lifes--;
		}
		
		if (gameMaster.Lifes <= 0)
		{
			print(@"********GAME OVER************");
		}
    }
}
