using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	[SerializeField] GameObject playerPivot;
	[SerializeField] GameObject watcher;
	Watcher watcherScript;

	public Rigidbody myRB;

	int moveSpeed = 50;
	public float moveInput;

	bool allowJump = true;
	public Ray jumpRay;
	public RaycastHit canJump;
	public Ray forwardRay;
	public RaycastHit ranIntoSomething;

	bool allowSlideOrRoll = true;
	public Vector3 standingScale = new Vector3(.75f, .75f, .75f);
	public Vector3 slideScale = new Vector3(.5f, .5f, .5f);


	void Awake() {
		myRB = GetComponent<Rigidbody>();

		watcherScript = watcher.GetComponent<Watcher>();
	}


	void Start () {
		
	}


	void FixedUpdate () {
		/*TODO Have the player accelerate to a "top speed", instead of just moving at a constant rate
		 */
		DetectForward();

		MovePlayer();
		Jump();
		if (Input.GetKeyDown(KeyCode.LeftShift) && allowSlideOrRoll) {
			StartCoroutine("SlideOrRoll");
		}


	}

	private void MovePlayer() {
		if (Input.GetAxisRaw("Horizontal") != 0) {
			if (ranIntoSomething.collider != null && canJump.collider == null) {
				//moveInput = 0;
				allowSlideOrRoll = false;
			}
			else {
				allowSlideOrRoll = true;
				moveInput = -(moveSpeed * Input.GetAxisRaw("Horizontal") * Time.deltaTime);
			}
		}
		else {
			moveInput = 0;
		}
		//Rotate the player
		//transform.RotateAround(watcher.transform.position, Vector3.up, watcherScript.moveInput);
		playerPivot.transform.Rotate(Vector3.up, moveInput, Space.Self);
		transform.localRotation = Quaternion.Euler(Vector3.up * (90 * moveInput));
	}


	private void Jump() {
		if (Input.GetKeyDown(KeyCode.Space) && allowJump) {
			jumpRay = new Ray(transform.position, Vector3.down);

			if (Physics.Raycast(jumpRay, out canJump, 1.1f)) {
				Debug.Log(canJump.collider.tag);

				if (canJump.collider.CompareTag("Platform")) {
					myRB.AddForce(Vector3.up * 7f, ForceMode.Impulse);
				}
				else if (canJump.collider.CompareTag("JumpPad")) {
					myRB.AddForce(Vector3.up * 10, ForceMode.Impulse);
				}
			}
		}
		//Debug.DrawRay(transform.position, Vector3.down, Color.red);
	}


	IEnumerator SlideOrRoll() {
		allowJump = false;
		allowSlideOrRoll = false;
		transform.localScale = slideScale;
		//pMove.myRB.AddForce(player.transform.forward * 10, ForceMode.Impulse);
		//transform.RotateAround(gameObject.transform.position, Vector3.up, 
		//	Mathf.Lerp(transform.rotation.y, transform.rotation.y + (moveInput * Time.deltaTime), 1f));

		yield return new WaitForSeconds(.4f);

		transform.localScale = standingScale;
		allowJump = true;
		allowSlideOrRoll = true;
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
