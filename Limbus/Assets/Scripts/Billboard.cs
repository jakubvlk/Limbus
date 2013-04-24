using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{
	private Transform myTransform;
	private Camera mainCamera;
	
	void Start ()
	{
		myTransform = transform;
		mainCamera = Camera.mainCamera;
	}
	
	// Update is called once per frame
	void Update ()
	{
		myTransform.LookAt(mainCamera.transform.position);
		Quaternion tmpRot = myTransform.rotation;
		tmpRot.x = tmpRot.z = 0;		
		myTransform.rotation = tmpRot;
	}
}
	