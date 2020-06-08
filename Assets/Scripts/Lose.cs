using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lose : MonoBehaviour {

	// Use this for initialization
	public SpriteRenderer silverCoin;
	public SpriteRenderer goldCoin;
	public SpriteRenderer bronzeCoin;
	public SpriteRenderer whiteCoin;
	public Button retry;
	public Button menu;
	public GameObject newLife;
	public Text text;
	int[] highScores = new int[5];
	public Text score;
	public Text bestScore;
	private Vector3 firtRetryLocation;
	private Vector3 firstMenuLocation;
	bool flagLose = false;
	
	void Start () {
		this.transform.Translate (Vector3.back);
		score.enabled = false;
		retry.enabled = false;
		menu.enabled = false;
		
		firstMenuLocation = menu.GetComponent<Transform> ().transform.position;
		firtRetryLocation = retry.GetComponent<Transform> ().transform.position;
		menu.GetComponent<Transform> ().transform.position = new Vector3 (-600, 25, 0);
		retry.GetComponent<Transform> ().transform.position = new Vector3 (-600, 25, 0);
		
		//newLife.SetActive(false);
		
		bestScore.enabled = false;
		if (PlayerPrefs.GetInt ("HighScore4") == 0) {
			for (int i = 0; i < 5; i++) {
				highScores [i] = 0;
				PlayerPrefs.SetInt ("HighScore" + i, highScores [i]);

			}
		} else {
			for (int i = 0; i < 5; i++) {
				highScores [i] = PlayerPrefs.GetInt ("HighScore" + i);
			}
		}
	}

	public void enableLifeChanseAction()
	{
	newLife.SetActive(true);	
	Invoke("checkClicking",4);
	//StartCoroutine(newLifeChanse());
	}

	private IEnumerator newLifeChanse()
	{
		float time = 3;
		
		while (time > 0)
		{
			//reSpawn.text = "" + time;
			yield return new WaitForSeconds(1);
			time--;
		}
	}

	private void checkClicking()
	{
		if (newLife != null)
		{
			Destroy(newLife);
			lost();
		}
	}
	
	public void disableLifeChanseAction()
	{
		Destroy(newLife);
		newLife = null;
	}
	
	public void lost(){
		if (!flagLose) {
			
			this.transform.Translate (Vector3.forward);
			flagLose = true;
			score.text = text.text;
			score.enabled = true;
			retry.enabled = true;
			menu.enabled = true;
			menu.GetComponent<Transform> ().transform.position = firstMenuLocation;
			retry.GetComponent<Transform> ().transform.position = firtRetryLocation;
			bestScore.enabled = true;
			
			if (int.Parse(score.text) >= 300) {
				bronzeCoin.sortingOrder = 9;
			}
			else if (int.Parse(score.text) >= 700) {
				silverCoin.sortingOrder = 9;
			}
			else if (int.Parse(score.text) >= 1200) {
				goldCoin.sortingOrder = 9;
			}
			if (int.Parse(score.text) < 300) {
				whiteCoin.sortingOrder = 9;
			}


			highScores [0] = int.Parse (score.text);
			for (int i = 0; i < 5; i++) {
				for (int j = i; j < 5; j++) {
					if (highScores [i] > highScores [j]) {
						int temp = highScores [i];
						highScores[i] = highScores[j];
						highScores[j]  = temp;
					}
				}
			}
			for (int i = 0; i < 5; i++)
				PlayerPrefs.SetInt ("HighScore" + i, highScores [i]);
			bestScore.text = highScores [4].ToString ();

		}
	}
}
