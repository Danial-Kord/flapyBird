using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityNative.Sharing;


public class LevelManager : MonoSingleton<LevelManager> {
	
	//private  IUnityNativeSharing unityNativeSharing;
	public static int choosenIndex = 0;
	[SerializeField] private GameObject[] Birds;
	public void startPlay()
	{
		SceneManager.LoadScene ("main");
		Time.timeScale = 1;
	}

	protected override void OnAwake()
	{
		//unityNativeSharing = UnityNativeSharing.Create();
		if (SceneManager.GetActiveScene().name.Equals("main"))
		{
			Birds[choosenIndex].SetActive(true);
		}
	}
	/*
	protected override void initialization()
	{
		
	}*/

	public void pauseGame(GameObject pause)
	{
		Time.timeScale = 0;
		//Bird.gameOver = true;
		pause.SetActive(false);
	}
	
	public void spawnButtons(GameObject dad)
	{
		dad.SetActive(true);
	}

	public void disableButtons(GameObject dad)
	{
		dad.SetActive(false);
	}
	
	public void resume(GameObject pause)
	{
		Bird.gameOver = true;
		Time.timeScale = 1;
		pause.SetActive(true);
	}
	public void menu(){
		SceneManager.LoadScene ("UI");
	}

	public void shareDownloadLink()
	{
		//unityNativeSharing.ShareText("google.com",true,"Select App To Share With");
	}
	public void Exit(){
		Application.Quit();
	}
	/*
	public void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Exit ();
		}
	}
	*/
}
