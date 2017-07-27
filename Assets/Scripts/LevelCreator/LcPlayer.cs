using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LcPlayer : MonoBehaviour {

	public float moveSpeed;
	public float maxSpeed=5F;
	private Vector3 input;
	Rigidbody rb;
	public static bool shouldMove;
	int level;
	Vector3 currentPos, newPos;
	CharacterController cc;

	CreatorUI creatorui;
	void Start () {
		rb = GetComponent<Rigidbody>();
		shouldMove = false;
		rb.constraints |= RigidbodyConstraints.FreezePositionY;
		creatorui = GameObject.Find("CreatorManager").GetComponent<CreatorUI> ();

	}

	void FixedUpdate () {

		if (shouldMove == true) {

			input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
			if (rb.velocity.magnitude < maxSpeed)
				rb.AddForce (input * moveSpeed);



		}

		//adaugat forta doar cand viteza scade sub o limita ->snappier movement :D



	}




	void OnTriggerEnter(Collider other)
	{
		//verificat terminat nivel.. trecerea la niv urmator se face in gamemanager



		if (other.transform.tag == "Goal")
		{
			creatorui.setEditMode (true);
			creatorui.ShowToolbox = true;
			creatorui.playMode = true;
			shouldMove = false;
		
		}

		if (other.transform.tag == "Coin") {

			if (shouldMove) {
			
				Timer.score += 200;
				Destroy (other.gameObject);
			}
		
		}
	}
	void toggleMovement(bool shouldMove)
	{
		print ("toggled movement: " + shouldMove.ToString());
		rb.useGravity = shouldMove;
		GetComponent<BoxCollider> ().enabled = shouldMove;
		if (shouldMove == false)
			rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
		else
			rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;

	}
}
