using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vBuff_DataOut : MonoBehaviour {


	public float[] floatValues;
	public float[] values;

	public ComputeBuffer _gatherBuffer;
	public ComputeBuffer _floatBuffer;

	
	public vBuffUpdater updater;
	public ComputeShader gatherShader;

	private int _gatherKernel;
	private bool ready = false;


	// Use this for initialization
	public void Live() {

		if( updater == null){
			updater = GetComponent<vBuffUpdater>();
		}

		if( updater.ready == false ){
			updater.OnWhenReady += WhenReady;
		}else{
		 	WhenReady();
		}
	}


	void WhenReady(){

		print("WHEN READY");

		_gatherKernel = gatherShader.FindKernel("CSMain");

		
		

		updater.OnBeforeDispatch += addBuffer;
		updater.OnAfterDispatch += readBuffer;

		floatValues = new float[4*updater.numGroups];
		values = new float[4];

		_floatBuffer = new ComputeBuffer(updater.numGroups, 4 * sizeof(float));
		_gatherBuffer = new ComputeBuffer(1, 4 * sizeof(float));


		ready = true;

	}

	public void Die(){
		updater.OnBeforeDispatch -= addBuffer;
		updater.OnAfterDispatch -= readBuffer;
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

			//print( values[0] );
			//print( values[1] );
			
		}

	}
	
	
}
