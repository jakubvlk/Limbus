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
	private float respawnTimer;	// timer after each unit
	private float waveRespawnTimer; // timer after end of wave
	private int numOfWaves;
	private Wave[] wave;
	private LoadWaves loadWaves;
	
	public int NumOfActiveUnits { get; set; }
	
	// Use this for initialization
	void Start ()
	{		
		Time.timeScale = 1f;
		respawnTimer = waveRespawnTimer = Time.time;
		numOfWaves = 0;
		loadWaves = GameObject.FindObjectOfType(typeof(LoadWaves)) as LoadWaves;
		loadWaves.LoadAllWaves();
		wave = loadWaves.Wave;
		NumOfActiveUnits = wave[0].Unit.Count;
		
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
		// If there is any other wave
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
			if (EndOfWave())
			{
				// If timer is OK...
				if (Time.time > waveRespawnTimer + wave[numOfWaves].PauseAfer)
				{
					numOfWaves++;
					NumOfActiveUnits = wave[numOfWaves].Unit.Count;
					UpdateGUI();
				}
			}
			else
			{
				waveRespawnTimer = Time.time;
			}
		}
	}
	
	private bool EndOfWave()
	{
		if (NumOfActiveUnits == 0)
		{
			return true;
		}
		
		return false;
	}
}
