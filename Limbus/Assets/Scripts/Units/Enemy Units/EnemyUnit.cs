using UnityEngine;
using System.Collections;
using System;

public class EnemyUnit : MonoBehaviour {
	
	public float speed, reward, turnSpeed = 3f;
	
	protected Transform myTransform;
	protected PathManager pathManager;
	
	#region Getters & Setters
	
	public Vector3 RespawnPoint {
		get {
			return pathManager.CurrentWaypoint.position; }
	}
	
	#endregion
	
	void Awake()
	{
		print(GameObject.FindObjectOfType(typeof(PathManager)) as PathManager);
		pathManager = GameObject.FindObjectOfType(typeof(PathManager)) as PathManager;
		
		// Automaticly assign correct waypoints to the pathManager.
		switch (gameObject.tag)
		{
			case "GroundEnemy":
				pathManager.InitWaypoints(GameObject.Find("GroundWaypoints"));
				break;
			
			default:
				throw new NotImplementedException();
				// add water, air tag and complete this case...
				break;
		}
	}
	
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void Move ()
	{
		throw new System.NotImplementedException ();
	}
}
