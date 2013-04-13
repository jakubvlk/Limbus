#pragma strict

var myExplosion : GameObject;
var myTarget : Transform;
var myRange : float = 10;
var mySpeed : float = 10;

var myDamageAmount : float = 25;

private var myDist : float;

function Start () {

}

function Update () 
{
	transform.Translate(Vector3.forward * Time.deltaTime * mySpeed);
	myDist += Time.deltaTime * mySpeed;
	if(myDist >= myRange)
		Explode();
	
	if(myTarget)
	{
		transform.LookAt(myTarget);
	}
	else
	{
		Explode();
	}
}

function OnTriggerEnter(other : Collider)
{
	if(other.gameObject.tag == "Air Enemy")
	{
		Explode();
		other.gameObject.SendMessage("TakeDamage", myDamageAmount, SendMessageOptions.DontRequireReceiver);
	}
}

function Explode()
{
	Instantiate(myExplosion, transform.position, Quaternion.identity);
	Destroy(gameObject);
}