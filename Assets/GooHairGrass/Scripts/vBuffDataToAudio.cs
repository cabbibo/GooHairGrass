using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vBuffDataToAudio : MonoBehaviour {
	
	public float multiplier = 1;
	public vBuff_DataOut data;
	public AudioSource audio;


	// Use this for initialization
	void Start () {

		if( data == null ){
			data = GetComponent<vBuff_DataOut>();
		}

		if( audio == null ){
			audio = GetComponent<AudioSource>();
		}
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	 	audio.volume = Mathf.Clamp(((float)data.values[0]) * multiplier / 3000,0,1);
	 	audio.pitch = 1+ (float)data.values[0] * multiplier / 1000;
		
	}
}
