using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
    
    public GameObject myExplosion;
    public float mySpeed = 10;
    
    private float minDistance;
    
    public Transform MyTarget {
   	 get;
   	 set;
    }
    
    // Use this for initialization
    void Start ()
    {
   	 minDistance = 1;
    }
    
    // Update is called once per frame
    void Update ()
    {
   	 
   	 if(MyTarget){
   		 
   		 transform.Translate(Vector3.forward * Time.deltaTime * mySpeed);
   		 if(Vector3.Distance(MyTarget.position, transform.position) < minDistance)
   		 Explode();
   	 
   		 if(MyTarget)
   		 {
   			 transform.LookAt(MyTarget);
   		 }
   		 else
   		 {
   			 Explode();
   		 }
   	 }else{
   	 Destroy(gameObject);
   	 }
    }
    
    void OnTriggerEnter(Collider other)
    {
   	 if(other.gameObject.tag == "Tower")
   	 {
   		 //Explode();
   	 }
    }
    
    private void Explode()
    {
   	 Instantiate(myExplosion, transform.position, Quaternion.identity);
   	 Destroy(gameObject);
    }
}
