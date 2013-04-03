using UnityEngine;
using System.Collections;

public class GroundUnit : EnemyUnit
{	
	// TODO: volat funkce v startu uz v EnemyUnit???
	// TODO: pridat do groundUnit pathManager jako komponentu (je nutne za behu??? - je protoze do prefabu se da pretahnout jenom prefab),
				//  pridat do PathManageru dalsi 2 pole na waypointy - pak se podle tagu rozhodnout, ktere skutecne ulozit do waypoints[]
	// 			- je nutne to mit teda jako objekt, kdyz to pridam ke kazde jednotce????
	// Use this for initialization
	void Start ()
	{
		myTransform = transform;
		
		//look at first waypoint
		myTransform.LookAt(pathManager.CurrentWaypoint.position);
	}
	
	// Update is called once per frame
	void Update () 
	{
		Move();
	}
	
	private void Move ()
	{
		// Check if waypoint is changing
		pathManager.CheckNewWaypoint(myTransform.position);
		
		// Rotation
		Quaternion desiredRotation = Quaternion.LookRotation(pathManager.CurrentWaypoint.position - myTransform.position);
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation, desiredRotation, Time.deltaTime * turnSpeed);
		
		// Moving forward
		myTransform.Translate(Vector3.forward * speed * Time.deltaTime);		
	}
}
