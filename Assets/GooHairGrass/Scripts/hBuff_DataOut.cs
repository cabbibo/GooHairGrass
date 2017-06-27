using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hBuff_DataOut : MonoBehaviour {


	public float[] floatValues;
	public float[] values;

	public ComputeBuffer _gatherBuffer;
	public ComputeBuffer _floatBuffer;

	
	public hBuffUpdater updater;
	public ComputeShader gatherShader;

	private int _gatherKernel;
	private bool ready = false;


	// Use this for initialization
	public void Live() {

		if( updater == null){
			updater = GetComponent<hBuffUpdater>();
		}

		if( updater.ready == false ){
			updater.OnWhenReady += WhenReady;
		}else{
		 	WhenReady();
		}
	}


	void WhenReady(){

		//print("WHEN READY");

		_gatherKernel = gatherShader.FindKernel("CSMain");

		
		

		updater.OnBeforeCollisionDispatch += addBuffer;
		updater.OnAfterCollisionDispatch += readBuffer;

		floatValues = new float[4*updater.numGroups];
		values = new float[4];

		_floatBuffer = new ComputeBuffer(updater.numGroups, 4 * sizeof(float));
		_gatherBuffer = new ComputeBuffer(4, 4 * sizeof(float));


		ready = true;

	}

	public void Die(){
		updater.OnBeforeCollisionDispatch -= addBuffer;
		updater.OnAfterCollisionDispatch -= readBuffer;
	}


	void addBuffer(ComputeShader computeShader , int _kernel){

		if( ready == true){
			computeShader.SetBuffer( _kernel , "outBuffer" , _floatBuffer );
		}

	}

	void readBuffer(ComputeShader computeShader , int _kernel){
		
		if( ready == true){

			
			gatherShader.SetBuffer( _gatherKernel , "floatBuffer" , _floatBuffer );
			gatherShader.SetBuffer( _gatherKernel , "gatherBuffer" , _gatherBuffer );
	
			gatherShader.Dispatch( _gatherKernel, 1 , 1 , 1 );
	
			_gatherBuffer.GetData(values);
			
		}

	}
	
	
}
