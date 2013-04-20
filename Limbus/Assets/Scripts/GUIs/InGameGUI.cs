using UnityEngine;
using System.Collections;

public class InGameGUI : MonoBehaviour
{
	public Transform placementPlanesRoot;
	public Material hoverMat;
	public LayerMask placementLayerMask;
	
	public Color onColor, offColor;
	public GameObject[] allStructures;
	public UISlicedSprite[] buildBtnGraphics;
	
	public GameObject pauseMenu;	
	
	public UILabel waveText, scoreText, lifesText, moneyText;
	
	// Private
	private Material originalMat;
	private GameObject lastHitObj;
	
	private GameObject[] towersPool;
	
	private int structureIndex;
	
	private GameMaster gameMaster;
	
	private GUIMode guiMode;
	
	
	private enum GUIMode 
	{
		GUIMode_Paused,
		GUIMode_Running
	}
	
	
	// Use this for initialization
	void Start ()
	{
		// If structure index is -1, than no button is pressed
		structureIndex = -1;
		gameMaster = GameObject.FindObjectOfType(typeof(GameMaster)) as GameMaster;
		UpdateGUI();
		towersPool = MakeTowersPool();
		guiMode = GUIMode.GUIMode_Running;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (guiMode == GUIMode.GUIMode_Running)
		{
			HighlightPlacements ();
	
			// MOUSE
			// building of tower
			if (Input.GetMouseButtonDown(0) && lastHitObj)
			{
				BuyAndBuild ();
			}
			// canceling of the building mode
			else if (Input.GetMouseButtonDown(1))
			{
				CancelBuildMode ();
			}
			
			//KEYBOARD
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				DoPauseToggle();
			}
		}
		else if (guiMode == GUIMode.GUIMode_Paused)
		{
			
		}
	}

	void HighlightPlacements ()
	{
		// Cast ray from camera through the current position of a mouse
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit, 1000, placementLayerMask))
		{
			if (lastHitObj)
			{
				lastHitObj.renderer.material = originalMat;
			}
			
			lastHitObj = hit.collider.gameObject;
			originalMat = lastHitObj.renderer.material;
			lastHitObj.renderer.material = hoverMat;
		}
		else
		{
			if (lastHitObj)
			{
				lastHitObj.renderer.material = originalMat;
				lastHitObj = null;
			}
		}
	}
	
	// On button click
	public void OnClick(GameObject btnObj)
	{
		if ( guiMode == GUIMode.GUIMode_Running)
		{
			// IF you change name of the button in editor you have to change name here as well!!
			switch (btnObj.name)
			{
				case "btn_MachineGun":
					structureIndex = 0;
					SetActivePlacementPlanes(true);	
					break;
				case "btn_Anti-aircraft_Satellite":
					structureIndex = 1;
					SetActivePlacementPlanes(true);	
					break;
				case "btn_GrenadeLauncher":
					structureIndex = 2;
					SetActivePlacementPlanes(true);	
					break;
				case "btn_Pause":
					DoPauseToggle();
					break;
				case "btn_DoubleSpeed":
					DoubleTime();
					break;
			}		
			
		}
		else if (guiMode == GUIMode.GUIMode_Paused)
		{
			switch (btnObj.name)
			{
				case "btn_Resume":
					DoPauseToggle();
					break;
				case "btn_Save":
					print("*************Game Saved!!!*********");
					break;
				case "btn_Quit":
					Application.LoadLevel(0);
					break;
			}
		}
	}
	
	public void UpdateGUI()
	{
		foreach (UISlicedSprite theBtnGraphic in buildBtnGraphics)
		{
			theBtnGraphic.color = offColor;	
		}
		
		// If is selected something...
		if (structureIndex != -1)
		{
			buildBtnGraphics[structureIndex].color = onColor;
		}
		
		waveText.text = @"Wave: " + (gameMaster.NumOfWaves + 1);
		scoreText.text = @"Score: " + gameMaster.Score;
		moneyText.text = @"Money: " + gameMaster.Money + @"$";
		lifesText.text = @"Lifes: " + gameMaster.Lifes;
	}
	
	private void SetActivePlacementPlanes(bool val)
	{
		placementPlanesRoot.gameObject.SetActive(val);
		UpdateGUI();
	}

	void BuyAndBuild ()
	{
		// enough money?..
		if (gameMaster.Money >= allStructures[structureIndex].GetComponent<Tower>().price)
		{			
			if (lastHitObj.tag == "PlacementPlane_Open")
			{
				GameObject newStructure = towersPool[structureIndex];
				newStructure.transform.position = lastHitObj.transform.position;
					//(GameObject)Instantiate(allStructures[structureIndex], lastHitObj.transform.position, Quaternion.identity);
				lastHitObj.tag = "PlacementPlane_Closed";
				
				gameMaster.Money -= allStructures[structureIndex].GetComponent<Tower>().price;
				UpdateGUI();
			}
		}
		else
		{
			print(@"**********Not enough money!**********");
		}
	}

	void CancelBuildMode ()
	{
		structureIndex = -1;
		if (lastHitObj)
		{
			lastHitObj.renderer.material = originalMat;
			lastHitObj = null;
		}
		
		SetActivePlacementPlanes(false);
	}
	
	// TODO: pause of the sound isn't the best... http://answers.unity3d.com/questions/7544/how-do-i-pause-my-game.html
	private void DoPauseToggle()
	{
		if(Time.timeScale > 0)
		{
        	Time.timeScale = 0;
			AudioListener.pause = true;
			pauseMenu.SetActive(true);
			guiMode = GUIMode.GUIMode_Paused;
			CancelBuildMode ();
		}
		else
		{
			Time.timeScale = 1;
			AudioListener.pause = false;
			pauseMenu.SetActive(false);
			guiMode = GUIMode.GUIMode_Running;
		}
	}
	
	private void DoubleTime()
	{
    	if(Time.timeScale == 1)
		{
        	Time.timeScale = 2;
		}
		else
		{
			Time.timeScale = 1;
		}
	}	
		
	private GameObject[] MakeTowersPool()
	{
		GameObject[] towersPool = new GameObject[allStructures.Length];
		
		for (int i = 0; i < towersPool.Length; i++)
		{
			towersPool[i] = (GameObject)Instantiate(allStructures[i], Vector3.zero, Quaternion.identity);
		}
		
		return towersPool;
	}
}


