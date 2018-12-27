using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	[SerializeField] GameObject watcher;

	Rigidbody myRB;

	int moveSpeed = 50;
	public float moveInput;

	Ray jumpRay;
	public RaycastHit canJump;
	Ray forwardRay;
	public RaycastHit ranIntoSomething;

	Vector3 standingScale = new Vector3(.75f, .75f, .75f);
	Vector3 slideScale = new Vector3(.5f, .5f, .5f);


	void Awake() {
		myRB = GetComponent<Rigidbody>();
	}


	void Start () {
		
	}


	void FixedUpdate () {
		/*TODO Have the player accelerate to a "top speed", instead of just moving at a constant rate
		 */
		DetectForward();

		MovePlayer();

		Jump();

		if (Input.GetKeyDown(KeyCode.LeftShift)) {
			StartCoroutine("Slide");
		}
	}

	private void MovePlayer() {
		if (Input.GetAxisRaw("Horizontal") != 0) {
			if (ranIntoSomething.collider != null && canJump.collider == null) {
				moveInput = 0;
			}
			else {
				moveInput = -(moveSpeed * Input.GetAxisRaw("Horizontal") * Time.deltaTime);
				transform.RotateAround(Vector3.zero, Vector3.up, moveInput);
			}
			//Rotate the player
			transform.forward = (Vector3.up * transform.position.y) - transform.position;
			transform.Rotate(transform.up, Mathf.Lerp(transform.rotation.y, -90 * moveInput, 1f), Space.Self);
		}
		else {
			moveInput = 0;
		}

/*TODO Optimise raycasting (not sure about creating a "new" raycast every update)
	*		Figure out the appropriate level of force to apply to the jumps
	*/
	}

	private void Jump() {
		jumpRay = new Ray(transform.position, Vector3.down);

		if (Input.GetKeyDown(KeyCode.Space)) {
			if (Physics.Raycast(jumpRay, out canJump, 1.1f)) {
				Debug.Log(canJump.collider.tag);

				if (canJump.collider.CompareTag("Platform")) {
					myRB.AddForce(Vector3.up * 6f, ForceMode.Impulse);
				}
				else if (canJump.collider.CompareTag("JumpPad")) {
					myRB.AddForce(Vector3.up * 10, ForceMode.Impulse);
				}
			}
		}
		//Debug.DrawRay(transform.position, Vector3.down, Color.red);
	}

	IEnumerator	Slide () {
		transform.localScale = slideScale;
		myRB.AddForce(transform.forward * 10, ForceMode.Force);

		yield return new WaitForSeconds(1.0f);

		transform.localScale = standingScale;
	}


	void DetectForward() {
		Debug.DrawRay(transform.position, transform.forward * .5f);
		forwardRay = new Ray(transform.position, transform.forward);
		if (Physics.Raycast(forwardRay, out ranIntoSomething, .5f)) {
			Debug.Log("Ran into something");
			myRB.AddForce(Vector3.down * .1f, ForceMode.Impulse);
		}
	}
}
