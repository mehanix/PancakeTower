using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class LanguageManager : MonoBehaviour {


	public LangTemplate lang;

 	string languagePath,languageData;
	// Use this for initialization

	
	// Update is called once per frame

	 void Awake() {
	 	languagePath="Assets/Language/Language.json";
		languageData = System.IO.File.ReadAllText (languagePath);

		lang = new LangTemplate (26);
		lang = JsonUtility.FromJson <LangTemplate>(languageData);
	}

}
