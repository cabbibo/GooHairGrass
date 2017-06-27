using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Livify : MonoBehaviour {

	// Use this for initialization
	void Start () {

		vBuffer vBuf = GetComponent<vBuffer>() as vBuffer;
 
		tBuffer tBuf = GetComponent<tBuffer>() as tBuffer;
		hBuffer hBuf = GetComponent<hBuffer>() as hBuffer;

		vBuffUpdater baseUpdater = GetComponent<vBuffUpdater>() as vBuffUpdater; 		
		vBuff_Transform vbt = GetComponent<vBuff_Transform>() as vBuff_Transform;

		hBuffUpdater hairUpdater = GetComponent<hBuffUpdater>() as hBuffUpdater;
		hBuff_Transform hbt = GetComponent<hBuff_Transform>() as hBuff_Transform;

		DisplayHairBufferWithLines dhbwl = GetComponent<DisplayHairBufferWithLines>() as DisplayHairBufferWithLines;
		DisplayVertBufferWithLines dvbwl = GetComponent<DisplayVertBufferWithLines>() as DisplayVertBufferWithLines;
		
		tBuf.Live();
		vBuf.Live();
		hBuf.Live();

		baseUpdater.Live();
		hairUpdater.Live();

		vbt.Live();
		hbt.Live();

		dhbwl.Live();
		dvbwl.Live();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
