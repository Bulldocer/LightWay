using UnityEngine;
using System.Collections;

public class DirectionalPlatform_Controller : MonoBehaviour {

	public bool movHorizontal;
	public float offset;
	public float period;

	private Vector3 iniPos;
	private float timer;
	// Use this for initialization
	void Start () {
		iniPos = transform.position;
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (movHorizontal)
			iniPos.x += Mathf.Sin (2.0f * Mathf.PI * timer / period) * offset;
		else
			iniPos.z += Mathf.Sin (2.0f * Mathf.PI * timer / period) * offset;
		transform.position = iniPos;
	}
}
