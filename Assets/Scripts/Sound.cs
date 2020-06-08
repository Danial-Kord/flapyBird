using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class Sound : MonoBehaviour {
	[SerializeField] private AudioSource Point;
	[SerializeField] private AudioSource Wing;
	[SerializeField] private AudioSource Die;
	[SerializeField] private AudioSource Hit;

	// Use this for initialization
	public void point(){
		Point.Play ();
	}
	public void wing(){
		Wing.Play ();
	}
	public void die(){
		Die.Play ();
	}
	public void hit(){
		Hit.Play ();
	}
	// Update is called once per frame
	void Update () {
		
	}
}
