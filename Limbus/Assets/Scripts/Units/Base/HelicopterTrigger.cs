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
		
		Invoke("CheckLooser", 3);
    }
}
