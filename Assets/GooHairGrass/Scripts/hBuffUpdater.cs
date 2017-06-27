using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hBuffUpdater : MonoBehaviour {

	public hBuffer hairBuffer;
	
	public ComputeShader collisionShader;
	public ComputeShader constraintShader;

	public bool update;

	private int _kernelCollision;
	private int _kernelConstraint;

	public delegate void BeforeCollisionDispatch(ComputeShader shader, int kernel);
	public event BeforeCollisionDispatch OnBeforeCollisionDispatch;

	public delegate void AfterCollisionDispatch(ComputeShader shader, int kernel);
	public event AfterCollisionDispatch OnAfterCollisionDispatch;

	public delegate void BeforeConstraintDispatch(ComputeShader shader, int kernel);
	public event BeforeConstraintDispatch OnBeforeConstraintDispatch;

	public delegate void AfterConstraintDispatch(ComputeShader shader, int kernel);
	public event AfterConstraintDispatch OnAfterConstraintDispatch;

	public delegate void WhenReady();
	public event WhenReady OnWhenReady;

	public int numGroups;
	public int numThreads;
	public int numVerts;


	public bool ready = false;

	public void Live(){
//		print("yus");
		if( hairBuffer == null ){ hairBuffer = GetComponent<hBuffer>(); }
		hairBuffer.OnWhenReady += WhenBufferReady;

		//print( hairBuffer );
		//print( hairBuffer.ready );
		if( hairBuffer.ready == true ){
			WhenBufferReady( hairBuffer._buffer );
		}
	}

	public void Die(){
		hairBuffer.OnWhenReady -= WhenBufferReady;
		ready = false;
	}


	// Use this for initialization
	void WhenBufferReady( ComputeBuffer b ) {

//		print( "hello");

		_kernelCollision = collisionShader.FindKernel("CSMain");
    _kernelConstraint = constraintShader.FindKernel("CSMain");

		numVerts =  hairBuffer.vertCount;
		numThreads = 256;
		
		numGroups = (numVerts+(numThreads-1))/numThreads;
		//print( "NUMGROUPS");
		//print( numGroups );

		SetCorrectly();

   	if(OnWhenReady != null) OnWhenReady();

   	ready = true;


   	SetCorrectly();
		
	}

	public void SetCorrectly(){

		collisionShader.SetInt( "_Reset" , 1 );

		float dT = Time.deltaTime;
		if( dT > .1f){ dT = 0; }
		collisionShader.SetFloat( "_DeltaTime" , dT );
		
		collisionShader.SetFloat( "_Time" , Time.time );

	  collisionShader.SetInt( "_VertsPerHair" , hairBuffer.numVertsPerHair );

	  collisionShader.SetBuffer( _kernelCollision , "hairBuffer"     , hairBuffer._buffer );
	  collisionShader.SetBuffer( _kernelCollision , "baseBuffer"     , hairBuffer.vBuf._buffer );
	    

		collisionShader.SetInt( "_NumVerts" , hairBuffer.vertCount );
		collisionShader.SetInt( "_NumGroups", numGroups );

		collisionShader.Dispatch( _kernelCollision, numGroups,1,1 );

	}

	public void DispatchComputeShader(){


		if(OnBeforeCollisionDispatch != null) OnBeforeCollisionDispatch( collisionShader , _kernelCollision );

		
		collisionShader.SetInt( "_Reset" , 0 );

		float dT = Time.deltaTime;
		if( dT > .1f){ dT = 0; }
		collisionShader.SetFloat( "_DeltaTime" , dT );
		collisionShader.SetFloat( "_Time" , Time.time );

	  collisionShader.SetInt( "_VertsPerHair" , hairBuffer.numVertsPerHair );

	  collisionShader.SetBuffer( _kernelCollision , "hairBuffer"     , hairBuffer._buffer );
	  collisionShader.SetBuffer( _kernelCollision , "baseBuffer"     , hairBuffer.vBuf._buffer );
	    

		collisionShader.SetInt( "_NumVerts" , hairBuffer.vertCount );
		collisionShader.SetInt( "_NumGroups", numGroups );

		collisionShader.Dispatch( _kernelCollision, numGroups,1,1 );

		if(OnAfterCollisionDispatch != null) OnAfterCollisionDispatch( collisionShader , _kernelCollision );


		if(OnBeforeConstraintDispatch != null) OnBeforeConstraintDispatch( constraintShader , _kernelConstraint );

	  constraintShader.SetInt( "_PassID" , 0 );

	  //print( hairBuffer.distBetweenHairs );
	  constraintShader.SetFloat( "_SpringDistance" , hairBuffer.distBetweenHairs );
	  constraintShader.SetInt( "_VertsPerHair" , hairBuffer.numVertsPerHair );

	  constraintShader.SetInt( "_NumVerts" , hairBuffer.vertCount );
		constraintShader.SetInt( "_NumGroups", numGroups );

	  constraintShader.SetBuffer( _kernelConstraint , "vertBuffer"     ,	hairBuffer._buffer );

	  constraintShader.Dispatch( _kernelConstraint , (int)Mathf.Ceil((float)numGroups/2) , 1 , 1 );


	  constraintShader.SetInt( "_PassID" , 1 );

	  constraintShader.SetFloat( "_SpringDistance" , hairBuffer.distBetweenHairs );
	  constraintShader.SetInt( "_VertsPerHair" , hairBuffer.numVertsPerHair );

	  constraintShader.SetInt( "_NumVerts" , hairBuffer.vertCount );
		constraintShader.SetInt( "_NumGroups", numGroups );

	  constraintShader.SetBuffer( _kernelConstraint , "vertBuffer"     ,	hairBuffer._buffer );
	  //constraintShader.SetBuffer( _kernelConstraint , "baseBuffer"     , buffer1._buffer);
	  constraintShader.Dispatch( _kernelConstraint , (int)Mathf.Ceil((float)numGroups/2) , 1 , 1 );

	 
		if(OnAfterConstraintDispatch != null) OnAfterConstraintDispatch( constraintShader , _kernelConstraint );
	  	

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//print("ya");

		//print("Ss");

		if( update == true && ready == true ){ DispatchComputeShader(); }
		
	}
		
	
}
