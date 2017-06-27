using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hBuffDataToSynth : MonoBehaviour {

	private hBuff_DataOut data;
	private GranularSynth gs;

  public int id;

  public float volumeDivider;
  public float speedDivider;
  public float playbackDivider;
  public float speedStart;
  public float playbackStart;

  
	// Use this for initialization
	void OnEnable() {

		gs = GetComponent<GranularSynth>();
		
	}

	void OnDisable(){
		gs = null;
		data = null;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if( data == null ){
			data = GetComponent<hBuff_DataOut>();
		}else{

			if( id == 2 ){
//				print( (float)data.values[2]);
				///print( (float)data.values[3]);
			}
			
			gs.playbackVolume = Mathf.Clamp(((float)data.values[1]*(float)data.values[1]) / volumeDivider,0,1);
	 		gs.playbackSpeed = speedStart + (float)data.values[0] /speedDivider;
	 		gs.playbackTime = playbackStart / (1 + (float)data.values[2] / playbackDivider);
	 }
		
	}
}
