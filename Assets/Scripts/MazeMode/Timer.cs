using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

	public float startTime;
	public string currentTime;
	public static bool shouldRun;
	public static int score;
	public Rect timerRect;
	public Rect scoreRect;
	MazeGUI mazegui;
	public GUISkin skin;
	bool showScore=false;
	public string coinScoreText;
	LevelManager levelmanager;
	Scene scene;
	void Start () {

		shouldRun = false;
		score = 0;
		mazegui = GetComponent<MazeGUI> ();
		levelmanager = GetComponent<LevelManager> (); 
		 scene = SceneManager.GetActiveScene ();
	
		if (scene.name == "MazeMode")
			showScore = true;

	}
	
	void Update () {


		if (shouldRun == true)
			if((scene.name=="MazeMode" && mazegui.paused == false) || (scene.name=="StoryMode" && levelmanager.paused==false))	{
				startTime += Time.deltaTime;
			currentTime = string.Format ("{0:0.0}", startTime);
			}
	
	}

	void OnGUI() {
	
		scoreRect = positionScoreRect ();
		GUI.skin = skin;
		GUI.Label (timerRect, currentTime,skin.GetStyle("Timer"));

		if (shouldRun) {
			
			if (showScore == true)
				GUI.Label (scoreRect, mazegui.languagemanager.lang.text [17].lang [mazegui.languageCode] + score, skin.GetStyle ("Timer"));
			else {
				GUI.Label (scoreRect, coinScoreText, skin.GetStyle ("Timer"));

			}
		
		}


	}

	Rect positionScoreRect(){

		return new Rect (new Vector2 (Screen.width - 100, 25), new Vector2 (500, 500));
	}
}
