﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionsManager : MonoBehaviour {


	public Slider volumeSlider;

	//false engleza, true romana
	public static bool language=false;
	public static KeyCode[] controls;
	public Toggle RoToggle;
	public Event e;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onSliderMoved() {
	
		AudioListener.volume = volumeSlider.value;
	}

	public void onValueChanged(bool value) {


		language = value;
		print (language.ToString ());
	}

}