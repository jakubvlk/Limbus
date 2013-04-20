using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour
{	
	// On button click
	public void OnClick(GameObject btnObj)
	{
		switch (btnObj.name)
			{
				case "btn_NewGame":
					Application.LoadLevel(1);
					break;
				case "btn_LoadGame":
					print("*******************Load Game*****************");
					break;
				case "btn_Stats":
					print("*******************Stats*****************");
					break;
				case "btn_Quit":
					Application.Quit();
					break;
			}		
	}
	
	
}
