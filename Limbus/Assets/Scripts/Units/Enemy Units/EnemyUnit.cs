using UnityEngine;
using System.Collections;
using System;

public class EnemyUnit : ExtendedUnit {
	
	public float speed, turnSpeed = 3f;
	public int reward;

	protected PathManager pathManager;
	
	private GameMaster gameMaster;
	
	#region Getters & Setters
	
	public Vector3 RespawnPoint
	{
		get 
		{
			return pathManager.CurrentWaypoint.position; 
		}
	}
	
	#endregion
	
	void Awake()
	{
		pathManager = GameObject.FindObjectOfType(typeof(PathManager)) as PathManager;
		
		// Automaticly assign correct waypoints to the pathManager.
		switch (gameObject.tag)
		{
			case "GroundEnemy":
				pathManager.InitWaypoints(GameObject.Find("GroundWaypoints"));
				break;
			case "WaterEnemy":
				pathManager.InitWaypoints(GameObject.Find("WaterWaypoints"));
				break;
			case "AirEnemy":
				pathManager.InitWaypoints(GameObject.Find("AirWaypoints"));
				break;
			
			default:
				throw new NotImplementedException();
				break;
		}
	}
	
	// Use this for initialization
	virtual protected void Start ()
	{
		base.Start();
		
		myTransform.LookAt(pathManager.CurrentWaypoint.position);
		gameMaster = GameObject.FindObjectOfType(typeof(GameMaster)) as GameMaster;
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
	
	private void AddScore()
	{
		gameMaster.score += reward;
		gameMaster.UpdateGUI();
	}
	
	protected override void Die()
	{
		Destroy();
		AddScore();
	}
}
