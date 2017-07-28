using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public class CreatorUI : MonoBehaviour {


	public bool ShowToolbox = true;
	public Rect[] toolboxRects;
	public Rect[] toolboxBtns;
	public GUISkin skin;
	LevelCreator lc;
	 bool ShowPropWindow=false, ShowInstructionsWindow = true,showNamingWindow=false, showLoadlevelWindow=false;
	public bool deleteMode=false, rotateMode=false,playMode=false;
	public static string wallWidth="",wallHeight="",levelName="";
	private int intWidth, intHeight;
	public Vector3 editModePos;
	public Vector3 viewModePos = new Vector3 (8, 12, -12);
	Quaternion viewModeRot = Quaternion.Euler (54, -34, 0);
	int wallCount=0;
	Vector3 playerInitialPos;
	public GameObject player;
	string rotateModeString="Rotate\nMode",deleteModeString="Delete\nMode";
	public static bool editMode = false;
	int selectedLevelId;
	//LangTemplate lang;
	string languagePath="Assets/Language/Language.json";
	string[] loadLevels;
	Vector2 loadLevelBoxVector;
	int languageCode;

	public  LangTemplate lang;

	// Use this for initialization
	void Start () {
		//savedCameraRot = this.gameObject.transform.rotation;
		lc = GetComponent<LevelCreator> ();	
		editModePos = new Vector3 (0, 30, 0);

//		string languageData = System.IO.File.ReadAllText (languagePath);
//
//		lang = new LangTemplate (26);
//		lang = JsonUtility.FromJson <LangTemplate>(languageData);

		languageCode = (OptionsManager.language == true) ? 1 : 0;

		//0 eng 1 romana
		int lineCount=31;
		lang = new LangTemplate (lineCount);
		for(int i=0;i<lineCount;i++)
		{
			lang.text[i] = new LanguageBlock ();
			//lang.text[i].lang[0]= new LanguageBlock ();

		}
		lang.text[0].lang[0]= "Play";
		lang.text[0].lang[1]= "Joaca";

	
		lang.text [1].lang[0]= "Settings";
		lang.text [1].lang[1]= "Setări";

		lang.text [2].lang[0]= "Puzzle Mode";
		lang.text [2].lang[1]= "Modul Puzzle";

		lang.text [3].lang[0]= "Maze Mode";
		lang.text [3].lang[1]= "Modul Labirint";

		lang.text [4].lang[0]= "Level Creator";
		lang.text [4].lang[1]= "Creator Nivele";

		lang.text [5].lang[0]= "Go Back";
		lang.text [5].lang[1]= "Inapoi";

		lang.text [6].lang[0]= "Volume: ";
		lang.text [6].lang[1]= "Volum: ";

		lang.text [7].lang[0]= "Language: ";
		lang.text [7].lang[1]= "Limba: ";

		//maze mode
		lang.text [8].lang[0]= "Instructions";
		lang.text [8].lang[1]= "Instructiuni";

		lang.text [9].lang[0]= "Get to the end of the maze as soon as you can!\nMove around with:";
		lang.text [9].lang[1]= "Gaseste iesirea din labirint in cat mai putin timp!\nDeplaseaza-te cu:";

		lang.text [10].lang[0]= "Collect coins to increase your score :)";
		lang.text [10].lang[1]= "Strange monede pentru a-ti creste scorul :)";

		lang.text [11].lang[0]= "Let's go!";
		lang.text [11].lang[1]= "Ok!";

		lang.text [12].lang[0]= "Congratulations!";
		lang.text [12].lang[1]= "Felicitari!";

		lang.text [13].lang[0]= "You have completed the maze in ";
		lang.text [13].lang[1]= "Ai terminat labirintul in ";

		lang.text [14].lang[0]= "seconds!\nYour final score is:";
		lang.text [14].lang[1]= "secunde!\nScorul tau final este:";


		lang.text [15].lang[0]= "Continue";
		lang.text [15].lang[1]= "Continua";

		lang.text [16].lang[0]= "Time: ";
		lang.text [16].lang[1]= "Timp: ";

		lang.text [17].lang[0]= "Score: ";
		lang.text [17].lang[1]= "Scor: ";

		//level creator


		lang.text [18].lang[0]= "Level Creator";
		lang.text [18].lang[1]= "Creator Nivele";


		lang.text [19].lang[0]= "Welcome! Here you can create your own levels,\nor load an existing level to play it.\n Press ESC when playing the level \nto go back to the level editor.";
		lang.text [19].lang[1]= "Bine ai venit! Aici poti crea nivele noi\nsau sa incarci nivele deja create.\nApasa ESC in timpul jocului\n pentru a te intoarce la editor.";


		lang.text [20].lang[0]= "Got it!";
		lang.text [20].lang[1]= "Ok!";

		lang.text [21].lang [0] = "\nNew High Score!";
		lang.text [21].lang [1] = "\nNou record!";

		lang.text [22].lang [0] = "Mute Sound";
		lang.text [22].lang [1] = "Opreste Sunetul";

		lang.text [23].lang [0] = "Back to Main Menu";
		lang.text [23].lang [1] = "Inapoi la meniu";

		lang.text [24].lang [0] = "Paused";
		lang.text [24].lang [1] = "Pauza";

		lang.text [25].lang [0] = "\nUnmute Sound";
		lang.text [25].lang [1] = "\nPorneste Sunetul";

		lang.text [26].lang [0] = "Complete the puzzles as quickly as you can!\nAll coins must be collected for the exit to open.\nAvoid spike traps!";
		lang.text [26].lang [1] = "Termina puzzle-urile in cat mai putin timp!\nToate monedele trebuie colectate pentru ca iesirea sa se deschida.\nEvita capcanele!";

		lang.text [27].lang [0] = "Coins: ";
		lang.text [27].lang [1] = "Monede: ";

		lang.text [28].lang [0] = "You have completed Puzzle Mode in ";
		lang.text [28].lang [1] = "Ai terminat Modul Puzzle in ";

		lang.text [29].lang [0] = " seconds.\n In total, you have died ";
		lang.text [29].lang [1] = " secunde.\n In total, ai murit de ";

		lang.text [30].lang [0] = " times.";
		lang.text [30].lang [1] = " ori.";





		//print (lang.text [2].ro);
		string langData = JsonUtility.ToJson (lang,true);
		print (langData);
		File.WriteAllText ("Assets/Language/Language.json",langData);
	}

	void Update() {
	
		if (Input.GetKeyUp (KeyCode.Escape)) {


			ShowToolbox = true;
			LcPlayer.shouldMove = false;
			setEditMode (true);
			player.GetComponent<Transform> ().SetPositionAndRotation (playerInitialPos, Quaternion.identity);
			print (player.GetComponent<Transform>().position.ToString());
			player.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;


		}
	}

	void OnGUI() {
		
		GUI.skin = skin;
		//windows
		if (ShowToolbox == true) {

			//add obj toolbox
			toolboxRects[0].height=Screen.height;
			toolboxRects [0] = GUI.Window (0, toolboxRects [0], Windows, "", skin.GetStyle ("LcToolbox"));



			//level controls toolbox
			toolboxRects[3]=new Rect(Screen.width-160, 0,160,40);
			toolboxRects [3] = GUI.Window (3, toolboxRects[3], Windows, "", skin.GetStyle ("LcToolbox"));

	

			if (ShowPropWindow == true) {
				toolboxRects[1] = GUI.Window(1,toolboxRects[1],Windows,"",skin.GetStyle("MMInstructionsWindow"));

			}
		}

	
		if (ShowInstructionsWindow == true) {

			toolboxRects [2] = CenterRect (400, 400);
			toolboxRects [2] = GUI.Window (2, toolboxRects [2], Windows, "Level Creator", skin.GetStyle ("MMInstructionsWindow"));
		}

		if (showNamingWindow == true) {

			toolboxRects [4] = CenterRect (220, 100);
			toolboxRects [4] = GUI.Window(4,toolboxRects[4],Windows,"",skin.GetStyle("MMInstructionsWindow"));

		}

		if (showLoadlevelWindow == true) {

			toolboxRects [5] = CenterRect (250, 400);
			toolboxRects [5] = GUI.Window(5,toolboxRects[5],Windows,"Load Level",skin.GetStyle("MMInstructionsWindow"));

		}

	}


	void Windows(int windowId) {

		//TOOLBOX:level editor
		if (windowId == 0) {
		
			//trecut din edit mode in view mode
			if (editMode == false)
			if (GUI.Button (toolboxBtns [0], "", skin.GetStyle("LcEditBtn"))) {

				//salvez coord curente ale camerei ca sa pot sa le resetez cand se iese din edit mode
				setEditMode (true);

			}

			if (editMode == true) {
			
				if (GUI.Button (toolboxBtns [0],"", skin.GetStyle("LcViewBtn"))) {

					setEditMode (false);
		


				}
					
			}
			//Object type : 0 wall 1 player 2 goal 3 coin 4 trap

			//butoanele si functionalitatea lor
			if (GUI.Button (toolboxBtns [1], "",skin.FindStyle("LcAddWallBtn"))) {
				ShowPropWindow = !ShowPropWindow;
			}

			if (GUI.Button (toolboxBtns [2], "",skin.FindStyle("LcAddGoalBtn"))) {

				lc.spawnObject (2);
			}
				if (GUI.Button (toolboxBtns [3],  "",skin.FindStyle("LcAddPlayerBtn"))) {

				lc.spawnObject (1);
			}
			if (GUI.Button (toolboxBtns [4], "",skin.FindStyle("LcAddCoinBtn"))) {

				lc.spawnObject (3);
			}


			if (GUI.Button (toolboxBtns [5],"",skin.GetStyle("LcRotateModeBtn"))) {

				rotateMode = !rotateMode;
				//rotateModeString = (rotateMode==true) ? "Stop\nRotating" :  "Rotate\nMode";

			}

		
				if (GUI.Button (toolboxBtns [6],"",skin.GetStyle("LcDeleteModeBtn"))) {
			
					deleteMode = !deleteMode;
					//deleteModeString = (deleteMode==true) ? "Stop\nDeleting" : "Delete\nMode";
				}
		

		}




		//TOOLBOX:optiuni dimensiuni pt creare wallpiece
		if (windowId == 1) {
			GUI.Label (new Rect (20, 20, 0, 0), "W:", skin.GetStyle ("MMCenteredText"));
			wallWidth = GUI.TextField (new Rect (40, 10, 30, 30), wallWidth, 2);

			GUI.Label (new Rect (85, 20, 0, 0), "H:", skin.GetStyle ("MMCenteredText"));
			wallHeight = GUI.TextField (new Rect (105, 10, 30, 30), wallHeight, 2);

			if (GUI.Button (new Rect (50, 50, 50, 30), "Create")) {

				if (int.TryParse (wallWidth, out intWidth) && int.TryParse (wallHeight, out intHeight)) {
					lc.createWall (intWidth, intHeight);
					ShowPropWindow = false;
				}
			}

		}








		//fereastra instructiuni
		if (windowId == 2) {

			GUI.Label (new Rect (0, 100, 400, 100),lang.text [19].lang[languageCode], skin.GetStyle ("MMCenteredText"));

			if (GUI.Button (new Rect (175, 330, 50, 40), lang.text[20].lang[languageCode])) {
				ShowInstructionsWindow = false;
			}
		}









		//TOOLBOX:fereastra load/save prefabs

		if (windowId == 3) {


			//buton start playing
			if (LcPlayer.shouldMove == false)
					if (GUI.Button (toolboxBtns [0], "",skin.GetStyle("LcPlayBtn"))) {

				if (GameObject.Find ("MazePlayer") && GameObject.Find ("MazeGoal")) {
					playMode = true;
					playerInitialPos = player.GetComponent<Transform> ().position;
					print (playerInitialPos.ToString ());

					// &= -> AND pe biti. ~ -> negated mask
					player.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.None;

					setEditMode (false);
					LcPlayer.shouldMove = true;
					ShowToolbox = false;						
				}

			}
		




					if (GUI.Button (toolboxBtns [7],"",skin.GetStyle("LcSaveBtn"))) {


				showNamingWindow = !showNamingWindow;
		
			}
					if (GUI.Button (toolboxBtns [8],"",skin.GetStyle("LcLoadBtn"))) {

				showLoadlevelWindow = !showLoadlevelWindow;
			}
					if (GUI.Button (toolboxBtns [9], "",skin.GetStyle("LcQuitBtn"))) {

				SceneManager.LoadScene ("Menu");
			}

		
		}

		//save level window
		if (windowId == 4) {


			GUI.Label (new Rect (30, 20, 0, 0), "Name:", skin.GetStyle ("MMCenteredText"));
			levelName = GUI.TextField (new Rect (65, 10, 140, 30), levelName, 50);


			if (GUI.Button (new Rect (85, 50, 50, 30), "Save")) {

				lc.saveLevel (levelName);
				showNamingWindow = false;
			}


		}
	
		//load level window
		if (windowId == 5) {

			string[] levelList = Directory.GetFiles ("Assets/CustomLevels").Where (lvName => lvName.EndsWith (".json")).ToArray();


			for (int i = 0; i < levelList.Length; i++) {
				levelList [i] = Path.GetFileNameWithoutExtension (levelList[i]);
			}
			
			loadLevelBoxVector = GUI.BeginScrollView(new Rect (15, 70, 220, 280),loadLevelBoxVector, new Rect (0, 0, 200, levelList.Length*50));

			selectedLevelId = GUI.SelectionGrid (new Rect (0, 0, 220,levelList.Length*50), selectedLevelId, levelList, 1);
			GUI.EndScrollView ();

			if (GUI.Button (new Rect (100, 340, 50, 30), "Load")) {

				lc.loadLevel (levelList[selectedLevelId]);
				showLoadlevelWindow = false;
			}
		}
}

	//centreaza fereastra de instructiuni la resize ecran
	Rect CenterRect(float windowWidth, float windowHeight) {

		float centeredX, centeredY;

		centeredX = (Screen.width - windowWidth) / 2;
		centeredY = (Screen.height - windowHeight) / 2;

		return new Rect (centeredX, centeredY, windowWidth, windowHeight);
	}

	public void setEditMode(bool status) {
	
		editMode = status;
		if (status == true) {
				//savedCameraPos = Camera.main.transform.position;
				//savedCameraRot = Camera.main.transform.rotation;
				Camera.main.transform.SetPositionAndRotation (editModePos, Quaternion.Euler (new Vector3 (90, 0, 0)));


		} else {
		
			Camera.main.transform.SetPositionAndRotation(viewModePos,viewModeRot);
		}
	}
}
