using UnityEngine;
using System.Collections;

public class DefaultUnit : MonoBehaviour {
	
	public float maxHealth;
	
	protected Transform myTransform;
	protected float currHealth;
	protected GameMaster gameMaster;
	
	// Use this for initialization
	protected virtual void Start ()
	{
		myTransform = transform;
		currHealth = maxHealth;
		gameMaster = GameObject.FindObjectOfType(typeof(GameMaster)) as GameMaster;
	}
	
	
}
