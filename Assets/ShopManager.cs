using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

	public GameObject ballColorButton;
	public GameObject selectionIndicator;
	public string[] colors = {"000000", "E53935", "8E24AA", "1E88E5", "00897B", "43A047", "FFB300", "6D4C41"};
	private BallColorButton[] buttons;

	void Start() {
		buttons = new BallColorButton[colors.Length]; 
		int row = 0;
		int col = 0;
		while (row < Math.Ceiling((decimal) colors.Length / 4)) {
			while (col < 4 && (row * 4) + col < colors.Length) {
				int ballCount = (row * 4) + col;
				GameObject newButton = Instantiate(ballColorButton, GameObject.Find("Button Container").transform);
				newButton.GetComponent<RectTransform>().localPosition = new Vector3((150 * col) - 225, -150 * (row + 1), 0);
				buttons[ballCount] = new BallColorButton(ballCount, colors[ballCount], newButton);
				newButton.GetComponent<Button>().onClick.AddListener(delegate {OnColorSelect(ballCount);});
				col++;
			}
			row++;
			col = 0;
		}
		
		if (!PlayerPrefs.HasKey("BallColorID")) {
			PlayerPrefs.SetInt("BallColorID", 0);
		}
		SetSelectionIndicatorPos(PlayerPrefs.GetInt("BallColorID"));
	}

	public void OnColorSelect(int id) {
		PlayerPrefs.SetInt("BallColorID", id);
		SetSelectionIndicatorPos(PlayerPrefs.GetInt("BallColorID"));
		GameObject.Find("Click").GetComponent<AudioSource>().Play();
	}

	private void SetSelectionIndicatorPos(int id) {
		selectionIndicator.GetComponent<RectTransform>().localPosition = new Vector3((150 * (id % 4)) - 225, -150 * (((int) id / 4) + 1), 0);
	}
}