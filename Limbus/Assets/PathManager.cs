using UnityEngine;
using System.Collections;

public class PathManager :MonoBehaviour {
	
	// Privates
	private Transform[] waypoints;	
	private int index = 0;
	private Vector3 direction;
	
	// Publics
	public float speed = 10f;
	public GameObject allWaypoints;
	
	#region Getters & Setters
	public Transform[] Waypoints
	{
		get { 
			return waypoints; }
	}
	
	public int Index
	{
		get { return index; }
		set { index = value; }
	}
	
	public Transform CurrentWaypoint
	{
		get { 
			return waypoints[index]; }
	}
	
	#endregion
	
	void Awake()
	{
		waypoints = new Transform[allWaypoints.GetComponentsInChildren<Transform>().Length - 1];
		
		int i = 0;
		foreach (Transform child in allWaypoints.transform)
		{   waypoints[i] = child;
			i++;
		}
	}
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		/*vehicle.transform.rotation = Quaternion.Lerp(vehicle.transform.rotation, Quaternion.LookRotation(direction),  Time.deltaTime*15);
		
		vehicle.transform.Translate(Vector3.forward * speed * Time.deltaTime);
		//vehicle.transform.rotation = Quaternion.Lerp(vehicle.transform.rotation, waypoints[index].rotation, Time.deltaTime * 10);
		if (Vector3.Distance(vehicle.transform.position, waypoints[index].position) < 1f)
		{
			index++;
			//vehicle.transform.LookAt(waypoints[index].position);
			direction = waypoints[index].position - vehicle.transform.position ;
			direction = direction.normalized;
		}
		//vehicle.transform.Translate(*/
	}
	
	public void CheckNewWaypoint(Vector3 position)
	{		
		if (index < waypoints.Length-1)
		{
			// If distance is small enough ...
			if (Vector3.Distance(position, waypoints[index].position) < 1f)
			{
				// if is any other waypoint ...
				if (Index < waypoints.Length)
				{
					index++;
				}
			}
		}
	}
}
