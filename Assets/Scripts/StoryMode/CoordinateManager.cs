using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateManager : MonoBehaviour {

	public  Vector3[] LevelPositions = { new Vector3 { x = -4F, y = 0, z = -5F }, 
										new Vector3 { x = -4F, y = 0, z = -3.5F},
										new Vector3 { x = -5F, y = 0, z = 1.5F},
										new Vector3 { x = -3.5F, y = 0, z = -1}};

	public Vector3[] PlayerSpawnPoints;
	public CoordinateManager()
	{
		PlayerSpawnPoints = new Vector3[] { new Vector3 { x = -4.5F, y = 0, z = 0 }, 
			new Vector3 { x = -2F, y = 0, z = -3.5F},
			new Vector3 { x = -3F, y = 0, z = -5.5F},
			new Vector3 { x = -1.5F, y = 0, z = -6.5F} };
	}

}
