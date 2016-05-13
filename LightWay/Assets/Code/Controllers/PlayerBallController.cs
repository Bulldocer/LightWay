using UnityEngine;
using System.Collections;

public class PlayerBallController : MonoBehaviour {

	//Public variables
	public float forceMagnitude = 10.0f;
	public float jumpMagnitude=100.0f;
	public float airReductionMovement=2.5f;
	public float maxForce= 100.0f;


	//Private variables
	private Rigidbody rb;
	private bool onAir = false;
	private float initialDrag;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		initialDrag = rb.drag;
	}
	
	// Update is called once per frame
	void Update () {

		float xMovement = Input.GetAxis ("Horizontal");
		float zMovement = Input.GetAxis ("Vertical");

		Vector3 force = new Vector3 (xMovement, 0.0f, zMovement);
		Vector3 finalForce = force * forceMagnitude;

		if (rb.velocity.y != 0) {
			onAir = true;
			rb.drag = 0;
			if(finalForce.magnitude<maxForce)
			rb.AddForce (finalForce/airReductionMovement);
		} 
		else {
			onAir = false;
			rb.drag = initialDrag;
			if(finalForce.magnitude<maxForce)
			rb.AddForce (finalForce);
		}
		
		
			



		if (Input.GetKeyDown (KeyCode.Space) && !onAir) {
			rb.AddForce(0.0f, jumpMagnitude, 0.0f);
		}

	}
}
