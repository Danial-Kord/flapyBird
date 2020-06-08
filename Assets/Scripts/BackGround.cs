using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackGround : MonoBehaviour {
	// Use this for initialization
	private Vector3 fisrt;
	public Text score;
	private int lastScore;
	public  float speed = -0.02f;

	private float validDistance;
	[SerializeField] private GameObject[] backGrounds;
	public float lastPos = 7.08f;
	
	void Start () {// - Vector3.right*1.1f;
		fisrt = new Vector3(19.91f,11.51522f,0);
		validDistance = Vector3.Distance(backGrounds[0].transform.position, backGrounds[1].transform.position);
		lastScore = 0;
		
		score = GameObject.FindWithTag("txt").GetComponent<Text>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {


		if (!Bird.getGameOver())
		{
			
			int index = 0;
			bool check = false;
			for (int i = 0; i < backGrounds.Length; i++)
			{
				backGrounds[i].transform.Translate(speed*Time.deltaTime,0,0);
				if (backGrounds[i].transform.position.x <= lastPos)
				{
					check = true;
					index = i;
				}
			}

			if (check)
			{
				float max = backGrounds[0].transform.position.x;
				for (int j = 1; j < backGrounds.Length; j++)
				{
					if (backGrounds[j].transform.position.x >= max)
					{
						max = backGrounds[j].transform.position.x;
					}
				}

				Vector3 temp = backGrounds[index].transform.position;
				temp.x = max + validDistance;
				backGrounds[index].transform.position = temp;
			}
		}
		/*
		if(this.transform.position.x <= lastScore){
			this.transform.position = fisrt;
		}*/
	
		if (int.Parse (score.text) != lastScore) {
			foreach (var t in backGrounds)
			{
				  if (t.GetComponent<SpriteRenderer>().color.g - 0.005f > 0) {

					t.GetComponent<SpriteRenderer>().color = new Color (1, t.GetComponent<SpriteRenderer> ().color.g - 0.01f, t.GetComponent<SpriteRenderer>().color.b);
				  }
				  else 	if (t.GetComponent<SpriteRenderer>().color.b - 0.005f > 0) {
					  t.GetComponent<SpriteRenderer>().color = new Color (1, t.GetComponent<SpriteRenderer>().color.g, t.GetComponent<SpriteRenderer>().color.b - 0.01f);
				  }
				  {
					  
				  }
			}

			speed -= 0.03f;
			lastScore = int.Parse (score.text);
		}
	}
}
