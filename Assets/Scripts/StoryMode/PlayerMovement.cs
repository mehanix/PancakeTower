using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed;
	public float maxSpeed=5F;
	private Vector3 spawnPoint,finalPos;
	private Vector3 input;
	public GameObject particles;
	public LevelManager manager;
	bool hasSwitchedLevel=false;
	GameObject floor;
	Rigidbody rb;
	public CoordinateManager cm;
	int level;
	bool shouldLerp=false;
	int collisionNr;
	//tumbling
	Vector3 pivotDirection;
	bool isPlayerRotating;
	//float startTime,finalTime,timeSinceStart,percentageFallen;
	public int coinCount,totalCoinsPerLevel;


	void Start () {
		rb = GetComponent<Rigidbody>();
		manager = manager.GetComponent<LevelManager>();
		cm = GameObject.Find("GameManager").GetComponent<CoordinateManager> ();

		transform.position = new Vector3 (-4.5F, 0, 0);
		spawnPoint = transform.position;
		InvokeRepeating ("CheckForFalling", 0f, 1F);
		totalCoinsPerLevel = 4;
	

	}

		void Update () {

//		input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
//
//		if (input.x != 0) {
//		
//			rb.angularVelocity = Vector3.right * input.x * 1F;
//		}
//
//
//		if (input.z != 0) {
//
//			rb.angularVelocity = Vector3.forward * -input.z * 1F;
//		}


		//CheckForFalling ();



		if (collisionNr == 0) {
			rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;


		}
		if (collisionNr >0 ) {
			rb.constraints = RigidbodyConstraints.FreezeRotation;


		}
//	
//		if (Input.GetAxisRaw ("Horizontal") != 0) {
//			GetComponent<Rigidbody> ().angularVelocity = Vector3.right * Input.GetAxisRaw ("Horizontal") * 1F;
//
//		} else if (Input.GetAxisRaw ("Vertical") != 0) {
//			GetComponent<Rigidbody> ().angularVelocity = Vector3.forward * -Input.GetAxisRaw ("Vertical") * 1F;
//
//		}
		
//			if (shouldLerp == true) {
//			
//				timeSinceStart = Time.time - startTime;
//				percentageFallen = timeSinceStart / finalTime;
//				transform.position= Vector3.Lerp(transform.position,finalPos,percentageFallen);
//				if (transform.position==finalPos)
//					shouldLerp = false;
//			}
		
		//}

		//verificat daca sunt in aer

		//Tumbling!
		//determ pivot point in jurul caruia se roteste cubul in fct de directia in care se merge
		// => in fct de tasta apasata. daca nu se apasa nimic, ramane 0
		
//	pivotDirection = Vector3.zero;
//
//		if (Input.GetKey(KeyCode.UpArrow))
//			pivotDirection = Vector3.forward;
//
//		if (Input.GetKey(KeyCode.DownArrow))
//			pivotDirection = Vector3.back;
//
//		if (Input.GetKey(KeyCode.LeftArrow))
//			pivotDirection = Vector3.left;
//
//		if (Input.GetKey(KeyCode.RightArrow))
//			pivotDirection = Vector3.right;
//		
//		if (pivotDirection != Vector3.zero && isPlayerRotating == false) {
//		
//			rotate (pivotDirection);
//		}
	}
//	void rotate(Vector3 pivotDirection) {
//	
//		isPlayerRotating = true;
//
//	}

	void FixedUpdate() {
	
		//adaugat forta doar cand viteza scade sub o limita ->snappier movement :D
		if (manager.paused == false) {
			input = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
			if (rb.velocity.magnitude < maxSpeed)
				rb.AddForce (input * moveSpeed);
		}
	}


	void CheckForFalling()
	{
	//	print("isFalling=" + GameObject.Find ("FloorNo" + manager.getCurrentLevel ().ToString ()).GetComponent<FloorFalling> ().isFalling.ToString());
		//print ("Hasswitchedlevel=" + hasSwitchedLevel.ToString ());

			if (manager.getCurrentLevel () > 0)

				if (GameObject.Find ("FloorNo " + manager.getCurrentLevel ().ToString ()).GetComponent<FloorFalling> ().isFalling == false && hasSwitchedLevel == true) {
					toggleMovement (true);
					hasSwitchedLevel = false;
					




		}
	}



	void OnCollisionEnter(Collision other)
	{
		collisionNr++;


	}

	void OnCollisionExit(Collision other)
	{
		collisionNr--;
	}
	void PlayerDeath()
	{
		//animatii fancy pt moarte + mutat la spawn
		//rb.velocity = Vector3.zero;
		GameObject explosion = GameObject.Instantiate(particles, transform.position, Quaternion.identity) as GameObject;
		gameObject.GetComponent<AudioSource>().PlayOneShot (gameObject.GetComponent<AudioSource> ().clip);

		transform.position = spawnPoint; 
		Destroy (explosion, 2.0f);
	}

	void OnTriggerEnter(Collider other)
	{
		//verificat terminat nivel.. trecerea la niv urmator se face in gamemanager

		if (other.transform.tag == "Enemy") PlayerDeath();

		if (other.transform.tag == "SpeedPad")
			maxSpeed = 10;

		if (other.transform.tag == "Coin") {
			coinCount++;


		}

		if (other.transform.tag == "Goal")
		{
				hasSwitchedLevel = true;
				toggleMovement (false);

				//lerping!
//				startTime=Time.time;
//				finalTime = startTime + 3F;
				print ("current level:" + manager.getCurrentLevel ());
				finalPos = cm.PlayerSpawnPoints [manager.getCurrentLevel ()+1];
				finalPos.y = transform.position.y + 6.5F;
				transform.position = finalPos;
			//	shouldLerp = true;


				//rb.AddForce (new Vector3 (0, 20, 0) * moveSpeed);

				level = manager.getCurrentLevel () + 1;
				transform.position = new Vector3(cm.PlayerSpawnPoints[level].x, 
				transform.position.y,
				cm.PlayerSpawnPoints[level].z);


				coinCount = 0;
				manager.nextLevel ();
				
				int newcoins = GameObject.FindGameObjectsWithTag ("Coin").Length;
				totalCoinsPerLevel = newcoins - totalCoinsPerLevel;

				floor=GameObject.Find("FloorNo " + manager.getCurrentLevel().ToString());
				spawnPoint = transform.position;
				spawnPoint.y += 1;






		}
	}
	void toggleMovement(bool shouldMove)
	{
		//print ("toggled movement: " + shouldMove.ToString());
		rb.useGravity = shouldMove;
		GetComponent<BoxCollider> ().isTrigger = !shouldMove;
		//GetComponent<BoxCollider> ().enabled = shouldMove;
		if (shouldMove == false)
			rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
		else
			rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;

	}

}
