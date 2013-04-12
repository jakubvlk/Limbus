using UnityEngine;
using System.Collections;

public class DefaultUnit : MonoBehaviour {
	
	public float maxHealth;
	
	protected Transform myTransform;
	protected float currHealth;
	
	// Use this for initialization
	virtual protected void Start ()
	{
		myTransform = transform;
		currHealth = maxHealth;
	}
	
	
}
