using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotate : MonoBehaviour {


	AudioClip sound;
	void Start () {

		gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y +0.5F, gameObject.transform.position.z);

	}
	void Update () {
		
		transform.Rotate(new Vector3(0,0,90)*Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {

			if(PlayerPrefs.GetInt("Muted")==0)
				gameObject.GetComponent<AudioSource>().PlayOneShot (gameObject.GetComponent<AudioSource> ().clip);
			gameObject.GetComponent<Renderer> ().enabled = false;
			gameObject.GetComponent<CapsuleCollider> ().enabled = false;
		
		}

	}
}
