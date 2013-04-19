using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour
{	
	public int lifes = 5;
	public int money = 1000;
	public int score = 0;
	public int levelNumber;
	public Transform groundRespawn, airRespawn, waterRespawn;
	public GameObject[] allUnits;
	
	// GUI
	public UILabel waveText, scoreText, lifesText, moneyText;
	
	// Privates
	private float respawnTimer, waveRespawnTimer;
	private int numOfWaves;
	private Wave[] wave;
	private LoadWaves loadWaves;
	
	// Use this for initialization
	void Start ()
	{		
		respawnTimer = waveRespawnTimer = Time.time;
		numOfWaves = 0;
		loadWaves = GameObject.FindObjectOfType(typeof(LoadWaves)) as LoadWaves;
		loadWaves.LoadAllWaves();
		wave = loadWaves.Wave;
		
		UpdateGUI();
	}	
	
	// Update is called once per frame
	void Update ()
	{		
		Respawn ();
	}
	
	public void UpdateGUI()
	{
		waveText.text = @"Wave: " + (numOfWaves + 1);
		scoreText.text = @"Score: " + score;
		moneyText.text = @"Money: " + money + @" $";
		lifesText.text = @"Lifes: " + lifes;
	}
	
	void Respawn ()
	{
		// If it isn't last wave ...
		if (numOfWaves < loadWaves.NumOfWaves)
		{
			if (wave[numOfWaves].Unit.Count > 0 && Time.time >= respawnTimer + wave[numOfWaves].Unit.RespawnIn)
			{
				GameObject newUnit = (GameObject) Instantiate((GameObject)allUnits[wave[numOfWaves].Unit.UnitIndex], Vector3.zero, Quaternion.identity);
				newUnit.transform.position = newUnit.GetComponent<EnemyUnit>().RespawnPoint;
				
				respawnTimer = Time.time;
				
				// decrement num of unit to go
				Unit tmp = wave[numOfWaves].Unit;
				tmp.Count--;
				wave[numOfWaves].Unit = tmp;
			}
			
			// If one wave ended...
			if (wave[numOfWaves].Unit.Count == 0)
			{
				//waveRespawnTimer = Time.time + wave[numOfWaves].PauseAfer;
				
				if (Time.time > respawnTimer + wave[numOfWaves].PauseAfer)
				{
					numOfWaves++;
					UpdateGUI();
				}
			}
		}
	}
}
