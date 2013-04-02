using UnityEngine;
using System.Collections;

public class InGameGUI : MonoBehaviour {
	
	//TODO: defaultne nic nevybrat; pridat jeste jednu vez; pri kliku na btn, zmenit structuruIndex
	
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
	
	
	
	// Use this for initialization
	void Start ()
	{
		structureIndex = 0;
		UpdateGUI();
	}
	
	// Update is called once per frame
	void Update ()
	{
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
		
		if (Input.GetMouseButtonDown(0) && lastHitObj)
		{
			if (lastHitObj.tag == "PlacementPlane_Open")
			{
				GameObject newStructure = (GameObject)Instantiate(allStructures[structureIndex], lastHitObj.transform.position, Quaternion.identity);
				Vector3 localEulerAngles = newStructure.transform.localEulerAngles;
				localEulerAngles.y = Random.Range(0, 360);
				newStructure.transform.localEulerAngles = localEulerAngles;
				lastHitObj.tag = "PlacementPlane_Closed";
			}
		}
	}
	
	// On button click
	public void OnClick()
	{
		print("click!!!");
	}
	
	private void UpdateGUI()
	{
		foreach (UISlicedSprite theBtnGraphic in buildBtnGraphics)
		{
			theBtnGraphic.color = offColor;	
		}
		
		buildBtnGraphics[structureIndex].color = onColor;
	}
}
