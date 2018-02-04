using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	private bool showMenu = false;
	private Vector3[] teleports;
	private GameObject player;

	public float range = 75;

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag != "Player") {
			return;
		}
		UpdateTeleports ();
		player = other.gameObject;
		showMenu = true;
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag != "Player") {
			return;
		}
		player = null;
		showMenu = false;
	}

	void OnGUI()
	{   
		if (!showMenu) {
			return;
		}

		for (int i = 0; i < teleports.Length; ++i) {
			if (GUI.Button (new Rect (5, 5 + 25 * i, 150, 20), teleports[i].ToString())) {
				player.transform.position = teleports [i];
			}
		}
	}

	private void UpdateTeleports() {
		List<Vector3> result = new List<Vector3> ();
		float distance = 0;

		foreach(GameObject teleport in GameObject.FindGameObjectsWithTag ("Teleport")) {
			distance = Vector3.Distance (teleport.transform.position, transform.position);
			if (distance <= range && distance > 0 ) {
				result.Add (teleport.transform.position);
			}
		}
		teleports = result.ToArray ();
	}
}
