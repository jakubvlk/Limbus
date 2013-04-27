using UnityEngine;
using System.Collections;

public class FlashLight : MonoBehaviour {
	
	public bool lightOn = true;
	public float waitingTime;
	private float switchTime;

	// Use this for initialization
	void Start () {
		light.enabled = lightOn;
		switchTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {		
		if((switchTime+waitingTime) < Time.time){
			light.enabled = ! light.enabled;
			switchTime = Time.time;
		}
	}	
	
}
