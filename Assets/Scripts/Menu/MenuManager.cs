﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public GameObject mainMenuButtons;
	public GameObject playMenuButtons;
	public Button[] buttons;
	public Button socialButton, exitButton;
	public Text[] labels;
	LanguageManager lang;
	int languageCode;
	public LanguageManager languagemanager;
	public Toggle ro, en;

	Animator animator;

	public AnimationClip anim;
	void Start() {
		
		languagemanager = GetComponent<LanguageManager> (); 
	
		animator = Camera.main.GetComponent<Animator> ();



	}
	void Update() {
	
		for(int i=0;i<buttons.Length-1;i++) {

			buttons [i].GetComponentInChildren<Text> ().text = languagemanager.lang.text [i].lang [languageCode];
		}
			buttons [6].GetComponentInChildren<Text> ().text = languagemanager.lang.text [5].lang [languageCode];

		labels[0].text = languagemanager.lang.text [6].lang [languageCode];
		labels[1].text = languagemanager.lang.text [7].lang [languageCode];

		ro.isOn = OptionsManager.language;

		languageCode = (OptionsManager.language == true) ? 1 : 0;


	}
	public void onToggleButtonClick(){
	
		toggleMenus ();
	}

	void toggleMenus() {
	
		mainMenuButtons.SetActive (!mainMenuButtons.activeInHierarchy);

		mainMenuButtons.GetComponent<Animation> ().Play("ButtonSlideUp");
		playMenuButtons.SetActive (!mainMenuButtons.activeInHierarchy);
		playMenuButtons.GetComponent<Animation> ().Play("ButtonSlideUp");


	}

	//main menu buttons

	public void onOptionsButtonClick() {
		
		animator.SetFloat ("Animate", 1F);
	}

	public void onExitButtonClick() {

		Application.Quit ();
	}


	//play menu buttons

	public void switchToScene(string name) {
	
		SceneManager.LoadScene(name);
	}


	//options menu buttons
	public void onGoBackBtnClick() {

		if(OptionsManager.language==true)
			PlayerPrefs.SetInt ("language", 1);
		else
			PlayerPrefs.SetInt ("language", 0);
		PlayerPrefs.Save ();
		animator.SetFloat ("Animate", 0F);
	}
}
