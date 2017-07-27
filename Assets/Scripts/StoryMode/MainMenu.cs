using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public GUISkin skin;
	void OnGUI()
	{
		GUI.skin = skin;
		GUI.Label (new Rect(10,10,400,45),"Pancake Tower! Test Build");
		if (GUI.Button (new Rect (10, 150, 45, 45), "Play"))
			Application.LoadLevel (0);
		if (GUI.Button (new Rect (10, 205, 45, 45), "Quit"))
			Application.Quit ();
	}
}
