using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

	public GameObject myExplosion;
	public float mySpeed = 10;
	public float minDistance;
	public float damage = 10;
	
	private float lifeTimer;

	public Transform MyTarget
	{
		get;
		set;
	}

	// Use this for initialization
	void Start ()
	{
		minDistance = 5;
		lifeTimer = Time.time;
	}

	// Update is called once per frame
	void Update ()
	{
		transform.Translate(Vector3.forward * Time.deltaTime * mySpeed);
		if(MyTarget)
		{
			if(Vector3.Distance(MyTarget.position, transform.position) < minDistance)
			{
				MyTarget.GetComponent<ExtendedUnit>().GetHit(damage);
				Explode();
			}

			transform.LookAt(MyTarget);
		}
		
		// If missile is older than 10s, if will explode
		if ( ( Time.time - lifeTimer ) > 10f)
		{
			Explode();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Terrain")
		{
			Explode();
		}
	}

	private void Explode()
	{
		Instantiate(myExplosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}