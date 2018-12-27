using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ascender : MonoBehaviour {

	float ascendSpeed = .5f;

	void Start () {
		
	}


	void Update () {
		Vector3 ascendPoint = new Vector3(0, transform.position.y + (ascendSpeed * Time.deltaTime), 0);
		transform.position = ascendPoint;
	}
}
