using UnityEngine;
using System.Collections;

public class EnemyUnit : MonoBehaviour {
	
	public float speed, reward, turnSpeed = 3f;
	
	protected Transform[] waypoints;
	protected Transform myTransform;
	protected PathManager pathManager;
	
	
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
