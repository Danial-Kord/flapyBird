using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Mile : MonoBehaviour {

	// Use this for initialization
	public GameObject mile1;
	private bool check = false;


	private void Start()
	{
		float x = Random.Range (6.2f,6.5f);

		transform.localPosition = new Vector3(transform.localPosition.x,x,0);
	}

	// Update is called once per frame
	void Update () {
		if (Bird.getGameOver () || Time.deltaTime == 0)
			return;

		if (transform.position.x <= 8 && !check) {
			float x = Random.Range (5.8f,7f);

			transform.localPosition = new Vector3(transform.localPosition.x,x,0);
			check = true;
		}
		if(mile1.transform.position.x > 8)
		{
			check = false;
		}

	}
}
