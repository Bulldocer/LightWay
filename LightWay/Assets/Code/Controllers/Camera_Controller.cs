using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour {

	public Transform player;
	public Vector3 offset;
	public float velRot;

	private Vector3 dir;

	// Use this for initialization
	void Start () {
		dir = player.forward;
	}
	
	// Update is called once per frame
	void Update () {
		float horAxis = Input.GetAxis ("Horizontal");
		dir.z += velRot * horAxis * Time.deltaTime;
		transform.position = player.position + offset;
		transform.RotateAround (player.position, new Vector3 (0.0f, 1.0f, 0.0f), dir.z);
		transform.LookAt (player.position);
	}
}
