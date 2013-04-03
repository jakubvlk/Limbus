using UnityEngine;
using System.Collections;
using System.Xml;

public class LoadWaves : MonoBehaviour {
	
	public TextAsset xmlFile;
	
	// Use this for initialization
	void Start ()
	{
		GetWaves();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void GetWaves ()
	{
		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
	  	xmlDoc.LoadXml(xmlFile.text); // load the file.
	 	XmlNodeList levelsList = xmlDoc.GetElementsByTagName("wave"); // array of the level nodes.
		
		foreach (XmlNode levelInfo in levelsList)
  		{
			XmlNodeList levelcontent = levelInfo.ChildNodes;
		   
			foreach (XmlNode levelsItems in levelcontent) // levels itens nodes.
		   	{
				//print(levelsItems.InnerText);
			}
			
			//print("-----------------------------------------------");
		}
	}
	
	/*struct Wave 
	{
		private int unitIndex = 0;
	private	int count = 5;
	private	float nextRespawnIn = 1.5f;
	}*/
}
