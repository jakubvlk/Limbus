using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour
{	
	public Transform groundRespawn, airRespawn, waterRespawn;
	public GameObject[] allUnits;
	
	// Privates
	private float respawnTimer;	// timer after each unit
	private float waveRespawnTimer; // timer after end of wave
	
	[SerializeField] 	// show in Editor - BUT when set from editor, setter is NOT called
	private int lifes = 5, money = 1000, score = 0;
	
	private Wave[] wave;
	private LoadWaves loadWaves;
	private InGameGUI inGameGUI;
	private bool noUnitsLeft;
	
	public int NumOfActiveUnits { get; set; }
	
	#region Getters & Setters
	
	public int NumOfWaves { 
		get;
		private set;
	}
	
	public bool Winner {
		get;
		private set;
	}
	
	public bool Looser {
		get;
		set;
	}
	
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
		
		loadWaves = GameObject.FindObjectOfType(typeof(LoadWaves)) as LoadWaves;
		loadWaves.LoadAllWaves();
		wave = loadWaves.Wave;
		NumOfActiveUnits = wave[0].Unit.Count;
		
		Winner = Looser = false;
		
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
					if (NumOfWaves < loadWaves.NumOfWaves)
						NumOfActiveUnits = wave[NumOfWaves].Unit.Count;
				}
			}
			else
			{
				waveRespawnTimer = Time.time;
			}
		}
		else
		{
			// WINNER
			if (EndOfWave())
			{
				Winner = true;
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
