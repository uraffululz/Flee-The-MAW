using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MawController : MonoBehaviour {

	MeshCollider mawCol;


	void Start () {
		mawCol = GetComponent<MeshCollider>();
	}


	void Update () {
	
	}


	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("RingParent")) {
			Destroy(other.gameObject);
		}
	}
}
