using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingCube : MonoBehaviour {



	PlayerMovement coinmanager;
	public LanguageManager languagemanager;
	Renderer rend;
	BoxCollider coll;
	// Use this for initialization
	void Start () {

			coinmanager = GameObject.Find ("Player").GetComponent<PlayerMovement> ();	
		rend = GetComponent<Renderer> ();
		coll = GetComponent < BoxCollider >();
		

	}
	
	// Update is called once per frame
	void Update () {
		if (coinmanager.coinCount == coinmanager.totalCoinsPerLevel && gameObject.GetComponent<Renderer> ().enabled==true) {

			rend.enabled = false;
			coll.enabled = false;
		}
	}


}
