using UnityEngine;
using System.Collections;

public class Rank : MonoBehaviour
{
	private Transform target, myTransform;
	
	public Texture rank3Texture;
	
	public int RankValue {
		get;
		private set;
	}
	
	void Start()
	{
		RankValue = 0;
	}

	public void Promote()
	{
		enabled = true;
		
		RankValue++;
		
		// 0 no texture, 1 = original texture, 2 = next texture
		if (RankValue == 2)
			ChangeTexture();
	}

	private void ChangeTexture ()
	{
		renderer.material.SetTexture("Rank3", rank3Texture);
	}
}
