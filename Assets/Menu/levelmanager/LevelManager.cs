using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour {
		
	public void start(){
		SceneManager.LoadScene ("S1");
		Bird.setGameOver (false);
	}
	public void menu(){
		SceneManager.LoadScene ("UI");
	}
	public void Exit(){
		Application.CancelQuit ();
	}
	public void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Exit ();
		}
	}
}
