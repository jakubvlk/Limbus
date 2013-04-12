using UnityEngine;
using System.Collections;

public class ExtendedUnit : DefaultUnit {
	
	public Transform turret;
	public float turretRotationSpeed;
	public float power;
	public GameObject explosion;
	
	protected Transform myTarget;	
	protected Quaternion desiredRotation;
	
	// Use this for initialization
	virtual protected void Start ()
	{
		base.Start();
		
	}
	
	private void Update()
	{
		if (myTarget)
		{
			CalculateAimPosition(myTarget.position - turret.position);
			myTransform.rotation = Quaternion.Lerp(turret.rotation, desiredRotation, Time.deltaTime * turretRotationSpeed);
					
			Fire();
		}
	}
	
	protected virtual void OnTriggerStay(Collider other)
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
			audio.Pause();
		}
	}

	private void Fire ()
	{
		if (!audio.isPlaying)
			audio.Play();
		myTarget.GetComponent<ExtendedUnit>().GetHit(power);
	}
	
	protected void CalculateAimPosition(Vector3 targetPos)
	{
		desiredRotation = Quaternion.LookRotation(targetPos);
		desiredRotation.x = 0;
		desiredRotation.z = 0;
	}
	
	public void GetHit(float healthLost)
	{
		currHealth -= healthLost;
		if (currHealth <= 0)
		{
			Die();
		}
	}

	protected virtual void Die()
	{
		Destroy();
	}
	
	protected void Destroy()
	{
		Instantiate(explosion, myTransform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
