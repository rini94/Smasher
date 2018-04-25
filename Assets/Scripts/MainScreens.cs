using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScreens : MonoBehaviour {

	private LevelManager levelManager;
	private GameObject[] mainScreenObjects;
	private GameObject[] instructionObjects;

	void Start () {

		levelManager = GameObject.FindObjectOfType<LevelManager> ();

		if (SceneManager.GetActiveScene ().name == "EndScreen") {
			Text score = GameObject.Find ("EndScore").GetComponent<Text> ();
			score.text = PlayerScript.score.ToString ();

		} else if (SceneManager.GetActiveScene ().name == "MainMenu") {

			instructionObjects = GameObject.FindGameObjectsWithTag("InstructionsScreen");
			mainScreenObjects = GameObject.FindGameObjectsWithTag("MainScreen");

			hideInstructions ();

			Text highestScore = GameObject.Find ("HighestScoreValue").GetComponent<Text> ();
			if (PlayerPrefs.HasKey ("highscore")) {
				highestScore.text = PlayerPrefs.GetInt ("highscore").ToString ();
			} else {
				highestScore.text = "0";
			}
		}
	}

	public void showInstructions () {

		levelManager.buttonClickSound ();
		foreach (GameObject gObj in mainScreenObjects) {
			gObj.SetActive (false);
		}
		foreach (GameObject gObj in instructionObjects) {
			gObj.SetActive (true);
		}
	}

	public void hideInstructions () {

		levelManager.buttonClickSound ();
		foreach (GameObject gObj in instructionObjects) {
			gObj.SetActive (false);
		}
		foreach (GameObject gObj in mainScreenObjects) {
			gObj.SetActive (true);
		}
	}
}
