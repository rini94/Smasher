using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	private LevelManager levelManager;
	private GameObject[] pausedObjects;
	private GameObject pauseButtonObj;
	private Text livesText;

	public static bool isGamePaused;

	void Start () {

		Time.timeScale = 0.7f;
		livesText = GameObject.Find ("LivesValue").GetComponent<Text> ();
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
		livesText.text = PlayerScript.lives.ToString();
		pauseButtonObj = GameObject.Find ("PauseButton");
		pausedObjects = GameObject.FindGameObjectsWithTag("PausedScreen");

		hidePaused ();
		isGamePaused = false;
	}

	void Update () {
		
		if (Input.GetKeyDown (KeyCode.P)) {
			if (Time.timeScale == 0) {
				resumeGame ();
			} else {
				pauseGame ();
			}
		}
	}

	public void pauseGame () {

		levelManager.buttonClickSound ();
		Time.timeScale = 0;
		foreach(GameObject gObj in pausedObjects){
			gObj.SetActive(true);
		}
		pauseButtonObj.SetActive(false);

		isGamePaused = true;
	}

	void hidePaused() {
		
		foreach(GameObject gObj in pausedObjects){
			gObj.SetActive(false);
		}
		pauseButtonObj.SetActive(true);
	}

	public void resumeGame () {

		Time.timeScale = 0.7f;
		levelManager.buttonClickSound ();
		hidePaused ();
		isGamePaused = false;
	}

	public void restartGame () {

		levelManager.buttonClickSound ();
		PlayerScript.lives = 3;
		PlayerScript.score = 0;
		levelManager.loadScene ("Level1");
	}

	public void nextLife() {
		
		GameObject ball = GameObject.Find("Ball");

		if (PlayerScript.lives > 1) {
			PlayerScript.lives -= 1;
			livesText.text = PlayerScript.lives.ToString();

			ball.GetComponent<BallScript> ().ballLaunched = false;

		} else {
			PlayerScript.lives = 3;
			int highscore = 0;
			if (PlayerPrefs.HasKey ("highscore")) {
				highscore = PlayerPrefs.GetInt ("highscore");
			}
			if (highscore < PlayerScript.score) {
				PlayerPrefs.SetInt("highscore",PlayerScript.score);
			}

			levelManager.loadScene ("EndScreen");
		}
	}


}