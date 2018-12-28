using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watcher : MonoBehaviour {

	[SerializeField] GameObject player;
	PlayerMove pMove;

	int moveSpeed = 50;
	public float moveInput;

	void Start () {
		pMove = player.GetComponent<PlayerMove>();

	}


	void FixedUpdate () {
		Vector3 watchPlayer = new Vector3(player.transform.position.x, 0, player.transform.position.z);
		transform.LookAt(watchPlayer);
	}
}
