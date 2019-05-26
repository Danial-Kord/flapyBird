using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Sprites;
public class Bird : MonoBehaviour {
	int score = 0;
	Rigidbody2D rb;
	Vector2 force;
	Text text;
	Touch touch;
	Sound sounds;
	[SerializeField] private GameObject goldBird1;
	[SerializeField] private GameObject goldBird2;
	[SerializeField] private GameObject goldBird3;
	private int coundWing;
	private bool clicked;
	private static bool gameOver =false;
	// Use this for initialization
	void Start () {
		clicked = false;
		coundWing = 0;
		sounds = GameObject.FindGameObjectWithTag ("Audio").GetComponent<Sound> ();
		force = new Vector2 (0, 200);
		rb = this.GetComponent<Rigidbody2D> ();
		text = GameObject.FindGameObjectWithTag ("txt").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyDown (KeyCode.Mouse0) && !gameOver) {
//			rb.velocity = Vector2.zero;
//			rb.AddForce (force);
//			clicked = true;
//			sounds.wing ();
//		}
		if (Input.touchCount > 0 && !gameOver) {
			if (Input.touches [0].phase.Equals (TouchPhase.Began)) {
				rb.velocity = Vector2.zero;
				rb.AddForce (force);
				clicked = true;
				sounds.wing ();
			}
		}
		if (clicked) {
			if (coundWing == 5) {
				goldBird1.GetComponent<Transform> ().transform.Translate(Vector3.forward);
				goldBird2.GetComponent<Transform> ().transform.Translate(Vector3.back);
			}
			if (coundWing == 10) {
				goldBird1.GetComponent<Transform> ().transform.Translate(Vector3.back);
				goldBird2.GetComponent<Transform> ().transform.Translate(Vector3.forward);
			}
			if (coundWing == 15) {
				goldBird2.GetComponent<Transform> ().transform.Translate(Vector3.back);
				goldBird3.GetComponent<Transform> ().transform.Translate(Vector3.forward);
			}
			if (coundWing == 20) {
				goldBird2.GetComponent<Transform> ().transform.Translate(Vector3.forward);
				goldBird3.GetComponent<Transform> ().transform.Translate(Vector3.back);
				clicked = false;
			}
			coundWing++;
			if (!clicked)
				coundWing = 0;
		}

	}
	public void addScore(){
		score++;
		text.text = score.ToString();
		sounds.point ();
	}
	public static void setGameOver(bool b){
		gameOver = b;
	}
	public static bool getGameOver(){
		return gameOver;
	}
	public void GameOver(){
		rb.angularVelocity = -50;
		sounds.die ();
//			rb.AddForce(new Vector2(100,100));

	}
}
