using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FadeSound : MonoBehaviour {

	private float speed = 3f;
	private AudioSource sound;

	private void Start() {
		sound = GetComponent<AudioSource>();
	}

	public void PlaySound() {
		sound.Play();
		StartCoroutine("FadeIn");
	}

	public void StopSound() {
		StartCoroutine("FadeOut");
		sound.Stop();
	}

	private IEnumerator FadeIn() {
		while (sound.volume < 1) {
			sound.volume += Time.deltaTime / speed;
			yield return null;
		}
		sound.volume = 1;
	}

	private IEnumerator FadeOut() {
		while (sound.volume > 0) {
			sound.volume -= Time.deltaTime / speed;
			yield return null;
		}
		sound.volume = 0;
	}
}
