﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {
	// Use this for initialization

	bool flagLose =false;
	Sound sounds;
	void Start () {

		sounds = GameObject.FindGameObjectWithTag ("Audio").GetComponent<Sound> ();
	}
	
	void OnCollisionEnter2D(Collision2D col){
		if(!flagLose)
		if (col.gameObject.tag.Equals ("Bird")) {
			//disableColliders ();
			GameObject.FindGameObjectWithTag ("Bird").GetComponent<Bird>().GameOver ();
			
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
