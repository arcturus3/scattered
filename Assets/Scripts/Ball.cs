using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Ball : MonoBehaviour {

	private bool target = false;
	private bool menuOpen = false;
	private Rigidbody rb;
	private GameManager gameManager;	
	private GameObject ballContainer;
	private GameObject ballMenu;	

	private void Start() {
		rb = GetComponent<Rigidbody>();
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		ballContainer = GameObject.FindGameObjectWithTag("BallContainer");
		ballMenu = gameObject.transform.GetChild(0).gameObject;
	}

	private void FixedUpdate() {
		rb.velocity = gameManager.ballSpeed * rb.velocity.normalized;
	}

	public void Select() {
		if (!gameManager.started) {
			target = true;
			gameManager.MoveBalls();
		}

		else if (gameManager.choosing) {
			if (target) {
				gameManager.GameOver("You win!");
				GameObject.Find("Win Sound").GetComponent<AudioSource>().Play();
			}
			else {
				gameManager.GameOver("You lose!");
				GameObject.Find("Lose Sound").GetComponent<AudioSource>().Play();
			}
		}
	}

	public void Move() {		
		rb.velocity = Random.onUnitSphere * gameManager.ballSpeed;			
	}

	public void Stop() {		
		rb.velocity = Vector3.zero;
	}

	public void Remove() {
		if (target) {
			gameManager.GameOver("You lose!");
			GameObject.Find("Lose Sound").GetComponent<AudioSource>().Play();
		}
		else if (ballContainer.transform.childCount == 1) {
			gameManager.GameOver("You lose!");
			GameObject.Find("Lose Sound").GetComponent<AudioSource>().Play();
		}
		else {
			GameObject.Find("Click").GetComponent<AudioSource>().Play();
		}
		Destroy(gameObject);
	}

	public void EnableBallMenu() {
		if (!menuOpen && gameManager.choosing) {
			ballMenu.SetActive(true);
			if (ballMenu.transform.parent.position.y < 2) {
				ballMenu.transform.localPosition = new Vector3(0, 0.8f, 0);
			}
			ballMenu.transform.rotation = Camera.main.transform.rotation;
			menuOpen = true;
		}
	}

	public void InitializeDisableBallMenu() {
		if (menuOpen && gameManager.choosing) {
			StartCoroutine("DisableBallMenu", 2f);
		}
	}

	public void CancelDisableBallMenu() {
		StopCoroutine("DisableBallMenu");
	}

	private IEnumerator DisableBallMenu(float time) {
		yield return new WaitForSeconds(time);
		ballMenu.SetActive(false);
		menuOpen = false;
	}
}