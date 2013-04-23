using UnityEngine;
using System.Collections;

public class LooserGUI : MonoBehaviour {

	public void OnClick(GameObject btnObj)
	{
		switch (btnObj.name)
		{
			case "btn_LoadGame":
				print("*********** Load Game **********************");
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
