using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour {
	
	// Publics
	public int levelNumber;
	public Transform groundRespawn, airRespawn, waterRespawn;
	public GameObject[] allUnits;
	
	// Privates
	private float respawnTimer;
	
	// struktura pro vsechny potrebne informace o respawnu???	
	private int unitIndex = 0;
	private	int count = 5;
	private	float nextRespawnIn = 1.5f;
	// --------------------------------------
	
	// Use this for initialization
	void Start ()
	{		
		respawnTimer = Time.time;
	
		unitIndex = 0;
	}	
	
	// Update is called once per frame
	void Update ()
	{		
		Respawn ();
	}
	
	void Respawn ()
	{
		if (count > 0 && Time.time >= respawnTimer + nextRespawnIn)
		{
			GameObject newVehicle = (GameObject) Instantiate((GameObject)allUnits[unitIndex], Vector3.zero, Quaternion.identity);
			newVehicle.transform.position = newVehicle.GetComponent<EnemyUnit>().RespawnPoint;
			
			respawnTimer = Time.time;
			count--;
		}
	}
}
