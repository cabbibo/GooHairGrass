using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranularSynth : MonoBehaviour {

	public int numSources;
	public float playbackTime;
	public float playbackSpeed;
	public float playbackVolume;
	public float playbackRandom;


	public AudioClip[] clips;
	private AudioSource[] sources;

	private float playTime;
	private int currentSource;

	// Use this for initialization
	void Start () {

		sources = new AudioSource[numSources];
		for( int i = 0; i < numSources; i++ ){

			sources[i] = gameObject.AddComponent<AudioSource>();

		}
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		playTime += Time.deltaTime;

		if( playTime >= playbackTime * Random.Range( 0.5f , 1f ) ){
			playTime = 0;
			currentSource ++;
			currentSource = currentSource % numSources;

			int clip = Random.Range( 0 , clips.Length -1 );
//			print( clip );

			sources[ currentSource ].clip = clips[clip];
			sources[ currentSource ].pitch = playbackSpeed;
			sources[ currentSource ].volume = playbackVolume;

			sources[ currentSource ].Play();
		}

		for( int i = 0; i < numSources; i++ ){
			sources[i].pitch = playbackSpeed;
			sources[i].volume = playbackVolume;
			//sources[i].volume = playbackVolume;

		}
		
	}
}
