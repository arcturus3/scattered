using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class ShopManager : MonoBehaviour {

	public GameObject ballColorButton;
	public GameObject selectionIndicator;
	public string[] colors = {"000000", "E53935", "8E24AA", "1E88E5", "00897B", "43A047", "FFB300", "6D4C41"};
	private BallColorButton[] buttons;

	void Start() {
		if (!PlayerPrefs.HasKey("LockedBallColors")) {
			PlayerPrefs.SetString("LockedBallColors", "1 2 3 4 5 6 7 ");
		}
		
		buttons = new BallColorButton[colors.Length]; 
		int row = 0;
		int col = 0;
		while (row < Math.Ceiling((decimal) colors.Length / 4)) {
			while (col < 4 && (row * 4) + col < colors.Length) {
				int ballCount = (row * 4) + col;
				GameObject newButton = Instantiate(ballColorButton, GameObject.Find("Button Container").transform);
				newButton.GetComponent<RectTransform>().localPosition = new Vector3((150 * col) - 225, -150 * (row + 1), 0);
				if (PlayerPrefs.GetString("LockedBallColors").Contains(ballCount.ToString() + " ") && PlayerPrefs.GetString("LockedBallColors").Length != 0) {
					buttons[ballCount] = new BallColorButton(ballCount, colors[ballCount], newButton, true);
				}
				else {
					buttons[ballCount] = new BallColorButton(ballCount, colors[ballCount], newButton, false);
				}
				newButton.GetComponent<Button>().onClick.AddListener(delegate {OnColorSelect(ballCount);});
				col++;
			}
			row++;
			col = 0;
		}
		
		SetSelectionIndicatorPos(PlayerPrefs.GetInt("BallColorID"));
	}

	public void OnColorSelect(int id) {
		if (!PlayerPrefs.GetString("LockedBallColors").Contains(id.ToString() + " ") || PlayerPrefs.GetString("LockedBallColors").Length == 0) {
			PlayerPrefs.SetInt("BallColorID", id);
			SetSelectionIndicatorPos(PlayerPrefs.GetInt("BallColorID"));
			GameObject.Find("Click").GetComponent<AudioSource>().Play();
		}
	}

	private void SetSelectionIndicatorPos(int id) {
		selectionIndicator.GetComponent<RectTransform>().localPosition = new Vector3((150 * (id % 4)) - 225, -150 * (((int) id / 4) + 1), 0);
	}

	public void ShowAd() {
		var options = new ShowOptions();
		options.resultCallback = HandleShowResult;

		Advertisement.Show("rewardedVideo", options);
	}

	void HandleShowResult(ShowResult result) {
		if (result == ShowResult.Finished) {
			if (PlayerPrefs.GetString("LockedBallColors").Length == 0) {
				return;
			}
			int random = UnityEngine.Random.Range(0, PlayerPrefs.GetString("LockedBallColors").Length);
			int randomID;
			if (PlayerPrefs.GetString("LockedBallColors")[random] == ' ') {
				randomID = PlayerPrefs.GetString("LockedBallColors")[random - 1] - '0';
			}
			else {
				randomID = PlayerPrefs.GetString("LockedBallColors")[random] - '0';
			}

			if (PlayerPrefs.GetString("LockedBallColors")[random] == ' ') {
				PlayerPrefs.SetString("LockedBallColors", PlayerPrefs.GetString("LockedBallColors").Remove(random - 1, 2));
			}
			else {
				PlayerPrefs.SetString("LockedBallColors", PlayerPrefs.GetString("LockedBallColors").Remove(random, 2));
			}
			buttons[randomID].Unlock();
		}
	}
}