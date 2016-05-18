using UnityEngine;
using System.Collections;

public class PlayerBall_Controller : MonoBehaviour {

	public float maxVel;
	public float maxAcc;
	public float velRot;
	public float frictionCoef;
	public float gravity;
	public float jumpForce;
	public int maxJumps;
	public Transform cam;


	private Rigidbody rb;
	private Vector3 vel;
	private bool onAir;
	private int jump; // Just in case we want more than one jump

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		vel = rb.velocity;
		onAir = true;
		jump = maxJumps;
	}
	
	// Update is called once per frame
	void Update () {
		float dt = Time.deltaTime;
		UpdateDirection (dt);
		UpdateVelocity (dt);
		UpdateGravity (dt);
		UpdateJump (dt);
		
	}

	void UpdateDirection(float dt){
		float horAxis = Input.GetAxis ("Horizontal");
		transform.Rotate (0.0f, velRot * horAxis * dt, 0.0f, Space.World);
	}

	void UpdateVelocity(float dt){
		float verAxis = Input.GetAxis ("Vertical");
		Vector3 dir = transform.position - cam.position;
		dir.y = 0;
		Vector3 targetVel = dir * maxVel * verAxis;
		vel = rb.velocity;
		Vector3 offset = targetVel - vel;
		float acc = maxAcc * dt;
		float offsetMagnitude = offset.magnitude;
		offsetMagnitude = Mathf.Clamp (offsetMagnitude, -acc, acc);
		offset = offset.normalized * offsetMagnitude;
		//vel += offset; //No Friction
		vel += offset - vel * frictionCoef * dt;  //With Friction
		rb.velocity = vel;
	}
	void UpdateGravity (float dt){
		if (onAir) {
			Vector3 grav = new Vector3 (0.0f, -gravity, 0.0f);
			vel = rb.velocity;
			vel += grav * dt;
			rb.velocity = vel;
		}
	}
	void UpdateJump (float dt){
		if (jump >= 0 && Input.GetKeyDown (KeyCode.Space)) {
			Vector3 aux = new Vector3 (0.0f, jumpForce, 0.0f);
			vel = rb.velocity;
			//vel += aux * dt;
			vel += aux;
			rb.velocity = vel;
			jump--;
		}
	}

	//Check enter and exit of collision to activate or disactivate gravity and reset jumps
	void OnCollisionEnter (Collision collision){
		if (collision.gameObject.GetComponent<Floor_Controller> ()) {
			onAir = false;
			jump = maxJumps;
		}
	}
	void OnCollisionExit (Collision collision){
		if (collision.gameObject.GetComponent<Floor_Controller> ()) {
			onAir = true;
		}
	}

}
