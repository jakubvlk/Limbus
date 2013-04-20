using UnityEngine;
using System.Collections;

public class ChooseLevelGUI : MonoBehaviour 
{	
	// On button click
	public void OnClick(GameObject btnObj)
	{
		switch (btnObj.name)
			{
				case "btn_Lvl1":
					Application.LoadLevel(2);
					break;
				case "btn_Lvl2":
					Application.LoadLevel(3);
					break;
				case "btn_Lvl3":
					Application.LoadLevel(4);
					break;
				case "btn_Back":
					Application.LoadLevel(0);
					break;
			}		
	}
}
