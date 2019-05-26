using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackGround : MonoBehaviour {
	// Use this for initialization
	private Vector3 fisrt;
	public Text score;
	private int lastScore;
	void Start () {// - Vector3.right*1.1f;
		fisrt = new Vector3(12.78f,11.51522f,0);
		lastScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(!Bird.getGameOver())
		this.transform.Translate (.01f,0,0);
		if(this.transform.position.x >= 21.32f){
			this.transform.position = fisrt;
		}
	
		if (int.Parse (score.text) != lastScore) {
			if (GetComponent<SpriteRenderer>().color.b - 0.005f > 0) {
				GetComponent<SpriteRenderer>().color = new Color (1, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b - 0.005f);
			} else if (GetComponent<SpriteRenderer>().color.g - 0.005f > 0) {

				GetComponent<SpriteRenderer>().color = new Color (1, gameObject.GetComponent<SpriteRenderer> ().color.g - 0.005f, GetComponent<SpriteRenderer>().color.b);

			}
			lastScore = int.Parse (score.text);
		}
	}
}
