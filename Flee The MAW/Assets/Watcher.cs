using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watcher : MonoBehaviour {

	[SerializeField] GameObject player;

	void Start () {
		
	}


	void LateUpdate () {
		//Vector3 findPlayer = Vector3.RotateTowards(transform.forward, player.transform.position, 1, 1);
		//transform.position = Vector3.up * player.transform.position.y;
		Vector3 playerAngle = new Vector3(player.transform.position.x, 0, player.transform.position.z);
		transform.LookAt(playerAngle);
	}
}
