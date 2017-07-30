using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class LevelCreator : MonoBehaviour {

	public GameObject wallPrefab;
	public GameObject[] spawnableObjects;
	private Vector3 mousePoint, offset;
	string path;
	public GameObject created_level, objectInstance;
	bool shouldSpawnObject;
	string[] objectNames = { "Wall", "MazePlayer", "MazeGoal","Coin" };



	public GameObject instancedObj;
	// Use this for initialization

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {


	}

	//wall creation
	public void createWall (int width, int height) {
	
		objectInstance = Instantiate (wallPrefab,new Vector3(14,0,-3), Quaternion.identity);

		objectInstance.GetComponent<Transform> ().localScale = new Vector3 (width, 1, height);
	
	}


	//Object type : 0 wall 1 player 2 goal 3 coin 4 trap
	public void spawnObject(int objectId){

		shouldSpawnObject = true;
		if (objectId == 2 || objectId == 1)
		if (GameObject.Find (objectNames [objectId]) == true)
			shouldSpawnObject = false;

		if (shouldSpawnObject) {

			objectInstance = Instantiate (spawnableObjects [objectId], new Vector3 (14, 0, -3), Quaternion.identity);
			objectInstance.name = objectNames [objectId];

		
		}
	}
				
	public void spawnObject(int objectId, Vector3 scale, Vector3 position){
		
		objectInstance = Instantiate (spawnableObjects [objectId], position, Quaternion.identity);
		objectInstance.transform.localScale = scale;
		objectInstance.name = objectNames [objectId];

			
	}


	Rect CenterRect(float windowWidth, float windowHeight) {

		float centeredX, centeredY;

		centeredX = (Screen.width - windowWidth) / 2;
		centeredY = (Screen.height - windowHeight) / 2;

		return new Rect (centeredX, centeredY, windowWidth, windowHeight);
	}


	public void saveLevel(string levelName) {

		GameObject[] walls = GameObject.FindGameObjectsWithTag ("Wall");
		GameObject[] coins = GameObject.FindGameObjectsWithTag ("Coin");


		foreach (GameObject wall in walls) {
		
			wall.transform.parent = created_level.transform;
		}

		foreach (GameObject coin in coins) {

			coin.transform.parent = created_level.transform;
		}
		GameObject.Find ("MazePlayer").transform.parent = created_level.transform;
		GameObject.Find ("MazeGoal").transform.parent = created_level.transform;




														//nr ziduri + nr monede + player + goal
		NewLevelTemplate newlevel = new NewLevelTemplate (walls.Length + coins.Length + 2);


		//Object type : 0 wall 1 player 2 goal 3 coin 4 trap

		for (int i = 0; i < walls.Length; i++) {
		
			newlevel.coords [i] = walls [i].transform.position;
			newlevel.scale [i] = walls [i].transform.localScale;
			newlevel.objectType [i] = 0;
		}
		for (int i = walls.Length; i < coins.Length; i++) {

			newlevel.coords [i] = coins [i].transform.position;
			newlevel.scale [i] = Vector3.one;
			newlevel.objectType [i] = 3;
		}

		int tempLength = walls.Length + coins.Length;


		//player,goal(unice per harta)
		newlevel.coords [tempLength] = GameObject.Find ("MazePlayer").transform.position;
		newlevel.scale [tempLength] = Vector3.one;
		newlevel.objectType [tempLength] = 1;

		tempLength++;
		newlevel.coords [tempLength] = GameObject.Find ("MazeGoal").transform.position;
		newlevel.scale [tempLength] = Vector3.one;
		newlevel.objectType [tempLength] = 2;

		string newlevelJson = JsonUtility.ToJson (newlevel);

		string path = "Assets/CustomLevels/" + levelName + ".json";

		File.WriteAllText (path, newlevelJson);

		print ("Done!");



	}


	public void loadLevel(string levelName) {
	
		string levelData = System.IO.File.ReadAllText ("Assets/CustomLevels/" + levelName + ".json");
		print (levelData);
		NewLevelTemplate level = JsonUtility.FromJson <NewLevelTemplate>(levelData);
		for (int i = 0; i < level.objectType.Length; i++) {

			spawnObject (level.objectType [i], level.scale [i], level.coords [i]);

		}
		//loading now! :D

	}


	public void clearLevel() {
	
		GameObject[] objs;

		objs = GameObject.FindGameObjectsWithTag ("Wall");

		foreach (GameObject obj in objs){
			Destroy (obj);
		}

		objs = GameObject.FindGameObjectsWithTag ("Coin");

		foreach (GameObject obj in objs){
			Destroy (obj);
		}

		Destroy(GameObject.FindGameObjectWithTag("Player"));
		Destroy(GameObject.FindGameObjectWithTag("Goal"));

		}
}