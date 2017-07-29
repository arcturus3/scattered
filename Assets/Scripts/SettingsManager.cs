using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

	public GameObject BallCount;
	public GameObject BallSpeed;
	public GameObject GameTime;
	public GameManager gameManager;

	void Start() {
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

		if (!PlayerPrefs.HasKey("BallCount")) {
			PlayerPrefs.SetInt("BallCount", 4);
			Debug.Log("set ballCount 4");
		}
		if (!PlayerPrefs.HasKey("BallSpeed")) {
			PlayerPrefs.SetInt("BallSpeed", 5);
			Debug.Log("set ballSpeed 5");
		}
		if (!PlayerPrefs.HasKey("GameTime")) {
			PlayerPrefs.SetInt("GameTime", 6);
			Debug.Log("set gameTime 6");
		}

		BallCount.transform.GetChild(0).gameObject.GetComponent<Slider>().value = PlayerPrefs.GetInt("BallCount");
		BallSpeed.transform.GetChild(0).gameObject.GetComponent<Slider>().value = PlayerPrefs.GetInt("BallSpeed"); 
		GameTime.transform.GetChild(0).gameObject.GetComponent<Slider>().value = PlayerPrefs.GetInt("GameTime");

		SetBallCount();
		SetBallSpeed();
		SetGameTime();

		gameObject.SetActive(false);
	}

	public void UseDefaults() {
		BallCount.transform.GetChild(0).gameObject.GetComponent<Slider>().value = 4;
		BallSpeed.transform.GetChild(0).gameObject.GetComponent<Slider>().value = 5; 
		GameTime.transform.GetChild(0).gameObject.GetComponent<Slider>().value = 6;

		gameManager.ballCount = 20;
		gameManager.ballSpeed = 5;
		gameManager.gameTime = 30;

		PlayerPrefs.SetInt("BallCount", 4);
		PlayerPrefs.SetInt("BallSpeed", 5);
		PlayerPrefs.SetInt("GameTime", 6);
	}

	public void SetBallCount() {
		gameManager.ballCount = (int) BallCount.transform.GetChild(0).gameObject.GetComponent<Slider>().value * 5;
		BallCount.transform.GetChild(2).gameObject.GetComponent<Text>().text = gameManager.ballCount.ToString();
		PlayerPrefs.SetInt("BallCount", gameManager.ballCount / 5);
	}

	public void SetBallSpeed() {
		gameManager.ballSpeed = (int) BallSpeed.transform.GetChild(0).gameObject.GetComponent<Slider>().value;
		BallSpeed.transform.GetChild(2).gameObject.GetComponent<Text>().text = gameManager.ballSpeed.ToString();
		PlayerPrefs.SetInt("BallSpeed", gameManager.ballSpeed);		
	}

	public void SetGameTime() {
		gameManager.gameTime = (int) GameTime.transform.GetChild(0).gameObject.GetComponent<Slider>().value * 5;
		GameTime.transform.GetChild(2).gameObject.GetComponent<Text>().text = gameManager.gameTime.ToString() + " <size=30>seconds</size>";
		PlayerPrefs.SetInt("GameTime", gameManager.gameTime / 5);
	}
}
