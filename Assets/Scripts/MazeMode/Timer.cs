using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	public float startTime;
	public string currentTime;
	public static bool shouldRun;
	public static int score;
	public Rect timerRect;
	public Rect scoreRect;
	MazeGUI mazegui;
	public GUISkin skin;

	void Start () {

		shouldRun = false;
		score = 0;
		mazegui = GetComponent<MazeGUI> ();
	}
	
	void Update () {

		if (shouldRun == true && mazegui.paused == false) {
			
			startTime += Time.deltaTime;
			currentTime = string.Format (mazegui.languagemanager.lang.text [16].lang[mazegui.languageCode] + "{0:0.0}", startTime);
		}
	}

	void OnGUI() {
	
		scoreRect = positionScoreRect ();
		GUI.skin = skin;
		GUI.Label (timerRect, currentTime, skin.GetStyle("Timer"));
		GUI.Label (scoreRect,mazegui.languagemanager.lang.text [17].lang[mazegui.languageCode] + score.ToString(), skin.GetStyle("Timer"));


	}

	Rect positionScoreRect(){

		return new Rect (new Vector2 (Screen.width - 100, 25), new Vector2 (500, 500));
	}
}
