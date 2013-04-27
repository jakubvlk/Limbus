using UnityEngine;
using System.Collections;

public class Soldiers : EnemyUnit {

	// Use this for initialization
	void Start () {
		
		base.Start();		
		myTransform.LookAt(pathManager.CurrentWaypoint.position);
	}
	
	// Update is called once per frame
	void Update () {
		if(!animation.isPlaying)
		animation.Play();
	}
	
	void Awake()
	{
		
	}
	
	private void Move ()
	{
	
		
	}
	
	protected override void InitSound ()
	{
		
	}
}
