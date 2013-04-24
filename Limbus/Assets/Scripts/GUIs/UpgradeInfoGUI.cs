using UnityEngine;
using System.Collections;

public class UpgradeInfoGUI : MonoBehaviour {
	
	public UILabel name, price, info;
	
	public void SetName(string name)
	{
		this.name.text = name;
	}
	
	public void SetPrice(int price)
	{
		
		this.price.text = @"Upgrade: " + price.ToString() + @"$";
	}
	
	public void SetInfo(int towerLevel)
	{
		if (towerLevel + 1 < 3)
		{
			this.info.text = @"# Upgrade on " + (towerLevel + 2) + ". level";
		}
		else
		{
			this.info.text = @"# No other upgrade available .";
		}
			
	}
}
