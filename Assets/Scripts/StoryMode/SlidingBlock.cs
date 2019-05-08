using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingBlock : MonoBehaviour {

	Rigidbody rb;
	Transform tr;
	Vector3 initialPos;
	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		rb.constraints = RigidbodyConstraints.FreezeAll;
		tr = gameObject.GetComponent<Transform> ();
		initialPos = tr.localPosition;
		print (initialPos.ToString ());
	}

	//only move when player touches!
	void OnCollisionEnter(Collision other) {
	
		if (other.gameObject.tag == "Player")
			rb.constraints = RigidbodyConstraints.FreezeRotation;
	}
	void OnCollisionExit(Collision other) {

		if (other.gameObject.tag == "Player")
			rb.constraints = RigidbodyConstraints.FreezeAll;
	}


	public void setInitialPos() {

	
		tr.localPosition= new Vector3(initialPos.x, tr.localPosition.y, initialPos.z);

	}
	// Update is called once per frame
	void Update () {
		
	}
}
