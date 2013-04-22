using UnityEngine;
using System.Collections;

public class WinnerGUI : MonoBehaviour
{	
	public void OnClick(GameObject btnObj)
	{
		switch (btnObj.name)
		{
			case "btn_NextLvl":
				// If it isn't last level...
				if (Application.loadedLevel < 4)
				{
					Application.LoadLevel(Application.loadedLevel + 1);
				}
				break;
			case "btn_Repeat":
				Application.LoadLevel(Application.loadedLevel);
				break;
			case "btn_Quit":
				// Go to main menu
				Application.LoadLevel(0);
				break;
		}	
	}
}
