using UnityEngine;
using System.Collections;

public class GroupOfSoldiers : EnemyUnit {
		
		
	// Update is called once per frame
	protected override void Update ()
	{	
		base.Update();
		if(transform.childCount == 0)
		{
			//need to destroy parent object, because single soldiers are standalone objects
			if(gameMaster.NumOfActiveUnits > 0)
			{
				gameMaster.NumOfActiveUnits--;
			}

			Destroy(gameObject);
		}
		
		
	}
}
