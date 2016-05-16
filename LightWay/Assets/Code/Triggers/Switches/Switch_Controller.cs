using UnityEngine;
using System.Collections;

public class Switch_Controller : MonoBehaviour {

	public GameObject pointLight;
	public float timeOn;

	private float timer;
	private bool isActive;
	private Renderer rend;
	private Material mat;
	// Use this for initialization
	void Start () {
		timer = 0.0f;
		isActive = false;
		pointLight.SetActive (false);
		rend = GetComponent<Renderer> ();
		mat = rend.sharedMaterial;
		mat.DisableKeyword("_EMISSION");
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0.0f, 3.0f, 0.0f), Space.World);
		if (isActive) {
			timer += Time.deltaTime;
			if (timer >= timeOn) {
				pointLight.SetActive (false);
				mat.DisableKeyword ("_EMISSION");
			}
		} else {
			mat.DisableKeyword ("_EMISSION");
		}
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.GetComponent<PlayerBallController> ()) {
			isActive = true;
			timer = 0;
			pointLight.SetActive (true);
			mat.EnableKeyword ("_EMISSION");
		}
	}
}
