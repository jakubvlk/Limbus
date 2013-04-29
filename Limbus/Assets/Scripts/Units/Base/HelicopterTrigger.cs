using UnityEngine;
using System.Collections;

public class HelicopterTrigger : Base {
	
	protected override void OnTriggerEnter(Collider other)
	{
		if (other.tag == "AirEnemy")
		{
        	AfterReachingBase(other);
		}
		
		Invoke("CheckLooser", 3);
    }
}
