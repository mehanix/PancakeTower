using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	int currentLevel;
	public GameObject[] levels;
	public GameObject floor;
	float previousY;
	public CoordinateManager cm;
	AudioSource audiosource;
	public bool showPausedWindow=false,paused=false;
	string soundMuteText;
	public GUISkin skin;
	public GameObject player;
	public bool shouldShowInstructionsWindow=true;
	public Rect windowRect,scoreWindowRect,buttonRect;
	public Timer timer;
	public LanguageManager languagemanager;
	public int languageCode;


	void Start () {
		languagemanager = GetComponent<LanguageManager> (); 

		currentLevel = 0;
		cm = GameObject.Find("GameManager").GetComponent<CoordinateManager> ();
		audiosource = gameObject.GetComponent<AudioSource> ();

		levels[currentLevel] = Instantiate(levels[0],new Vector3(-4,0,-5), Quaternion.identity) as GameObject;
		player = GameObject.Find ("Player");
		languageCode = (OptionsManager.language == true) ? 1 : 0;
		timer = GetComponent<Timer> ();


	}

	// Update is called once per frame
	void Update () {
		//if (floor.GetComponent<FloorFalling>().isFalling == false) 
			//levels[currentLevel] = Instantiate(levels[currentLevel],new Vector3(0, 5, 0), Quaternion.identity) as GameObject;
		checkForPausing();
		
	}


	void OnGUI() {
		scoreWindowRect = CenterRect (400, 250);
		windowRect = CenterRect (400, 400);
		GUI.skin = skin;
		if (shouldShowInstructionsWindow == true) 
			GUI.Window (0, scoreWindowRect, Windows, languagemanager.lang.text [8].lang[languageCode] ,skin.GetStyle("MMInstructionsWindow"));
		if (showPausedWindow == true)
			GUI.Window (1, new Rect ((Screen.width / 2) - 75, (Screen.height / 2) - 125, 150, 250), Windows, "Paused", skin.GetStyle ("MMInstructionsWindow"));
		timer.coinScoreText = languagemanager.lang.text [27].lang [languageCode] + player.GetComponent<PlayerMovement> ().coinCount.ToString() + "/" + player.GetComponent<PlayerMovement> ().totalCoinsPerLevel.ToString();
	}

	void Windows(int windowId) {
	

		if (windowId == 0) {
			//instructions
			GUI.Label (new Rect (0, 80, 400, 50), languagemanager.lang.text [26].lang [languageCode], skin.GetStyle ("MMCenteredText"));
			GUI.Label (new Rect (0, 250, 400, 50), "S", skin.GetStyle ("MMCenteredText"));
		
			if (GUI.Button (new Rect (150, 185, 100, 35), languagemanager.lang.text [11].lang [languageCode])) {
				shouldShowInstructionsWindow = false;
			}
		}


		if (windowId == 1) {
			soundMuteText = (audiosource.mute == true) ? "Unmute Sound" : "Mute Sound";

			if (GUI.Button (new Rect (15, 90, 120, 30), "Reset Level")) {
				resetLevel ();
			}

			if (GUI.Button (new Rect (15, 130, 120, 30), soundMuteText)) {

				audiosource.mute = !audiosource.mute;

			}
			if (GUI.Button (new Rect(15,170,120,30), "Back to Main Menu")) {

				SceneManager.UnloadSceneAsync ("StoryMode");
				SceneManager.LoadScene ("Menu");
			}


		}
	}
		

	public void nextLevel()
	{
		levels [currentLevel].transform.parent = null;
		previousY = levels [currentLevel].transform.position.y;
		Destroy (levels [currentLevel]);

		currentLevel++;
		floor = Instantiate (floor, new Vector3 (0,  previousY + 4, 0), Quaternion.identity) as GameObject;
		floor.name = "FloorNo " + currentLevel.ToString ();
		print (cm.PlayerSpawnPoints [currentLevel].x.ToString() + ' ' + cm.PlayerSpawnPoints [currentLevel].y.ToString() + ' ' + cm.PlayerSpawnPoints[currentLevel].z.ToString() + ' ');

		cm.LevelPositions [currentLevel].y = previousY +cm.LevelPositions [currentLevel].y+4.5F;


		levels[currentLevel] = Instantiate(levels[currentLevel],cm.LevelPositions[currentLevel], Quaternion.identity) as GameObject;

		levels [currentLevel].transform.SetParent (floor.transform);
	
	}

	public void resetLevel() {

		GameObject[] obj = GameObject.FindGameObjectsWithTag ("Coin");
		foreach (GameObject coin in obj) {

			coin.GetComponent<Renderer> ().enabled = true;
			coin.GetComponent<CapsuleCollider> ().enabled = true;
		}

		obj = GameObject.FindGameObjectsWithTag ("SlidingBlock");
		foreach (GameObject block in obj) {
			block.GetComponent<SlidingBlock> ().setInitialPos ();

		}
		obj = GameObject.FindGameObjectsWithTag ("BlockingCube");

		foreach (GameObject block in obj) {
			block.GetComponent<Renderer> ().enabled = true;
			block.GetComponent<BoxCollider> ().enabled = true;

		}
		player.GetComponent<PlayerMovement>().coinCount=0;

		player.transform.position = new Vector3 (cm.PlayerSpawnPoints [currentLevel].x, player.transform.position.y, cm.PlayerSpawnPoints [currentLevel].z);

	}
	public int getCurrentLevel()
	{
		return currentLevel;
	}

	void checkForPausing() {

		if (Input.GetKeyUp (KeyCode.Escape)) {

			paused = !paused;
			showPausedWindow = !showPausedWindow;
		}
	}

	Rect CenterRect(float windowWidth, float windowHeight) {

		float centeredX, centeredY;

		centeredX = (Screen.width - windowWidth) / 2;
		centeredY = (Screen.height - windowHeight) / 2;

		return new Rect (centeredX, centeredY, windowWidth, windowHeight);
	}


}