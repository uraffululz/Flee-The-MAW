using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {

	[SerializeField] GameObject spawner;

	[SerializeField] GameObject[] platforms;
	[SerializeField] GameObject basePlatform;
	[SerializeField] GameObject fullWallPrefab;
	[SerializeField] GameObject jumpPad;
	[SerializeField] GameObject nullGap;

	int recentPlatInt;
	GameObject recentPlatform = null;

	int howManyRingsSpawned = 0;
	

	void Awake() {
		for (int i = 1; i <= 3; i++) {
			transform.position = Vector3.up * (i * 4.5f);
			SpawnRing();
		}
	}


	void Start () {
		
	}


	void Update () {

	}


	void SpawnRing() {
		howManyRingsSpawned++;
		GameObject ringParent = new GameObject("RingParent" + howManyRingsSpawned);
		ringParent.transform.position = transform.position;

		for (int i = 0; i <= 300; i += 15) {
			if (i == 0) {
				ringParent.transform.Rotate(Vector3.up, 15 * howManyRingsSpawned);
				GameObject fullWall = Instantiate(fullWallPrefab, spawner.transform.position, Quaternion.identity, ringParent.transform);
				fullWall.transform.Rotate(-90, 0, 0);

				ringParent.transform.Rotate(Vector3.up, 15);
				GameObject jumper = Instantiate(jumpPad, spawner.transform.position, Quaternion.identity, ringParent.transform);
				jumper.transform.Rotate(-90, 0, 0);
			}
			else if (i == 300) {
				ringParent.transform.Rotate(Vector3.up, 15);
				GameObject basePlat = Instantiate(basePlatform, spawner.transform.position, Quaternion.identity, ringParent.transform);
				basePlat.transform.Rotate(-90, 0, 0);
			}
			else if (i%2 == 1) {
				ringParent.transform.Rotate(Vector3.up, 15);
				GameObject basePlat = Instantiate(basePlatform, spawner.transform.position, Quaternion.identity, ringParent.transform);
				basePlat.transform.Rotate(-90, 0, 0);
			}
			else {
				ringParent.transform.Rotate(Vector3.up, 15);

				int randomPlatform = Random.Range(0, platforms.Length);

				if (randomPlatform == recentPlatInt) {
					ringParent.transform.Rotate(Vector3.up, -15);
					i -= 15;

				}
				else {
					recentPlatInt = randomPlatform;

					GameObject newPlatform = Instantiate(platforms[randomPlatform], spawner.transform.position, Quaternion.identity, ringParent.transform);
					newPlatform.transform.Rotate(-90, 0, 0);
					recentPlatform = newPlatform;
				}
			}
			//Debug.Log(ringParent.transform.rotation.eulerAngles);
			//Debug.Log(spawner.transform.position);
		}
		ringParent.transform.Rotate(Vector3.up, -15 - (30 * howManyRingsSpawned));
		transform.Rotate(Vector3.up, -transform.rotation.y);
		spawner.transform.localPosition = Vector3.forward * -8.65f;
	}
}
