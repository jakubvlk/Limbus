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
	
	
	// Private
	private Material originalMat;
	private GameObject lastHitObj;
	
	private int structureIndex;
	
	private GameMaster gameMaster;
	
	
	
	// Use this for initialization
	void Start ()
	{
		// If structure index is -1, than no button is pressed
		structureIndex = -1;
		gameMaster = GameObject.FindObjectOfType(typeof(GameMaster)) as GameMaster;
		UpdateGUI();
	}
	
	// Update is called once per frame
	void Update ()
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
		
		// building of tower
		if (Input.GetMouseButtonDown(0) && lastHitObj)
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
		
		// canceling of the building mode
		if (Input.GetMouseButtonDown(1))
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
	}
	
	// On button click
	public void OnClick(GameObject btnObj)
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
		}	
		
		SetActivePlacementPlanes(true);		
		UpdateGUI();
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
	
	private void Buy(GameObject btnObj)
	{
		
		
		// not enough money?
		if (gameMaster.money < allStructures[structureIndex].GetComponent<Tower>().price)
		{
			structureIndex = -1;
			SetActivePlacementPlanes(false);
		}
		else
		{
			
			SetActivePlacementPlanes(true);
			
			
			
			
		}
	}
}
