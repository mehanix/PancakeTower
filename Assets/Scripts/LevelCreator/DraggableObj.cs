using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObj : MonoBehaviour {

	CreatorUI creatorui;

	Vector3 point,mousePos;
	void Start() {
	
		creatorui = GameObject.Find ("CreatorManager").GetComponent<CreatorUI> ();
		if (creatorui == null)
			this.enabled = false;

	}


	void OnMouseDrag() {
	
		//print ("dragging " + gameObject.name);

		if (CreatorUI.editMode == true) {


		
			//Vector3 dist = 
			//	mousePos.z = Vector3.Distance (Camera.main.transform.position, GameObject.Find ("Floor").transform.position);
			//mousePos.z=10;
			//point = Camera.main.ScreenToWorldPoint (mousePos);
			//print (point.ToString ());
			//transform.Translate (Mathf.Floor(point.x), transform.position.y, Mathf.Floor(point.z));
			//transform.Translate(
			mousePos = Input.mousePosition;
			mousePos.z = Vector3.Distance (Camera.main.transform.position, GameObject.Find ("Floor").transform.position);
			point = Camera.main.ScreenToWorldPoint (mousePos);
			//print (point.ToString ());
			transform.position = new Vector3 (point.x, transform.position.y, point.z);
		}

	}
	void OnMouseUp() {
	

		//snap to grid, pt aliniere mai usoara a nivelelor

		// "*2/2" -> rotunjeste la 0.5
		transform.position = new Vector3 (Mathf.Round(transform.position.x*2)/2, transform.position.y, Mathf.Round(transform.position.z*2)/2);
	}

	void OnMouseDown() {
	
		if (creatorui.deleteMode == true)
			Destroy (this.gameObject);

		if (creatorui.rotateMode == true) {
			
			//gameObject.transform.Rotate (0, 90, 0);

			//schimba dimensiunile in loc de rotire -> astfel la salvare exista un parametru mai putin
			transform.localScale = new Vector3(transform.localScale.z,transform.localScale.y,transform.localScale.x);
		}
	}

	void Update() {
		
	}

//	float halfRounding(float number) {
//
//		//snapping to .5
//
//		float decimalPart = number % 1;
//
//		if (decimalPart <= 0.25F)
//			return number - decimalPart;
//		
//			
//
//
//	}
}
