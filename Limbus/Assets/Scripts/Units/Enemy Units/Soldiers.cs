using UnityEngine;
using System.Collections;

public class Soldiers : EnemyUnit {

	// Use this for initialization
	protected override void Start () {
		
		firePause = 0;
		fireTimer = Time.time;		
		Virtual = false;
		myTransform = transform;
		currHealth = maxHealth;
		gameMaster = GameObject.FindObjectOfType(typeof(GameMaster)) as GameMaster;
		
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
		if(!animation.isPlaying)
		animation.Play();
	}
	
	
	protected override void FireInFrontOf ()
	{
		float direction;			
		// Restriction of fire angle
		// TMP - so far, not all of units have turrets!
		// TODO: fix it :-)
		if (turret)
		{
			Vector3 dir = (myTarget.position - turret.position).normalized;			
			direction = Vector3.Dot(dir, turret.forward);
		}
		else
		{
			Vector3 dir = (myTarget.position - myTransform.position).normalized;			
			direction = Vector3.Dot(dir, myTransform.forward);
		}
		
		// If timer is OK and the target is infront of us!
		if (Time.time >= fireTimer + firePause && direction > 0)
		{
			Fire();
		}
	}
	
	
}
