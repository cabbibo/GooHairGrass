using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class computeCollisionAudio : MonoBehaviour {

	public int numSources;

	public AudioSource[] sources;
	public AudioClip clip;

	public vBuff_DataOut data;
	private int currentSource = 0;



	// Use this for initialization
	void Start () {

		if( data == null ){
			data = GetComponent<vBuff_DataOut>();
		}

		
		sources = new AudioSource[ numSources ];
		
		for( int i = 0; i < numSources; i++ ){
			sources[i] = gameObject.AddComponent<AudioSource>() as AudioSource;
			sources[i].clip = clip;
		}


	}
	
	// Update is called once per frame
	void FixedUpdate () {

		int numCollisions = (int)data.values[1];

//		print( numCollisions );

		for( int i = 0; i < numCollisions; i++ ){


			float rand = Random.Range(0.1f , .6f );

			//if( rand > .9f ){

				currentSource ++;
				currentSource = currentSource % (numSources-1);
				sources[currentSource].Play();
				sources[currentSource].pitch = rand  * rand;
				sources[currentSource].volume = (1-rand) * .1f;//rand  * rand;
			//}


		}
		
	}
}
