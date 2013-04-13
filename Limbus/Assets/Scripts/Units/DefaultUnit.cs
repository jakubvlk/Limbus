using UnityEngine;
using System.Collections;

public class DefaultUnit : MonoBehaviour {
	
	public float maxHealth;
	
	protected Transform myTransform;
	protected float currHealth;
	
	// Use this for initialization
	protected virtual void Start ()
	{
		myTransform = transform;
		currHealth = maxHealth;
	}
	
	
}
