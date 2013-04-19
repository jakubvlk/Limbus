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
	
	
	// Private
	private Material originalMat;
	private GameObject lastHitObj;
	
	private int structureIndex;
	
	private GameMaster gameMaster;
	
	private GUIMode guiMode;
	
	
	
	// Use this for initialization
	void Start ()
	{
		// If structure index is -1, than no button is pressed
		structureIndex = -1;
		gameMaster = GameObject.FindObjectOfType(typeof(GameMaster)) as GameMaster;
		UpdateGUI();
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
					break;
				case "btn_Anti-aircraft_Satellite":
					structureIndex = 1;
					break;
				case "btn_GrenadeLauncher":
					structureIndex = 2;
					break;
				case "btn_Pause":
					DoPauseToggle();
					break;
				case "btn_DoubleSpeed":
					DoubleTime();
					break;
				
				SetActivePlacementPlanes(true);		
				UpdateGUI();
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
					print("*************Quit to main menu!!!*********");
					break;
			}
		}
	}
	
	private void UpdateGUI()
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
	}
	
	private void SetActivePlacementPlanes(bool val)
	{
		placementPlanesRoot.gameObject.SetActive(val);
	}

	void BuyAndBuild ()
	{
		// enough money?..
		if (gameMaster.money >= allStructures[structureIndex].GetComponent<Tower>().price)
		{			
			if (lastHitObj.tag == "PlacementPlane_Open")
			{
				GameObject newStructure = (GameObject)Instantiate(allStructures[structureIndex], lastHitObj.transform.position, Quaternion.identity);
				lastHitObj.tag = "PlacementPlane_Closed";
				
				gameMaster.money -= allStructures[structureIndex].GetComponent<Tower>().price;
				gameMaster.UpdateGUI();
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
		
		UpdateGUI();
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
	
	private enum GUIMode 
	{
		GUIMode_Paused,
		GUIMode_Running
	}
}


