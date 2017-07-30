using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnTouch : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		

			if(PlayerPrefs.GetInt("Muted")==0)
				gameObject.GetComponent<AudioSource>().PlayOneShot (gameObject.GetComponent<AudioSource> ().clip);

}
}
