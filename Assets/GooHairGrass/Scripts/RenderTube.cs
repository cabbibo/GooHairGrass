using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTube : MonoBehaviour {

	public Material m;
	public hBuffer  hBuf;

	public int tubeLength;
	public int tubeWidth;


	public void Live(){
		if( hBuf == null ){ hBuf = GetComponent<hBuffer>(); }
	}

	public void Die(){}

	void OnRenderObject(){

		//print(tBuf.triCount);
		//print("ss");

		int totalVerts = hBuf.totalHairs * tubeWidth * (tubeLength-1) * 3 * 2;
		m.SetPass(0);


		
		m.SetInt( "_NumVertsPerHair" , hBuf.numVertsPerHair );
		m.SetInt( "_TubeLength" , tubeLength );
		m.SetInt( "_TubeWidth" , tubeWidth );
		m.SetInt( "_TotalVerts" , totalVerts );

		m.SetBuffer( "vertBuffer", hBuf._buffer );

		//print((hBuf.numVertsPerHair));

		

		Graphics.DrawProcedural( MeshTopology.Triangles , totalVerts );


	}
}