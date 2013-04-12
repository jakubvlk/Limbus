using UnityEngine;
using System.Collections;
using System;

public class EnemyUnit : ExtendedUnit {
	
	// TODO: 1) pridat zvuk pohybu tanku - pravdepodobne je treba pridat vic audiosourcu... pak ale vsude O.o popremyslet a vygooglit!!!
	// najit lepsi zvuk pohybu tanku 2) udelat fire rate za minutu (specialne na tank je to duuulezite)
	
	public float speed, turnSpeed = 3f;
	public int reward;
	public AudioClip movingSound;

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
		
		// TODO: proc se hybe smerem ke kulometu???
		base.Update();
		Move();
	}
	
	protected override void OnTriggerStay(Collider other)
	{		
		if (!myTarget && other.tag == "Tower")
		{
			myTarget = other.transform;
		}
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
	
	private void AddScoreAndMoney()
	{
		
		// TODO: jak pocitat skore???
		//gameMaster.score += ;
		
		gameMaster.money += reward;
		gameMaster.UpdateGUI();
	}
	
	protected override void Destroy()
	{
		base.Destroy();
		AddScoreAndMoney();
	}
}
