using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public static int lives;
	public static int score;
	public static PlayerScript instance = null;

	void Awake () {
		
		if (instance == null) {
			
			instance = this;
			lives = 3;
			score = 0;
		} else {
			
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}
}
