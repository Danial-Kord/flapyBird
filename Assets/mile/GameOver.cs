using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {
	// Use this for initialization
	Bird bird;
	bool flagLose =false;
	Lose lose;
	Sound sounds;
	void Start () {
		bird = GameObject.FindGameObjectWithTag ("Bird").GetComponent<Bird>();
		lose = GameObject.FindGameObjectWithTag ("Lose").GetComponent<Lose>();
		sounds = GameObject.FindGameObjectWithTag ("Audio").GetComponent<Sound> ();
	}
	
	void OnCollisionEnter2D(Collision2D col){
		if(!flagLose)
		if (col.gameObject.tag.Equals ("Bird")) {
			Bird.setGameOver (true);
			disableColliders ();
			bird.GameOver ();
			lose.setActive (true);
			flagLose = true;
			sounds.hit ();
		}
	}
	private void disableColliders(){
		GameObject[] mileha = GameObject.FindGameObjectsWithTag ("MileUp");
		foreach (var item in mileha) {
			item.GetComponent<BoxCollider2D> ().enabled = false;
		}
		GameObject[] mileha2 = GameObject.FindGameObjectsWithTag ("Miledown");
		foreach (var item in mileha2) {
			item.GetComponent<BoxCollider2D> ().enabled = false;
		}
	}
}
