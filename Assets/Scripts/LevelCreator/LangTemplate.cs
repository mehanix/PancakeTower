using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LangTemplate {




	public LanguageBlock[] text;


	public LangTemplate(int length) {
		text = new LanguageBlock[length];
	}

}

[System.Serializable]
public class LanguageBlock {

	public string[] lang;
	public LanguageBlock() {

		lang = new string[2];

	}



}