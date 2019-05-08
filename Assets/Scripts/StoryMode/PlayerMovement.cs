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
	public int coinCount,totalCoinsPerLevel,deaths;


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

	

		if (collisionNr == 0) {
			rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;


		}
		if (collisionNr > 0) {
			rb.constraints = RigidbodyConstraints.FreezeRotation;


		}
	}
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
;

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
		GameObject explosion = GameObject.Instantiate(particles, transform.position, Quaternion.identity) as GameObject;
		gameObject.GetComponent<AudioSource>().PlayOneShot (gameObject.GetComponent<AudioSource> ().clip);

		transform.position = new Vector3(spawnPoint.x,transform.position.y,spawnPoint.z); 
		deaths++;
		Destroy (explosion, 2.0f);
	}

	void OnTriggerEnter(Collider other)
	{
		//verificat terminat nivel.. trecerea la niv urmator se face in gamemanager


		if (other.transform.tag == "Enemy")
			PlayerDeath ();
		if (other.transform.tag == "SpeedPad")
			maxSpeed = 10;

		if (other.transform.tag == "Coin") {
			coinCount++;


		}

		if (other.transform.tag == "Goal")
		{
			
				
				hasSwitchedLevel = true;
				toggleMovement (false);

				print ("current level:" + manager.getCurrentLevel ());
				finalPos = cm.PlayerSpawnPoints [manager.getCurrentLevel () + 1];
				finalPos.y = transform.position.y + 6.5F;
				transform.position = finalPos;
			


		

				level = manager.getCurrentLevel () + 1;
				transform.position = new Vector3 (cm.PlayerSpawnPoints [level].x, 
					transform.position.y,
					cm.PlayerSpawnPoints [level].z);


				coinCount = 0;
				manager.nextLevel ();

				int newcoins = GameObject.FindGameObjectsWithTag ("Coin").Length;
				totalCoinsPerLevel = newcoins - totalCoinsPerLevel;

				floor = GameObject.Find ("FloorNo " + manager.getCurrentLevel ().ToString ());
				spawnPoint = transform.position;
				spawnPoint.y += 1;
			}

		if (other.transform.tag == "PMFinish") {
		

			manager.paused = true;
			toggleMovement (false);
			manager.shouldShowVictoryWindow = true;
		}
				







	}
	void toggleMovement(bool shouldMove)
	{
		if (shouldMove == false)
			rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
		else
			rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
		

		rb.useGravity = shouldMove;
		GetComponent<BoxCollider> ().isTrigger = !shouldMove;

	}

}
