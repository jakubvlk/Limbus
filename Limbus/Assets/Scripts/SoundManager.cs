using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	private float audio1Volume = 1f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void FadeIn(AudioSource audioSource, float duration)
	{
	}
	
	public void FadeOut(AudioSource audioSource, float duration)
	{
		if(audio1Volume > 0.1f)
	    {
	        audio1Volume -= 0.1f * Time.deltaTime;
	        audioSource.volume = audio1Volume;
	    }
	}
}
