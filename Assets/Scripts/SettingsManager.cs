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
	}
	public void UseDefaults() {
		BallCount.transform.GetChild(0).gameObject.GetComponent<Slider>().value = 4;
		BallSpeed.transform.GetChild(0).gameObject.GetComponent<Slider>().value = 5; 
		GameTime.transform.GetChild(0).gameObject.GetComponent<Slider>().value = 6;
		gameManager.ballCount = 20;
		gameManager.ballSpeed = 5;
		gameManager.gameTime = 30;
	}

	public void SetBallCount() {
		gameManager.ballCount = (int) BallCount.transform.GetChild(0).gameObject.GetComponent<Slider>().value * 5;
		BallCount.transform.GetChild(2).gameObject.GetComponent<Text>().text = gameManager.ballCount.ToString();
	}

	public void SetBallSpeed() {
		gameManager.ballSpeed = (int) BallSpeed.transform.GetChild(0).gameObject.GetComponent<Slider>().value;
		BallSpeed.transform.GetChild(2).gameObject.GetComponent<Text>().text = gameManager.ballSpeed.ToString();		
	}

	public void SetGameTime() {
		gameManager.gameTime = (int) GameTime.transform.GetChild(0).gameObject.GetComponent<Slider>().value * 5;
		GameTime.transform.GetChild(2).gameObject.GetComponent<Text>().text = gameManager.gameTime.ToString() + " <size=30>seconds</size>";		
	}
}
