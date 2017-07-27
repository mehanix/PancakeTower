using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NewLevelTemplate {

	// Use this for initialization

	//TEMPLATE PT NIVEL NOU
	//Object type : 0 wall 1 player 2 goal 3 coin 4 trap

	public Vector3[] coords;
	public Vector3[] scale;
	public int[] objectType;

	public NewLevelTemplate(int length) {
	
		coords = new Vector3[length];
		scale = new Vector3[length];
		objectType = new int[length];

	}

}
