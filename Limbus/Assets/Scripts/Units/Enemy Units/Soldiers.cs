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
		if(!animation.isPlaying)
		animation.Play();
	}
	
	
	protected override void InitSound ()
	{
		
	}
}
