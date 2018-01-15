using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour {

	public AudioClip click;
	public AudioClip drop;
	private AudioSource audioSource;
	void Start(){
		audioSource=GetComponent<AudioSource>();
	}
	public void playClickAudio(){
		audioSource.PlayOneShot(click);
	}
	public void playDropAudio(){
		audioSource.PlayOneShot(drop);
	}
}
