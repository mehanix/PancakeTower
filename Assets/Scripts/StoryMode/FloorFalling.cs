using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorFalling : MonoBehaviour {

	public bool isFalling = true;
	float currentHeight, previousHeight, distance;
	string nameCheck;
	Vector3 finalPos;

	float startTime,finalTime,timeSinceStart,percentageFallen;
	// Use this for initialization
	void Start () {
		byte r = (byte) (Random.Range(220F,240F));
		byte g = (byte) (Random.Range (191F, 202F));
		byte b = (byte) (Random.Range (115F, 150F));

		Renderer rend = GetComponent<Renderer>();
		rend.material.SetColor ("_Color", new Color32(r,g,b,255));
		string[] splitName = gameObject.name.Split ();
		nameCheck = splitName [0] + " " + (int.Parse(splitName [1]) - 1).ToString ();
		print (nameCheck);
		finalPos = new Vector3 (transform.position.x, transform.position.y-3.5F, transform.position.z);
		startTime = Time.time;
		finalTime = startTime + 5F;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//updateFalling ();
		if(isFalling==true){
			timeSinceStart = Time.time - startTime;
			percentageFallen = timeSinceStart / finalTime;
			transform.position= Vector3.Lerp(transform.position,finalPos,percentageFallen);


		}

		if (transform.position.y-finalPos.y <0.5F) {
			isFalling = false;
			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
		}
	}

	 void updateFalling()
	{
//		currentHeight = transform.position.y;
//		distance = currentHeight - previousHeight;
//
//		if(distance <1)
//		{
//			isFalling = false;
//			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
//
//		} else 
//		{
//			isFalling = true;
//		}



//		if (GetComponent<Rigidbody> ().velocity.y > -0.5F ) {
//			isFalling = false;
//			GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
//		} else {
//			isFalling = true;
//		}
	
	}

	void OnCollisionEnter(Collision other) {
	
//		print (nameCheck);
//		print (other.gameObject.tag == "Floor".ToString ());
//
//		if (other.gameObject.name == nameCheck && other.gameObject.tag == "Floor" && isFalling==true) {
//			isFalling = false;
//		 	GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
//			//this.enabled = false;
//
//		}
	}

	public bool getFalling()
	{
		return isFalling;
	}
}