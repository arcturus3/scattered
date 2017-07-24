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
		BallCount.transform.GetChild(0).gameObject.GetComponent<Slider>().value = 20;
		BallSpeed.transform.GetChild(0).gameObject.GetComponent<Slider>().value = 5; 
		GameTime.transform.GetChild(0).gameObject.GetComponent<Slider>().value = 30;
	}
}
