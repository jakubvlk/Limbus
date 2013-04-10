using UnityEngine;
using System.Collections;

public class Tower : ExtendedUnit {
	
	//TODO:
	// nataceni allies vezi na projizdejici jednotky - pridat collider na vez, dat to na trigger, vyzkouset
	
	public Transform turret;
	public float fireRate;
	public float rotationSpeed;
	public float firePauseTime;
	
	private Transform myTarget;
	private Quaternion desiredRotation;
	private Transform myTransform;
	
	// Use this for initialization
	void Start ()
	{
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (myTarget)
		{
			CalculateAimPosition(myTarget.position - turret.position);
			myTransform.rotation = Quaternion.Lerp(turret.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
		}
	}
	
	private void CalculateAimPosition(Vector3 targetPos)
	{
		desiredRotation = Quaternion.LookRotation(targetPos);
		desiredRotation.x = 0;
		desiredRotation.z = 0;
	}
	
	private void OnTriggerStay(Collider other)
	{
		if (!myTarget && (other.tag == "GroundEnemy" || other.tag == "WaterEnemy"))
		{
			myTarget = other.transform;
		}
	}
	
	private void OnTriggerExit(Collider other)
	{
		if (other.transform == myTarget)
		{
			myTarget = null;
		}
	}
}
