using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMover : MonoBehaviour {

	Transform transform;
	Camera camera;

	void Start() {
		transform = GetComponent<Transform>();
		camera = Camera.main;
	}
	
	void Update() {
		if (camera.transform.eulerAngles.y < 90 || camera.transform.eulerAngles.y > 270) {
			transform.position = new Vector3(0, 3.5f, 3);
			transform.rotation = Quaternion.Euler(0, 0, 0);
		}
		else {
			transform.position = new Vector3(0, 3.5f, -3);
			transform.rotation = Quaternion.Euler(0, 180, 0);
		}
	}
}