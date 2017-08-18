using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

	public int ballCount;
	public int ballSpeed;	
	public int gameTime;
    public GameObject ball;
	private GameObject ballContainer;
	private GameObject mainMenu;
	private GameObject gameMenu;
	private GameObject outcomeMenu;
	[HideInInspector]
	public bool started = false;
	[HideInInspector]
	public bool choosing = false;
	private bool setNewGameMenuCameraRotation = true;

	private void Start() {
		ballContainer = GameObject.FindGameObjectWithTag("BallContainer");
		mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
		gameMenu = GameObject.FindGameObjectWithTag("GameMenu");
		outcomeMenu = GameObject.FindGameObjectWithTag("OutcomeMenu");	
		gameMenu.SetActive(false);
		outcomeMenu.SetActive(false);
	}

	void Update() {
		if (!mainMenu.active && !outcomeMenu.active) {
			if (Camera.main.transform.eulerAngles.x > 65 && Camera.main.transform.eulerAngles.x < 115) {
				gameMenu.SetActive(true);
				if (setNewGameMenuCameraRotation) {
					gameMenu.transform.rotation = Quaternion.Euler(270, Camera.main.transform.eulerAngles.y + 180, 0);
					setNewGameMenuCameraRotation = false;
				}
			}
			else {
				gameMenu.SetActive(false);
				setNewGameMenuCameraRotation = true;
			}
		}
	}

	private void SpawnBalls() {
		for (int i = 0; i < ballCount; i++) {
			Vector3 ballPosition;
			bool validPosition;

			do {
				ballPosition = new Vector3(Random.Range(-9.5f, 9.5f), Random.Range(0.5f, 4.5f), Random.Range(-9.5f, 9.5f));

				if (Physics.Raycast(ballPosition, Vector3.up) && Physics.OverlapSphere(ballPosition, 0.5f).Length == 0) {
					validPosition = true;
				}
				else {					
					validPosition = false;
				}

				if (Mathf.Abs(ballPosition.x) <= 1 && Mathf.Abs(ballPosition.y - 2) <= 1 && Mathf.Abs(ballPosition.z) <= 1) {
					validPosition = false; //too close to camera
				}
			} while (!validPosition);
			
			GameObject newBall = Instantiate(ball, ballPosition, Quaternion.identity);
			newBall.transform.parent = ballContainer.transform;
		}
	}

	public void MoveBalls() {
		started = true;
		ToggleBallEventTriggers(false);
		StartCoroutine("GameEndTimer", gameTime);
		foreach (Transform child in ballContainer.transform) {
			child.gameObject.GetComponent<Ball>().Move();
		}
	}

	private void StopBalls() {
		choosing = true;
		ToggleBallEventTriggers(true);
		foreach (Transform child in ballContainer.transform) {
			child.gameObject.GetComponent<Ball>().Stop();
		}
	}

	private void DestroyBalls() {
		foreach (Transform child in ballContainer.transform) {
			Object.Destroy(child.gameObject);
		}
	}

	private void ToggleBallEventTriggers(bool state) {
		foreach (Transform child in ballContainer.transform) {
			child.gameObject.GetComponent<EventTrigger>().enabled = state;
		}
	}

	public void BeginGame() {
		SpawnBalls();
		mainMenu.SetActive(false);
		gameMenu.SetActive(true);
	}

	public void Restart() {
		DestroyBalls();
		StopCoroutine("GameEndTimer");
		started = false;		
		choosing = false;
		SpawnBalls();
		outcomeMenu.SetActive(false);
		gameMenu.SetActive(true);
	}

	public void EndGame() {
		DestroyBalls();
		StopCoroutine("GameEndTimer");
		mainMenu.SetActive(true);
		gameMenu.SetActive(false);
		outcomeMenu.SetActive(false);
		started = false;		
		choosing = false;
	}

	public void GameOver(string message) {
		DestroyBalls();
		outcomeMenu.SetActive(true);
		gameMenu.SetActive(false);
		outcomeMenu.transform.GetChild(0).gameObject.GetComponent<Text>().text = message;
	}

	private IEnumerator GameEndTimer(int seconds) {
		yield return new WaitForSeconds((float)seconds);
		StopBalls();
	}
}