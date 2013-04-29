using UnityEngine;
using System.Collections;

public class GroupOfSoldiers : EnemyUnit {
		
		
	// Update is called once per frame
	protected override void Update () {
	
			base.Update();
			if(transform.childCount == 0){
			//need to destroy parent object, because single soldiers are standalone objects
			gameMaster.NumOfActiveUnits--;	
			print("Group destroyed left : " + gameMaster.NumOfActiveUnits);
			Destroy(gameObject);
		}
	}
	
	
	
}
