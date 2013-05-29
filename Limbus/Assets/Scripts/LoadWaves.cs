using UnityEngine;
using System.Collections;
using System.Xml;

public class LoadWaves : MonoBehaviour {
	
	public TextAsset xmlFile;
	public int NumOfWaves {	get; private set; }
	
	public Wave[] Wave { get; set; }

	public void LoadAllWaves ()
	{
		// XML loading
		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
	  	xmlDoc.LoadXml(xmlFile.text); // load the file.
		
		XmlNodeList levelsList = xmlDoc.GetElementsByTagName(@"wave"); // array of the level nodes.
		
		// Number of waves		
		NumOfWaves = 0;
		foreach (XmlNode levelInfo in levelsList)
  		{
			NumOfWaves++;
		}
		Wave = new Wave[NumOfWaves];
		
		// Reading waves stats
		int index = 0;		
		foreach (XmlNode levelInfo in levelsList)
  		{
			Wave wave = new Wave();
			
			XmlNodeList levelcontent = levelInfo.ChildNodes;
		    foreach (XmlNode levelsItems in levelInfo) // levels itens nodes.
		   	{
				if(levelsItems.Name == @"name")
				{	
					wave.Name = levelsItems.InnerText;
				}
				// Reading unit stats
				else if (levelsItems.Name == @"unit")
				{
					Unit unit = new Unit();
					//XmlNodeList levelcontent = levelInfo.ChildNodes;
					foreach (XmlNode theUnit in levelsItems)
					{
						if (theUnit.Name == @"unitIndex")
						{
							unit.UnitIndex = int.Parse(theUnit.InnerText);
						}
						else if (theUnit.Name == @"name")
						{
							unit.Name = theUnit.InnerText;
						}
						else if (theUnit.Name == @"count")
						{
							unit.Count = int.Parse(theUnit.InnerText);
						}
						else if (theUnit.Name == @"respawnIn")
						{
							unit.RespawnIn = float.Parse(theUnit.InnerText);
						}						
					}
					
					wave.Unit = unit;
				}
				else if(levelsItems.Name == @"pauseAfter")
				{	
					wave.PauseAfer = float.Parse(levelsItems.InnerText);
				}				
			}
			
			Wave[index] = wave;
			index++;
		}	
	}
}

public struct Wave 
	{
		public string Name { get; set; }
		public Unit Unit { get; set; }
		public float PauseAfer { get; set; }	
	}
	
	public struct Unit 
	{
		public int UnitIndex { get; set; }
		public string Name { get; set; }
		public	int Count { get; set; }
		public	float RespawnIn { get; set; }
	}

