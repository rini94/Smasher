using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickScript : MonoBehaviour {

	private LevelManager levelManager;
	private Text scoreText;
	private int hits;
	private static int bricksCount = 0;

	public AudioClip hitSound;
	public AudioClip crackSound;
	public Sprite[] brickSprite;
	public GameObject smoke;

	void Start () {

		if (gameObject.tag == "Breakable") {
			bricksCount++;
		}
		scoreText = GameObject.Find ("ScoreValue").GetComponent<Text> ();
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
		hits = 0;
	}

	void OnCollisionEnter2D(Collision2D collision) {

		if(gameObject.tag == "Breakable") {
			
			hits++;
			int maxHits = brickSprite.Length + 1;
			if (hits >= maxHits) {
				
				bricksCount--;
				AudioSource.PlayClipAtPoint (hitSound, transform.position);

				if (maxHits == 3) {
					PlayerScript.score += 15;
				} else if (maxHits == 2) {
					PlayerScript.score += 10;
				} else {
					PlayerScript.score += 5;
				}
				scoreText.text = PlayerScript.score.ToString ();

				GameObject smokePuff = Instantiate ( smoke, transform.position, Quaternion.identity);
				ParticleSystem particleSystem = smokePuff.GetComponent<ParticleSystem> ();
				ParticleSystem.MainModule main = particleSystem.main;
				Color color = GetComponent<SpriteRenderer> ().color;
				main.startColor = color;
				particleSystem.Play ();

				if (bricksCount <= 0) {
					bricksCount = 0;
					levelManager.loadNextLevel ();
				}

				Destroy (gameObject);
			} else {
				
				AudioSource.PlayClipAtPoint (crackSound, transform.position);
				this.GetComponent<SpriteRenderer>().sprite = brickSprite[hits - 1];
			}
		}
	}
}
