using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField] GameObject player;
	[SerializeField] GameObject watcher;

	Vector3 offset;


	void Start () {
	}


	void FixedUpdate () {
		Vector3 camFocus = new Vector3(watcher.transform.position.x, player.transform.position.y, watcher.transform.position.z);
		transform.LookAt(player.transform.position);

		//transform.RotateAround(Vector3.zero, Vector3.up, player.GetComponent<PlayerMove>().moveInput);
		Vector3 updatedPos = new Vector3(0, player.transform.position.y, 0);

		offset = (watcher.transform.forward * 15) + (Vector3.up * (player.transform.position.y + 2));

		transform.position = offset;
	}
}
