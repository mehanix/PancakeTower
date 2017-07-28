using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeGUI : MonoBehaviour {



	public Rect windowRect,buttonRect,arrowkeysRect,scoreWindowRect;
	public Rect[] controlsRect;
	public GUISkin skin;
	public float width, height;
	public Texture arrowkeysTexture;
	Timer timer;
	int totalScore;
	public static bool shouldShowInstructionsWindow=true,
	   				   shouldShowVictoryWindow=false,
						showPausedWindow=false;
	public bool paused=false;
	string soundMuteText;
	AudioSource audiosource;
	private string victoryMessage;
	public LanguageManager languagemanager;
	public int languageCode;
	void Start () {
		setStaticVariables ();
		languagemanager = GetComponent<LanguageManager> (); 
		timer = this.gameObject.GetComponent<Timer>();
		print (PlayerPrefs.GetInt ("highscore",0).ToString ());
		audiosource = gameObject.GetComponent<AudioSource> ();
		languageCode = (OptionsManager.language == true) ? 1 : 0;
		soundMuteText= languagemanager.lang.text [22].lang[languageCode];

	}

	void OnGUI() {

		GUI.skin = skin;

		windowRect = CenterRect (400, 400);
		scoreWindowRect = CenterRect (400, 250);
		if (shouldShowInstructionsWindow == true) 
			windowRect = GUI.Window (0, windowRect, Windows,  languagemanager.lang.text [8].lang[languageCode],skin.GetStyle("MMInstructionsWindow"));

		if (shouldShowVictoryWindow == true)
			windowRect = GUI.Window (1, scoreWindowRect, Windows, languagemanager.lang.text [12].lang[languageCode],skin.GetStyle("MMInstructionsWindow"));
		if (showPausedWindow == true)
			windowRect = GUI.Window (2, new Rect ((Screen.width / 2) - 75, (Screen.height / 2) - 125, 150, 250), Windows, languagemanager.lang.text [24].lang[languageCode], skin.GetStyle ("MMInstructionsWindow"));
		
	}

	void Update() {
	
		checkForPausing ();
	}


	void Windows(int windowId) {


		if(windowId==0) {
				//instructions
			GUI.Label (new Rect (0, 60, 400, 50), languagemanager.lang.text [9].lang[languageCode],skin.GetStyle("MMCenteredText"));
			GUI.Label (new Rect (0, 250, 400, 50), languagemanager.lang.text [10].lang[languageCode],skin.GetStyle("MMCenteredText"));

				GUI.DrawTexture (arrowkeysRect, arrowkeysTexture);
			GUI.Label (controlsRect[0], "a",skin.GetStyle("MMCenteredText"));
				GUI.Label (controlsRect[1], "←",skin.GetStyle("MMCenteredText"));
				GUI.Label (controlsRect[2], "↓",skin.GetStyle("MMCenteredText"));
				GUI.Label (controlsRect[3], "→",skin.GetStyle("MMCenteredText"));


				//start timer and hide window
			if (GUI.Button (buttonRect, languagemanager.lang.text [11].lang[languageCode])) {

					Timer.shouldRun = true;
					shouldShowInstructionsWindow = false;
					MazePlayerMovement.shouldMove = true;
					}
			}

		if (windowId == 1) {

			float timeScore = 1 / timer.startTime * 10000;
			totalScore = (int)timeScore + Timer.score;
	
			if (totalScore > PlayerPrefs.GetInt ("highscore", 0)) {

				victoryMessage +=  languagemanager.lang.text [21].lang[languageCode];
				PlayerPrefs.SetInt ("highscore", totalScore);
				PlayerPrefs.Save ();
			}
			string[] splitTime = timer.currentTime.Split ();
			victoryMessage = languagemanager.lang.text [13].lang[languageCode] +splitTime[1] + languagemanager.lang.text [14].lang[languageCode] + totalScore;

			GUI.Label (new Rect (0, 60, 400, 50), victoryMessage, skin.GetStyle ("MMCenteredText"));
			//GUI.Label (new Rect (0, 250, 400, 50), "Your score is: " + totalScore, skin.GetStyle ("MMCenteredText"));

			if (GUI.Button (new Rect (150, 185, 100, 35), languagemanager.lang.text [15].lang[languageCode])) {

				//tba fancy fade effect?
				SceneManager.LoadScene ("Menu");
			}
		}
		//pause window!
			if (windowId == 2) {
			soundMuteText = (audiosource.mute == true) ? languagemanager.lang.text [22].lang[languageCode] : languagemanager.lang.text [25].lang[languageCode];

			//mute sound
			if (GUI.Button (new Rect (15, 130, 120, 30), soundMuteText)) {
			
				audiosource.mute = !audiosource.mute;
		
			}
			//back to menu
			if (GUI.Button (new Rect(15,170,120,30),  languagemanager.lang.text [23].lang[languageCode])) {
					
					SceneManager.UnloadSceneAsync ("MazeMode");
					SceneManager.LoadScene ("Menu");
			}
				
		
	}
	}
		

	Rect CenterRect(float windowWidth, float windowHeight) {
	
		float centeredX, centeredY;

		centeredX = (Screen.width - windowWidth) / 2;
		centeredY = (Screen.height - windowHeight) / 2;

		return new Rect (centeredX, centeredY, windowWidth, windowHeight);
	}

	void checkForPausing() {

		if (Input.GetKeyUp (KeyCode.Escape)) {

			paused = !paused;
			showPausedWindow = !showPausedWindow;
		}
	}

	void setStaticVariables() {
	
		shouldShowInstructionsWindow = true;
		shouldShowVictoryWindow=false;
		showPausedWindow=false;
		paused=false;
	}
}
