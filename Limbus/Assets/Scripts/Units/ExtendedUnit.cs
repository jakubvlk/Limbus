using UnityEngine;
using System.Collections;

public class ExtendedUnit : DefaultUnit {
	
	public Transform turret;
	public float turretRotationSpeed;
	public float power;
	public GameObject explosion;
	public AudioClip fireSound, turretRotationSound;
	public int fireRatePerMin;
	
	// TODO: dat pak na protected
	public Transform myTarget;	
	protected Quaternion desiredRotation;
	
	private AudioSource fireAS, turretRotationAS;
	private float firePause, fireTimer;
	
	// Use this for initialization
	protected override void Start ()
	{
		base.Start();
		InitSound();
		
		firePause = 0;
		fireTimer = Time.time;
	}
	
	protected virtual void Update()
	{
		if (myTarget)
		{
			if (turret)
			{
				CalculateAimPosition(myTarget.position - turret.position);
				turret.rotation = Quaternion.Lerp(turret.rotation, desiredRotation, Time.deltaTime * turretRotationSpeed);
				
				// Play turret rotation sound
				if (!turretRotationAS.isPlaying)
					turretRotationAS.Play();
			}
			
			if (Time.time >= fireTimer + firePause)
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
			
			// Stop sounds
			// TODO: asi pro nektere jednotky jo a pro nektere ne (delo musi doznit v dalce, vojaci musi prestat strilet)
			//fireAS.Stop();
			turretRotationAS.Stop();
		}
	}

	private void Fire ()
	{
		// Play fire sound
		if (!fireAS.isPlaying)
			fireAS.Play();
		
		// Set timer and fire pause
		fireTimer = Time.time;
		if (fireRatePerMin <= 0)
			fireRatePerMin = 1;
		firePause = 60f / fireRatePerMin;
		
		// Get a hit
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
			Destroy();
		}
	}

	protected virtual void Destroy()
	{
		Instantiate(explosion, myTransform.position, Quaternion.identity);
		Destroy(gameObject);
	}
	
	protected virtual void InitSound ()
	{
		// Sound of fire
		fireAS = gameObject.AddComponent<AudioSource>();
		fireAS.clip = fireSound;
		fireAS.minDistance = 100;
		fireAS.playOnAwake = false;
		
		// Sound of turret rotation
		turretRotationAS = gameObject.AddComponent<AudioSource>();
		turretRotationAS.clip = turretRotationSound;
		turretRotationAS.minDistance = 20;
		turretRotationAS.playOnAwake = false;
	}
}
