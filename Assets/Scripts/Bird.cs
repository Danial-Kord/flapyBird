using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TapsellSDK;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Sprites;
public class Bird : MonoBehaviour {
	int score = 0;
	Rigidbody2D rb;
	Vector2 force;
	Text text;
	
	
	
	Touch touch;
	Sound sounds;
	[SerializeField]private GameObject[] goldBirds;
	[SerializeField] private Text reSpawn;
	[SerializeField] private bool hasReversesSprite;
	
	private int coundWing;
	public bool clicked;
	public static bool gameOver = false;
	private Lose lose;

	private static int timePlays;

	private Coroutine reSpawinig = null;
	
	private static long lostNumbers = 0;
	public static bool isSetInitials = false;

	[SerializeField] public Vector3 firstPos;
	
	[SerializeField] private GameObject tap;
	[SerializeField] private GameObject tapIcon;

	private int index = 0;
	private int lastIndex = 0;
	private int count = 1;

	private float timePassed = 0;
	[Range(0,19)]
	[SerializeField] int  wingingSpeed = 15;

	
	// Use this for initialization
	void Start ()
	{

		wingingSpeed = 20 - wingingSpeed;
		clicked = false;
		coundWing = 0;
		gameOver = false;
		sounds = GameObject.FindGameObjectWithTag ("Audio").GetComponent<Sound> ();
		firstPos = transform.position;
		force = new Vector2 (0, 200);
		rb = this.GetComponent<Rigidbody2D>();
		lose = GameObject.FindGameObjectWithTag ("Lose").GetComponent<Lose>();
		text = GameObject.FindGameObjectWithTag ("txt").GetComponent<Text>();
		
		firstTimeInitilization();
		StartCoroutine(tapOnScreen());
		//resetLife();
	}

	private void firstTimeInitilization()
	{
		if(isSetInitials)
			return;
		isSetInitials = true;
		timePlays = PlayerPrefs.GetInt("timePlays")+1;
		PlayerPrefs.SetInt("timePlays",timePlays);
	}

	private IEnumerator tapOnScreen(Action onComplete = null)
	{

		Time.timeScale = 0;
		bool check = true;
		tap.SetActive(true);
		tapIcon.SetActive(true);
		float delta = 0.8f;
		while (check)
		{
			if (Input.touchCount > 0)
			{
				if (Input.touches[0].phase == TouchPhase.Began)
				{
					Time.timeScale = 1;
					rb.velocity = Vector2.zero;
					rb.AddForce (force);
					clicked = true;
					index = 0;
					count = 1;
					coundWing = 0;
					resetSprites();
					sounds.wing ();
					check = false;
					break;
				}
			}

			if (tap.transform.rotation.eulerAngles.x > 25 || tap.transform.rotation.eulerAngles.x < 0)
				delta = -delta;
			tap.transform.Rotate(delta,0,0);
			yield return null;
		}
		tapIcon.SetActive(false);
		tap.SetActive(false);
		
		onComplete?.Invoke();
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyDown (KeyCode.Mouse0) && !gameOver) {
//			rb.velocity = Vector2.zero;
//			rb.AddForce (force);
//			clicked = true;
//			sounds.wing ();
//		}
		if (Input.touchCount > 0 && !gameOver && Time.timeScale == 1) {
			if (Input.touches [0].phase.Equals (TouchPhase.Began)) {
				rb.velocity = Vector2.zero;
				rb.AddForce (force);
				clicked = true;
				index = 0;
				count = 1;
				timePassed = 0;
				coundWing = 0;
				resetSprites();
				sounds.wing ();
			}
		}
		if (clicked)
		{

			if (coundWing % wingingSpeed ==0)
			//if(timePassed >= (wingingSpeed / (float)goldBirds.Length))
			{

				if (index == goldBirds.Length - 1)
				{
					if (!hasReversesSprite)
					{
						count = 1;
						clicked = false;
						coundWing = 0;
						goldBirds[index].GetComponent<SpriteRenderer>().enabled = false;
						goldBirds[0].GetComponent<SpriteRenderer>().enabled = true;
						return;
					}
					else
					{
						count = -1;
					}
				}
				else if((index == 0 && count < 0))
				{
					count = 1;
					clicked = false;
					coundWing = 0;
					return;
				}


				lastIndex = index;
				index+=count;
				//Debug.Log("" + index + "......" +lastIndex);
				goldBirds[index].GetComponent<SpriteRenderer>().enabled = true;
				goldBirds[lastIndex].GetComponent<SpriteRenderer>().enabled = false;
				timePassed = 0;
			}
			coundWing++;
			//timePassed += Time.deltaTime;
			if (!clicked)
			{
			}
		}

	}

