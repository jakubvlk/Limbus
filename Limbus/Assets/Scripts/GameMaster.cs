using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour
{	
	public int levelNumber;
	public Transform groundRespawn, airRespawn, waterRespawn;
	public GameObject[] allUnits;
	
	// Privates
	private float respawnTimer;	// timer after each unit
	private float waveRespawnTimer; // timer after end of wave
	//private int numOfWaves;
	private int lifes, money, score;
	private Wave[] wave;
	private LoadWaves loadWaves;
	private InGameGUI inGameGUI;
	
	public int NumOfActiveUnits { get; set; }
	
	public int NumOfWaves {
		get;
		private set;
	}
	
	#region Getters & Setters
	
	public int Lifes {
		get
		{
			return lifes;
		}
		
		set
		{ 
			lifes = value;
			inGameGUI.UpdateGUI();
		}
	}
	
	public int Money {
		get
		{
			return money;
		}
		
		set
		{ 
			money = value;
			inGameGUI.UpdateGUI();
		}
	}
	
	public int Score {
		get
		{
			return score;
		}
		
		set
		{ 
			score = value;
			inGameGUI.UpdateGUI();
		}
	}
	
	
	
	#endregion
	
	// Use this for initialization
	void Start ()
	{		
		Time.timeScale = 1f;
		respawnTimer = waveRespawnTimer = Time.time;
		
		inGameGUI = GameObject.FindObjectOfType(typeof(InGameGUI)) as InGameGUI;
		NumOfWaves = 0;
		Lifes = 5;
		Money = 1000;
		Score = 0;
		
		loadWaves = GameObject.FindObjectOfType(typeof(LoadWaves)) as LoadWaves;
		loadWaves.LoadAllWaves();
		wave = loadWaves.Wave;
		NumOfActiveUnits = wave[0].Unit.Count;
		
		//UpdateGUI();
	}	
	
	// Update is called once per frame
	void Update ()
	{		
		Respawn ();
	}
	
	void Respawn ()
	{
		// If there is any other wave
		if (NumOfWaves < loadWaves.NumOfWaves)
		{
			if (wave[NumOfWaves].Unit.Count > 0 && Time.time >= respawnTimer + wave[NumOfWaves].Unit.RespawnIn)
			{
				GameObject newUnit = (GameObject) Instantiate((GameObject)allUnits[wave[NumOfWaves].Unit.UnitIndex], Vector3.zero, Quaternion.identity);
				newUnit.transform.position = newUnit.GetComponent<EnemyUnit>().RespawnPoint;
				
				respawnTimer = Time.time;
				
				// decrement num of unit to go
				Unit tmp = wave[NumOfWaves].Unit;
				tmp.Count--;
				wave[NumOfWaves].Unit = tmp;
			}
			
			// If one wave ended...
			if (EndOfWave())
			{
				// If timer is OK...
				if (Time.time > waveRespawnTimer + wave[NumOfWaves].PauseAfer)
				{
					NumOfWaves++;
					NumOfActiveUnits = wave[NumOfWaves].Unit.Count;
					//UpdateGUI();
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
