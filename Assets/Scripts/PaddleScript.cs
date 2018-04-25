using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour {

	private LineRenderer lineRend;
	Vector3 paddlePos = new Vector3 (6.4f, 0.09f, 0.0f);
	private BallScript ball;
	private float mousePosX;

	public bool autoPlay;

	void Start () {
		lineRend = GetComponent<LineRenderer>();
		ball = GameObject.FindObjectOfType<BallScript> ();
	}
	void Update () {

		if (GameController.isGamePaused == false) {

			if (ball.ballLaunched == false) {
				lineRend.enabled = true;
				lineRend.SetPosition (0, paddlePos);
				Vector2 CursorPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				lineRend.SetPosition (1, CursorPosition);
				lineRend.material = new Material(Shader.Find("Sprites/Default"));

			} else {
				lineRend.enabled = false;
			}

			if (autoPlay == false) {
				Vector3 screenPos = Camera.main.WorldToScreenPoint (transform.position);

				//Move with keyboard
				if (Input.GetKey (KeyCode.RightArrow) && screenPos.x < Screen.width) {
					paddlePos.x = paddlePos.x + 0.1f;
				} else if (Input.GetKey (KeyCode.LeftArrow) && screenPos.x > 0) {
					paddlePos.x = paddlePos.x - 0.1f;
				}

				//Move with mouse
				if (Input.GetMouseButton (0) && Input.mousePosition.x > screenPos.x && screenPos.x < Screen.width) {
					paddlePos.x = paddlePos.x + 0.1f;
				} else if (Input.GetMouseButton (0) && Input.mousePosition.x < screenPos.x && screenPos.x > 0) {
					paddlePos.x = paddlePos.x - 0.1f;
				}

			} else {
				paddlePos.x = ball.transform.position.x;
			}

			this.transform.position = paddlePos;
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		
		if (ball.ballLaunched == true) {
			this.GetComponent<AudioSource> ().Play ();
		}
	}
}
