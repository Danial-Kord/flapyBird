using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MovableMile : MonoBehaviour {
	public Text score;
	public float hardlyStage = 0.1f;
	private int lastScore=0;
	private bool animationUp, animationDown;
	private int lastAnimation = 0;

	private bool check = false;
	// Use this for initialization
	void Start () {
		animationUp = false;
		animationDown = false;
		score = GameObject.FindWithTag("txt").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (Bird.getGameOver ()) {
			return;
		}
		/*
		if (transform.position.x <= 10.25f) {
			transform.position = new Vector3 (18.9f, transform.position.y);
		}
		*/
		if (transform.position.x > 8)
			check = false;
		if ( hardlyStage <= 2 && transform.position.x <=8 && !check) {
			hardlyStage += 0.1f;
			lastScore = int.Parse (score.text);
			float x = Random.Range (0, hardlyStage);
			if (transform.position.y+x <=8 && this.tag.Equals("Miledown")) {
				transform.Translate (0, x , 0);
			} 
			if (transform.position.y-x >=15.5f && this.tag.Equals("MileUp")) {
				transform.Translate (0, -x, 0);
			}

			check = true;
		}

//		if (int.Parse (score.text) >0 && int.Parse (score.text) !=lastAnimation && transform.position.x >=15.5f) {
//			int x = Random.Range (1, 3);
//			lastAnimation = int.Parse (score.text);
//			if(x==1){
//			if(this.tag == "Miledown")
//			animationUp = true;
//			if(this.tag == "MileUp")
//			animationDown = true;			
//			}
//		}
//		print (transform.position.x+"<<<<<<<<<");
//		if(tag.Equals("MileUp")){
//			if(animationDown){
//				if (transform.position.y <= 16.2f  - hardlyStage) {
//					animationDown = false;
//					animationUp = true;
//				} else {
//					transform.Translate(Vector3.down*0.01f);
//				}
//			}
//			else if (animationUp) {
//				
//				if (transform.position.y >= 13.6f  + hardlyStage) {
//					animationUp = false;
//				} else {
//					transform.Translate (Vector3.up * 0.01f);
//				}
//			}
//		}
//		else if(tag.Equals("Miledown")){
//			if (animationUp) {
//				
//				if (transform.position.y >= 6.4f + hardlyStage) {
//					animationUp = false;
//				} else {
//					transform.Translate (Vector3.up * 0.01f);
//				}
//			}
//			else if(animationDown){
//
//				if (transform.position.y <= 9.4f  - hardlyStage) {
//					animationDown = false;
//					animationUp = true;
//				} else {
//					transform.Translate(Vector3.down*0.01f);
//				}
//			}
//		}
	}
}
