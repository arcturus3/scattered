using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ShopManager : MonoBehaviour {

	public GameObject ballColorButton;
	private string[] colors = {"E53935", "8E24AA", "1E88E5", "00897B", "43A047", "FFB300", "6D4C41", "000000"};
	private BallColorButton[] buttons;

	void Start() {
		buttons = new BallColorButton[colors.Length]; 
		int row = 0;
		int col = 0;
		while (row < Math.Ceiling((decimal) colors.Length / 4)) {
			while (col < 4 && (row * 4) + col < colors.Length) {
				int ballCount = (row * 4) + col;
				GameObject newButton = Instantiate(ballColorButton, GameObject.Find("Shop Screen").transform);
				newButton.GetComponent<RectTransform>().localPosition = new Vector3((150 * col) - 225, -150 * (row + 1), 0);
				buttons[ballCount] = new BallColorButton(ballCount, colors[ballCount], newButton);
				col++;
			}
			row++;
			col = 0;
		}
	}
}
