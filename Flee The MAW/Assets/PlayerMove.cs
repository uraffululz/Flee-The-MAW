using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	[SerializeField] GameObject watcher;

	Rigidbody myRB;

	int moveSpeed = 50;
	public float moveInput;
	public float myAngleAroundCenter;



	Ray jumpRay;
	RaycastHit canJump;
	Ray forwardRay;
	RaycastHit ranIntoSomething;



	void Awake() {
		myRB = GetComponent<Rigidbody>();
	}


	void Start () {
		
	}


	void Update () {
		/*TODO Have the player accelerate to a "top speed", instead of just moving at a constant rate
		 */
		DetectForward();

		MovePlayer();
	}

	private void MovePlayer() {
		if (Input.GetAxisRaw("Horizontal") != 0) {
			if (ranIntoSomething.collider != null && canJump.collider == null) {
				moveInput = 0;
			}
			else {
				moveInput = -(moveSpeed * Input.GetAxisRaw("Horizontal") * Time.deltaTime);
				transform.RotateAround(Vector3.zero, Vector3.up, moveInput);
				myAngleAroundCenter = watcher.transform.rotation.y;

				transform.forward = (Vector3.up * transform.position.y) - transform.position;
				transform.Rotate(transform.up, Mathf.Lerp(transform.rotation.y, -90 * moveInput, 1f), Space.Self);
			}
		}
		else {
			moveInput = 0;
		}

		/*TODO Optimise raycasting (not sure about creating a "new" raycast every update)
		 *		Figure out the appropriate level of force to apply to the jumps
		 */

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
		Debug.DrawRay(transform.position, Vector3.down, Color.red);
	}


	void DetectForward() {
		Debug.DrawRay(transform.position, transform.forward);
		forwardRay = new Ray(transform.position, transform.forward);
		if (Physics.Raycast(forwardRay, out ranIntoSomething, 1f)) {
			Debug.Log("Ran into something");
			myRB.AddForce(Vector3.down * .1f, ForceMode.Impulse);
		}
	}
}
