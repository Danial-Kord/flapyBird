using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mile : MonoBehaviour {

	// Use this for initialization
	public GameObject mile1;
	private float speed;
	public Text score;
	private int lastScore=0;
	void Start () {
		speed = -.01f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Bird.getGameOver ())
			return;
		if(speed >= -1)
		if (int.Parse (score.text) != lastScore) {
			speed -= 0.001f;
			lastScore = int.Parse (score.text);
		}
		transform.Translate (speed, 0, 0);
		if (mile1.transform.position.x <=10.38) {
			float x = Random.Range (-0.75f, 0.75f);

			while(!(transform.position.y+x <26 && transform.position.y+x>24)){
				x = Random.Range (-0.75f, 0.75f);
			}
			transform.position = new Vector3(transform.position.x,transform.position.y + x,0);
		}

	}
}
