using UnityEngine;
using System.Collections;

public class GroundUnit : EnemyUnit
{	
	// Use this for initialization
	void Start ()
	{
		pathManager = (PathManager) GameObject.FindObjectOfType(typeof(PathManager));
		waypoints = pathManager.Waypoints;
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
