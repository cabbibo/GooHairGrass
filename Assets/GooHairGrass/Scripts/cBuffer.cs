using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cBuffer : MonoBehaviour {

	public ComputeBuffer _buffer;

	public bool ready = false;

	public delegate void WhenReady(ComputeBuffer b);
  public event WhenReady OnWhenReady;


	// Use this for initialization
	public virtual void Live() {

		SetUpBuffer();
		ready = true;
		if(OnWhenReady != null){
			OnWhenReady( _buffer );
		};
		
	}

	public virtual void Die(){
		ReleaseBuffer();
		ready = false;
	}

	public virtual void SetUpBuffer(){}

	public virtual void ReleaseBuffer(){
  	if(_buffer != null ){ _buffer.Release(); }
	}
	
}
