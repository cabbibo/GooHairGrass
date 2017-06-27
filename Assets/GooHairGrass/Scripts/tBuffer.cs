using UnityEngine;
using System.Collections;

public class tBuffer : cBuffer {

	public Mesh m;

	public int triCount;
	public int[] values;

	// Use this for initialization
	public override void SetUpBuffer() {

		if( m == null ){ m = GetComponent<MeshFilter>().mesh; }

		values =  m.triangles;
		triCount = m.triangles.Length;

		_buffer = new ComputeBuffer( triCount , sizeof(int) ); 
		_buffer.SetData(values);
	
	}

}
