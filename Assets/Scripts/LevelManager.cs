using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public AudioClip buttonSound;

	public void loadScene (string sceneName) {
		SceneManager.LoadScene (sceneName);
	}
	 
	public void loadNextLevel() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void quitButton () {

		buttonClickSound ();
		Application.Quit ();
	}

	public void buttonClickSound() {

		AudioSource.PlayClipAtPoint (buttonSound, transform.position);
	}

	public void resetGame () {

		buttonClickSound ();
		PlayerScript.lives = 3;
		PlayerScript.score = 0;
		loadScene ("MainMenu");
	}
}