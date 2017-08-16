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
			AudioListener.volume = 0;
			gameObject.GetComponentInChildren<Image>().sprite = audioOff;
			PlayerPrefs.SetString("AudioState", "on");
		}
		else {
			AudioListener.volume = 1;
			gameObject.GetComponentInChildren<Image>().sprite = audioOn;
			PlayerPrefs.SetString("AudioState", "off");
		}
	}
}
