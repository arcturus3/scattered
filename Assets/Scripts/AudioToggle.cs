using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioToggle : MonoBehaviour {

	public Sprite audioOn;
	public Sprite audioOff;
	private Toggle toggle;

	private void Start() {
		toggle = GetComponent<Toggle>();
	}

	public void Toggle() {
		if (toggle.isOn) {
			AudioListener.pause = false;
			gameObject.GetComponentInChildren<Image>().sprite = audioOff;
		}
		else {
			AudioListener.pause = true;
			gameObject.GetComponentInChildren<Image>().sprite = audioOn;
		}
	}
}
