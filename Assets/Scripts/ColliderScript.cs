using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour {

	private GameController gameController;

	void Start () {
		gameController = GameObject.FindObjectOfType<GameController> ();
	}

	void OnCollisionEnter2D(Collision2D collision) {

		StartCoroutine(waitAndRespawn());
	}

	IEnumerator waitAndRespawn () {

		Time.timeScale = 0;
		GameController.isGamePaused = true;
		yield return new WaitForSecondsRealtime(1);
		GameController.isGamePaused = false;
		Time.timeScale = 0.7f;
		gameController.nextLife ();
	}
}