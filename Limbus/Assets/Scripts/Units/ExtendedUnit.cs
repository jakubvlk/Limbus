using UnityEngine;
using System.Collections;

public class ExtendedUnit : DefaultUnit {
	
	public Transform turret;
	public float turretRotationSpeed;
	//public float power;
	public GameObject explosion;
	public GameObject fireEffect;
	public AudioClip fireSound, turretRotationSound;
	public int fireRatePerMin;
	public GameObject projectile;
	public Transform[] muzzleTransform;
	
	protected Transform myTarget;	
	protected Quaternion desiredRotation;
	
	private AudioSource fireAS, turretRotationAS;
	protected float firePause, fireTimer;
	
	
	// Unit is not moving, targeting - nothing, just standing on place
	public bool Virtual
	{
		get;
		set;
	}
	
	// Use this for initialization
	protected override void Start ()
	{
		base.Start();
		InitSound();
		
		firePause = 0;
		fireTimer = Time.time;
		
		Virtual = false;
	}
	
	protected virtual void Update()
	{
		if (myTarget && !Virtual)
		{
			if (turret)
			{
				CalculateAimPosition(myTarget.position - turret.position);
				turret.rotation = Quaternion.Lerp(turret.rotation, desiredRotation, Time.deltaTime * turretRotationSpeed);
				
				// Play turret rotation sound
				if (!turretRotationAS.isPlaying)
					turretRotationAS.Play();
			}
			
			float direction;			
			// Restriction of fire angle
			// TMP - so far, not all of units have turrets!
			// TODO: fix it :-)
			if (turret)
			{
				Vector3 dir = (myTarget.position - turret.position).normalized;			
				direction = Vector3.Dot(dir, turret.forward);
			}
			else
			{
				Vector3 dir = (myTarget.position - myTransform.position).normalized;			
				direction = Vector3.Dot(dir, myTransform.forward);
			}
			
			// If timer is OK and the target is infront of us!
			if (Time.time >= fireTimer + firePause && direction > 0.9f)
			{
				Fire();
			}
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
			if(turret && turretRotationSound)
			turretRotationAS.Stop();
		}
	}

	protected virtual void Fire ()
	{
		// Play fire sound
		if (!fireAS.isPlaying)
			fireAS.Play();
		
		// Set timer and fire pause
		fireTimer = Time.time;
		if (fireRatePerMin <= 0)
			fireRatePerMin = 1;
		firePause = 60f / fireRatePerMin;
		
		// Fire missle
		if (projectile)
		{
			int muzzleIndex = Random.Range(0, muzzleTransform.Length);
			GameObject newMissile = Instantiate(projectile, muzzleTransform[muzzleIndex].position, muzzleTransform[muzzleIndex].rotation) as GameObject;
			newMissile.GetComponent<Missile>().MyTarget = myTarget;
			
			//Fire effect
			if(fireEffect)
			{
				GameObject newFireEffect = Instantiate(fireEffect, muzzleTransform[muzzleIndex].position, muzzleTransform[muzzleIndex].rotation) as GameObject;
				Destroy(newFireEffect,1);
			}
			
		}
		
		
		
		// Get a hit
		//myTarget.GetComponent<ExtendedUnit>().GetHit(power);
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

	public virtual void Destroy()
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
