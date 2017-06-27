using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayVertBufferWithLines : MonoBehaviour {

	public Material m;
	public tBuffer  tBuf;
	public vBuffer  vBuf;

	public void Live(){
		if( tBuf == null ){ tBuf = GetComponent<tBuffer>(); }
		if( vBuf == null ){ vBuf = GetComponent<vBuffer>(); }
	}

	public void Die(){ tBuf =null; vBuf = null;}

	void OnRenderObject(){

		//print(tBuf.triCount);
		//print("ss");
		m.SetPass(0);

		m.SetBuffer( "_vertBuffer", vBuf._buffer );
		m.SetBuffer( "_triBuffer", tBuf._buffer );

		//Graphics.DrawProcedural(MeshTopology.Points, vBuf.vertCount );
		Graphics.DrawProcedural(MeshTopology.Lines, tBuf.triCount * 2 );


	}
}
