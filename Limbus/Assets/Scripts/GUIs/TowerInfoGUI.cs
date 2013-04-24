using UnityEngine;
using System.Collections;

public class TowerInfoGUI : MonoBehaviour 
{	
	public UILabel name, price, info;
	
	public void SetName(string name)
	{
		this.name.text = name;
	}
	
	public void SetPrice(int price)
	{
		
		this.price.text = @"Cost: " + price.ToString() + @"$";
	}
	
	public void SetInfo(string info)
	{
		this.info.text = info;
	}
}
