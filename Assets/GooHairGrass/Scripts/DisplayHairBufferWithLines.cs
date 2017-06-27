using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayHairBufferWithLines : MonoBehaviour {

	public Material m;
	public hBuffer  hBuf;


	public void Live(){
		if( hBuf == null ){ hBuf = GetComponent<hBuffer>(); }
	}

	public void Die(){ hBuf = null; }

	void OnRenderObject(){


		m.SetPass(0);

		m.SetInt( "_VertsPerHair" , hBuf.numVertsPerHair );
		m.SetBuffer( "_vertBuffer", hBuf._buffer );

		Graphics.DrawProcedural(MeshTopology.Lines, hBuf.totalHairs * (hBuf.numVertsPerHair) * 2 );


	}
}

