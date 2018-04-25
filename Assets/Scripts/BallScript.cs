using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour {

	private Vector3 paddleToBall;
	private PaddleScript paddle;
	private Vector3 mousePos;

	public bool ballLaunched;

	void Start () {

		paddle = GameObject.FindObjectOfType<PaddleScript> ();
		ballLaunched = false;
		paddleToBall = this.transform.position - paddle.transform.position;
		InvokeRepeating ("decreaseScore", 1.0f, 3.0f);
	}

	void Update () {
		
		if (ballLaunched == false) {
			transform.position = paddle.transform.position + paddleToBall;

			if (Input.GetMouseButtonUp (0) || Input.GetKeyDown (KeyCode.Space)) {
				mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (mousePos.x - transform.position.x, mousePos.y - transform.position.y).normalized * 7f;
				ballLaunched = true;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {

		Vector2 tweak = new Vector2 (Random.Range (0.0f, 0.2f), Random.Range (0.0f, 0.2f));
		this.GetComponent<Rigidbody2D> ().velocity += tweak;
	}

	void decreaseScore() {

		if (ballLaunched == true && PlayerScript.score > 0) {
			PlayerScript.score--;
		}
		Text scoreText = GameObject.Find ("ScoreValue").GetComponent<Text> ();
		scoreText.text = PlayerScript.score.ToString ();
	}
}