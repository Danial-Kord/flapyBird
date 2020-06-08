using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mile : MonoBehaviour {

	// Use this for initialization
	public GameObject mile1;
	private bool check = false;

	// Update is called once per frame
	void FixedUpdate () {
		if (Bird.getGameOver ())
			return;

		if (mile1.transform.position.x <= 8 && !check) {
			float x = Random.Range (-0.75f, 0.75f);

			while(!(transform.position.y+x <26 && transform.position.y+x>24)){
				x = Random.Range (-0.75f, 0.75f);
			}
			transform.position = new Vector3(transform.position.x,transform.position.y + x,0);
			check = true;
		}
		if(mile1.transform.position.x > 8)
		{
			check = false;
		}

	}
}
