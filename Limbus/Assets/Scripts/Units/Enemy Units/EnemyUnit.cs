using UnityEngine;
using System.Collections;
using System;

public class EnemyUnit : ExtendedUnit
{	
	public float speed, turnSpeed = 3f;
	public int reward;
	public AudioClip movingSound;
	
	protected PathManager pathManager;
	
	
	// AS = AudioSource
	private AudioSource movingAS;
	
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
		}
	}
	
	// Use this for initialization
	protected override void Start ()
	{
		base.Start();
		
		myTransform.LookAt(pathManager.CurrentWaypoint.position);
		
		// Play moving units sound
		if(movingAS){
		movingAS.Play();
		}
	}
	
	// Update is called once per frame
	protected override void Update ()
	{
		base.Update();
		Move();
	}
	
	protected override void OnTriggerStay(Collider other)
	{		
		if (!myTarget && other.tag == "Tower" && !other.GetComponent<ExtendedUnit>().Virtual)
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
		
		gameMaster.Money += reward;
	}
	
	public override void Destroy()
	{
		base.Destroy();
		AddScoreAndMoney();
		gameMaster.NumOfActiveUnits--;
	}

	protected override void InitSound ()
	{
		base.InitSound();
		
		// Sound of moving of units
		movingAS = gameObject.AddComponent<AudioSource>();
		movingAS.clip = movingSound;
		movingAS.minDistance = 100;
		movingAS.loop = true;
	}
}
