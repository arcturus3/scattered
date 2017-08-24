using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BallColorButton {

	public int ID {get;}
	public string ColorValue {get;}
	public bool Unlocked {get; set;}
	public bool Selected {get; set;}
	public GameObject Button {get;}

	public BallColorButton(int id, string val, GameObject btn) {
		ID = id;
		ColorValue = val;
		Button = btn;
		Unlocked = false;
		Selected = false;

		Button.GetComponent<Image>().color = new Color32(
			(byte) Convert.ToInt32(ColorValue.Substring(0, 2), 16),
			(byte) Convert.ToInt32(ColorValue.Substring(2, 2), 16),
			(byte) Convert.ToInt32(ColorValue.Substring(4, 2), 16),
			255);
	}
}