	private void resetSprites()
	{
		for (int i = 1; i < goldBirds.Length; i++)
		{
			goldBirds[i].GetComponent<SpriteRenderer>().enabled = false;
		}
		goldBirds[0].GetComponent<SpriteRenderer>().enabled = true;
	}
	private void resetLife()
	{
		transform.position = firstPos;
		transform.rotation = Quaternion.identity;
		GetComponent<Collider2D>().isTrigger = true;
		gameOver = false;
	 	reSpawinig = StartCoroutine(tapOnScreen(()=> { StartCoroutine(reSpawning()); }));
	}

	private IEnumerator reSpawning()
	{
		Time.timeScale = 1;

		
		
		//rb.angularVelocity = 0;
		//rb.isKinematic = true;

		
		GetComponent<Collider2D>().isTrigger = true;
		reSpawn.gameObject.SetActive(true);
		float time = 5.99f;

		float a = 0.89f;
		float delta = -0.015f;

		while ((int)time >= 0)
		{

			Color target = goldBirds[index].GetComponent<SpriteRenderer>().material.color;
			target.a = a;

			goldBirds[index].GetComponent<SpriteRenderer>().material.color = target;
			
			reSpawn.text = "" + (int)time;

			yield return null;
			a += delta;
			if (a >= 0.9f || a <= 0.15f)
				delta = -delta;
			
			time -= Time.deltaTime;
		}
		reSpawn.gameObject.SetActive(false);


		foreach (var t in goldBirds)
		{
			Color target = t.GetComponent<SpriteRenderer>().material.color;
			target.a = 1;
			t.GetComponent<SpriteRenderer>().material.color = target;
		}
		
		GetComponent<Collider2D>().isTrigger = false;


		if (transform.position.y < 6.5f)
		{
			GameOver();
		}
		else
		{
			gameOver = false;
		}
		
		
		reSpawinig = null;
	}
	
	
	
	public void addScore(){
		score++;
		text.text = score.ToString();
		sounds.point();
	}

	private void onFinishingAd(TapsellAdFinishedResult f = null)
	{
		Time.timeScale = 1;
		LevelManager.resetLevel();
	}
	private void playFastBanner()
	{
		AdManager.Instance.playFastBanner(onFinishingAd);
		Time.timeScale = 0;
	}
	

	private void onFinishingRewardVideo(TapsellAdFinishedResult completed)
	{
		if(reSpawinig != null)
			return;
		if (completed.completed)
		{
			resetLife();
		}
		else
		{
			lose.lost ();

		}	
	}
	
	public void playRewardVideo()
	{
		lose.disableLifeChanseAction();
		AdManager.Instance.playRewardVideoAd(onFinishingRewardVideo);
		Time.timeScale = 0;
	}
	
	
	/*
	
	private void OnAvailableAd(TapsellAd tapsellAd)
	{
		//Debug.Log("found Ad");
		GameObject.Find("debug").GetComponent<Text>().text = "founded!";
	}
	
	private void OnNotAvailableAd()
	{
		Debug.Log("not found Ad");
		Time.timeScale = 1;
		
	}
	*/
	public static bool getGameOver(){
		return gameOver;
	}
	public void GameOver(){
		if (!gameOver)
		{
			lostNumbers++;
			rb.angularVelocity = -50;
			sounds.die();
			if (lostNumbers % 4 == 0 && AdManager.Instance.tapselRewardVideoAvailable )
			{
				lose.enableLifeChanseAction();
			}
			else
			{
				lose.lost ();

			}
			gameOver = true;
		}
		else
		{
			Destroy(rb);
		}
//			rb.AddForce(new Vector2(100,100));
	
	}

	public void OnresetingGame()
	{
		Debug.Log(timePlays);
		if (timePlays > 2)
		{
			if (lostNumbers % 2 == 0)
			{
				if (AdManager.Instance.tapsellFastBannerAvailable)
				{
					playFastBanner();
					return;
				}
				else
				{
					lostNumbers--;
				}
			}
		}
		else if(lostNumbers % 3 == 0)
		{
			if (AdManager.Instance.tapsellFastBannerAvailable)
			{
				playFastBanner();
				return;
			}
			else
			{
				lostNumbers--;
			}
		}
		onFinishingAd();
	}

	
	/*
	private void OnEnable()
	{
		Debug.Log("enable");
		AdManager.onAdAvailableAction += OnAvailableAd;
		AdManager.onAnyErrorAdAction += OnNotAvailableAd;
	}

	private void OnDisable()
	{
		Debug.Log("disable");
		AdManager.onAdAvailableAction -= OnAvailableAd;
		AdManager.onAnyErrorAdAction -= OnNotAvailableAd;
	}
	*/
}
