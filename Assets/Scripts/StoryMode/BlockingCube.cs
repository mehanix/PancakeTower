using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingCube : MonoBehaviour {



	PlayerMovement coinmanager;
	public LanguageManager languagemanager;

	// Use this for initialization
	void Start () {

			coinmanager = GameObject.Find ("Player").GetComponent<PlayerMovement> ();	
	
	

	}
	
	// Update is called once per frame
	void Update () {
		if (coinmanager.coinCount == coinmanager.totalCoinsPerLevel && gameObject.GetComponent<Renderer> ().enabled==true) {
		
			gameObject.GetComponent<Renderer> ().enabled = false;
			gameObject.GetComponent<BoxCollider> ().enabled = false;
		}
	}


}
