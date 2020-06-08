using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour {

	public Text text;
	// Use this for initialization

	[SerializeField] private GameObject[] birds;
	private int index = 0;

	private void Start()
	{
		LevelManager.choosenIndex = index;
	}

	public void nextBird()
	{
		birds[index].GetComponent<SpriteRenderer>().enabled = false;
		index++;
		if (index == birds.Length)
			index = 0;
		birds[index].GetComponent<SpriteRenderer>().enabled = true;
		LevelManager.choosenIndex = index;

	}

	public void lastBird()
	{
		birds[index].GetComponent<SpriteRenderer>().enabled = false;
		index--;
		if (index == -1)
			index = birds.Length-1;
		birds[index].GetComponent<SpriteRenderer>().enabled = true;
		LevelManager.choosenIndex = index;
	}
	
}
