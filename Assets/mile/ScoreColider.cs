using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScoreColider : MonoBehaviour {
	Bird bird;

	// Use this for initialization
	void Start () {
		bird = GameObject.FindGameObjectWithTag ("Bird").GetComponent<Bird>();

//		GameObject[] gms = GameObject.FindGameObjectsWithTag; when there are more than one gameobjects with this tag
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag.Equals ("Bird")) {
			bird.addScore ();
		}
	}
}
