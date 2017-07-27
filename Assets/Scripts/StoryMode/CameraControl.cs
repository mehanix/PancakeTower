using UnityEngine;

public class CameraControl : MonoBehaviour {

	public Transform player;
	public Vector3 offset;


	void Start()
	{
		offset = new Vector3 (8F, 18F, -11F);
	}
	void Update () {

		
		transform.position = player.position + offset;
	}

}
