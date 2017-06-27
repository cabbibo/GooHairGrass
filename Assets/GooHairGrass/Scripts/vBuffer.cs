using UnityEngine;
using System.Collections;

public class vBuffer : cBuffer {

	public Mesh m;

	public int vertCount;
	public float[] values;

	public Vector3[] vertices;
	public Vector3[] normals;
	public Vector2[] uvs;

	struct Vert{
		public float used;
	    public Vector3 pos;
	    public Vector3 vel;
	    public Vector3 nor;
	    public Vector2 uv;

	    public Vector3 targetPos;

	    public Vector3 debug;
	};

	private int vertStructSize = 1 + 3 + 3 + 3+2+3+3;


	// Use this for initialization
	public override void SetUpBuffer() {

		if( m == null ){ m = GetComponent<MeshFilter>().mesh; }

		//meshObject.GetComponent<SkinnedMeshRenderer>().BakeMesh( m );
		//Mesh m = meshObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
		vertices = m.vertices;
		uvs = m.uv;
		normals = m.normals;

		vertCount = vertices.Length;

//		print( "VERTCOUNT");
//		print( vertCount);

		_buffer = new ComputeBuffer( vertCount , vertStructSize * sizeof(float) );

		values = new float[ vertStructSize * vertCount ];

		int index = 0;
		for( int i = 0; i < vertCount; i++ ){

			// used 
			values[ index++ ] = 1;

			// positions
			values[ index++ ] = vertices[i].x;
			values[ index++ ] = vertices[i].y;
			values[ index++ ] = vertices[i].z;

			// vel
			values[ index++ ] = 0;
			values[ index++ ] = 0;
			values[ index++ ] = 0;

			// normals
			values[ index++ ] = normals[i].x;
			values[ index++ ] = normals[i].y;
			values[ index++ ] = normals[i].z;


			if( i < uvs.Length ){
				// uvs
				values[ index++ ] = uvs[i].x;
				values[ index++ ] = uvs[i].y;
			}else{
				values[ index++ ] = 0;
				values[ index++ ] = 0;
			}


			// target pos
			values[ index++ ] = vertices[i].x;
			values[ index++ ] = vertices[i].y;
			values[ index++ ] = vertices[i].z;


			// Debug
			values[ index++ ] = 0;
			values[ index++ ] = 0;
			values[ index++ ] = 0;

		} 
		
		_buffer.SetData(values);


	
	}

}
