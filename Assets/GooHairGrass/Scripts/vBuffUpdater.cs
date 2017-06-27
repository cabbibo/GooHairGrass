using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vBuffUpdater : MonoBehaviour {

	public ComputeShader computeShader;
	public vBuffer vertBuffer;

	public bool resetOnStart;
	public bool update;

	private int _kernel;

	public delegate void BeforeDispatch(ComputeShader shader, int kernel);
  public event BeforeDispatch OnBeforeDispatch;

  public delegate void AfterDispatch(ComputeShader shader, int kernel);
  public event AfterDispatch OnAfterDispatch;

  public delegate void WhenReady();
  public event WhenReady OnWhenReady;

  public int numGroups;
  public int numThreads;
  public int numParticles;
  public bool ready = false;

  public void Live(){
  	if( vertBuffer == null ){ vertBuffer = GetComponent<vBuffer>(); }
		vertBuffer.OnWhenReady += WhenBufferReady;

		if( vertBuffer.ready == true ){
			WhenBufferReady( vertBuffer._buffer );
		}
  }

  public void Die(){
		vertBuffer.OnWhenReady -= WhenBufferReady;
		ready = false;
  }


	// Use this for initialization
	void WhenBufferReady( ComputeBuffer b ) {

		_kernel = computeShader.FindKernel("CSMain");

		int numParticles =  vertBuffer.vertCount;
		int numThreads = 256;
		
		numGroups = (numParticles+(numThreads-1))/numThreads;

	  if(OnWhenReady != null) OnWhenReady();

	  ready = true;

	  if( resetOnStart == true ){ ResetShader(); }

	   	//computeShader.Dispatch( _kernel, vertBuffer.SIZE , vertBuffer.SIZE , vertBuffer.SIZE );
		
	}

	public void ResetShader(){
		computeShader.SetInt( "_Reset", 1 );
		DispatchComputeShader();
		computeShader.SetInt( "_Reset", 0 );
	}

	public void DispatchComputeShader(){


		computeShader.SetFloat( "_DeltaTime"    , Time.deltaTime );
    computeShader.SetFloat( "_Time"         , Time.time      );

		computeShader.SetVector( "_CenterPos", transform.position);


		if(OnBeforeDispatch != null) OnBeforeDispatch( computeShader , _kernel);


		computeShader.SetBuffer( _kernel , "vertBuffer"     , vertBuffer._buffer );

		computeShader.SetInt( "_NumVerts" , numParticles );
		computeShader.SetInt( "_NumGroups", numGroups );

	  computeShader.Dispatch( _kernel, numGroups , 1 , 1 );


	  if(OnAfterDispatch != null) OnAfterDispatch( computeShader , _kernel);

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if( update == true && ready == true ){ DispatchComputeShader(); }
		
	}
		
	
}
