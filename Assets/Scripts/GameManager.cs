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

	private void Start() {
		ballContainer = GameObject.FindGameObjectWithTag("BallContainer");
		mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
		gameMenu = GameObject.FindGameObjectWithTag("GameMenu");
		outcomeMenu = GameObject.FindGameObjectWithTag("OutcomeMenu");	
		gameMenu.SetActive(false);
		outcomeMenu.SetActive(false);
	}

	private void SpawnBalls2() {
		Vector3[] ballPositions = new Vector3[ballCount];

		for (int i = 0; i < ballCount; i++) {
			Vector3 vector;
			bool intersects;

			do {
				vector = new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(0.5f, 9.5f), Random.Range(-4.5f, 4.5f));
				intersects = false;

				for (int j = 0; j < i; j++) {
					if (Mathf.Abs(vector.x - ballPositions[j].x) <= 1 && Mathf.Abs(vector.y - ballPositions[j].y) <= 1 && Mathf.Abs(vector.z - ballPositions[j].z) <= 1) {
						intersects = true;
					}
				}

				if (Mathf.Abs(vector.x) <= 1 && Mathf.Abs(vector.y - 2) <= 1 && Mathf.Abs(vector.z) <= 1) {
					intersects = true; //too close to camera
				}
			} while (intersects);

			ballPositions[i] = vector;			
			GameObject newBall = Instantiate(ball, vector, Quaternion.identity);
			newBall.transform.parent = ballContainer.transform;
		}
	}

	private void SpawnBalls() {
		Vector3[] ballPositions = new Vector3[ballCount];

		for (int i = 0; i < ballCount; i++) {
			Vector3 ballPosition;
			bool validPosition;

			do {
				Ray raycast = new Ray(Vector3.zero, Random.onUnitSphere);
				RaycastHit raycastHit;
				Vector3 hitPosition;
				
				//raycast.origin = raycast.GetPoint(100);
				raycast.direction = -raycast.direction;
				

				Debug.DrawRay(raycast.origin, raycast.direction, Color.white, 100f);

				if (Physics.Raycast(raycast, out raycastHit)) {
					hitPosition = raycastHit.point;
				}

				ballPosition = hitPosition * Random.Range(0f, (hitPosition.magnitude - 1)); //balls don't spawn at edges
				validPosition = false;

				for (int j = 0; j < i; j++) {
					if (Mathf.Abs(ballPosition.x - ballPositions[j].x) <= 1 && Mathf.Abs(ballPosition.y - ballPositions[j].y) <= 1 && Mathf.Abs(ballPosition.z - ballPositions[j].z) <= 1) {
						validPosition = true;
					}
				}

				if (Mathf.Abs(ballPosition.x) <= 1 && Mathf.Abs(ballPosition.y - 2) <= 1 && Mathf.Abs(ballPosition.z) <= 1) {
					validPosition = true; //too close to camera
				}
			} while (validPosition);

			ballPositions[i] = ballPosition;			
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