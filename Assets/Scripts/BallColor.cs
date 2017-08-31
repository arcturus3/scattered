using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BallColorButton {

	public int ID {get;}
	public string ColorValue {get;}
	public bool Locked {get; set;}
	public bool Selected {get; set;}
	public GameObject Button {get;}

	public BallColorButton(int id, string val, GameObject btn, bool locked) {
		ID = id;
		ColorValue = val;
		Button = btn;
		Locked = locked;
		Selected = false;

		Button.GetComponent<Image>().color = new Color32(
			(byte) Convert.ToInt32(ColorValue.Substring(0, 2), 16),
			(byte) Convert.ToInt32(ColorValue.Substring(2, 2), 16),
			(byte) Convert.ToInt32(ColorValue.Substring(4, 2), 16),
			255);

		if (Locked) {
			Button.transform.GetChild(0).gameObject.SetActive(true);
		}
	}

	public void Unlock() {
		Locked = false;
		Button.transform.GetChild(0).gameObject.SetActive(false);
	}
}
