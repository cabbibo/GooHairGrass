using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hBuffer : cBuffer {

	public vBuffer vBuf;
	public tBuffer tBuf;

	public int numVertsPerHair;
	public float hairLength;
	public int totalHairs;

	public int vertCount { get { return  totalHairs * numVertsPerHair; }}


	public float distBetweenHairs { get { return hairLength / (float)numVertsPerHair; }}

	public float[] values;

	struct Vert{

		public Vector3 pos;
		public Vector3 oPos;
		public Vector3 vel;
		public Vector3 nor;
		public Vector2 uv;
		public Vector3 debug;

		public Vector3 triIDs;
		public Vector3 triWeights;

	};

	public int vertStructSize = 3+ 3+ 3 + 3 + 2 + 3 + 3 + 3;

	private int buffersLoaded;

	//public delegate void WhenReady(ComputeBuffer b);
 	//public event WhenReady OnWhenReady;


	public override void Live(){

		buffersLoaded = 0;

		if( vBuf == null ){ vBuf = GetComponent<vBuffer>(); }
		if( tBuf == null ){ tBuf = GetComponent<tBuffer>(); }

//		print("vBufff");
//		print( vBuf.ready );

		if( vBuf.ready == true ){
			WhenBufferReady( vBuf._buffer );
		}

		if( tBuf.ready == true ){
			WhenBufferReady( tBuf._buffer );
		}


		vBuf.OnWhenReady += WhenBufferReady; 
		tBuf.OnWhenReady += WhenBufferReady; 



	}

	public override void Die(){

		vBuf.OnWhenReady -= WhenBufferReady; 
		tBuf.OnWhenReady -= WhenBufferReady; 

		ReleaseBuffer();

	}

	void WhenBufferReady( ComputeBuffer b ){

//		print("bufferReady");
		buffersLoaded ++;
		if( buffersLoaded == 2 ){ SetUp(); }

	}

	void SetUp(){
		BothBuffersReady();
		ready = true;
		//if(OnWhenReady != null) OnWhenReady( _buffer );
	}


	void BothBuffersReady(){


		_buffer = new ComputeBuffer( vertCount , vertStructSize * sizeof(float));
		values = new float[ vertCount * vertStructSize ];

		int index = 0;

		for( int i = 0;  i< totalHairs; i++ ){

			float randomVal = getRandomFloatFromSeed(  i * 20 );
			
			int tri0 = (int)(randomVal * (float)(tBuf.values.Length/3)) * 3;
      int tri1 = tri0 + 1;
      int tri2 = tri0 + 2;

      tri0 = tBuf.values[tri0];
      tri1 = tBuf.values[tri1];
      tri2 = tBuf.values[tri2];

      Vector3 pos = GetRandomPointInTriangle( i , vBuf.vertices[ tri0 ] , vBuf.vertices[ tri1 ]  , vBuf.vertices[ tri2 ]  );
					
			float a0 = AreaOfTriangle( pos , vBuf.vertices[tri1] , vBuf.vertices[tri2] );
			float a1 = AreaOfTriangle( pos , vBuf.vertices[tri0] , vBuf.vertices[tri2] );
			float a2 = AreaOfTriangle( pos , vBuf.vertices[tri0] , vBuf.vertices[tri1] );
			float aTotal = a0 + a1 + a2;

			float p0 = a0 / aTotal;
			float p1 = a1 / aTotal;
			float p2 = a2 / aTotal;
			
			Vector3 nor     = vBuf.normals[tri0]  * p0 + vBuf.normals[tri1]  * p1 + vBuf.normals[tri2]  * p2;
			nor = nor.normalized;




			Vector3 fPos;

			for( int j = 0; j < numVertsPerHair; j++){

				int uvX =i;//(float)i / (float)totalHairs;
				int uvY =j;//(float)j / (float)numVertsPerHair;

				fPos = pos + nor * uvY * hairLength;

				 // pos
        values[index++] = fPos.x;
        values[index++] = fPos.y;
        values[index++] = fPos.z;

        // oPos
        values[index++] = fPos.x;
        values[index++] = fPos.y;
        values[index++] = fPos.z;

        //vel
        values[index++] = 0;
        values[index++] = 0;
        values[index++] = 0;

        // nor
        values[index++] = nor.x;
        values[index++] = nor.y;
        values[index++] = nor.z;

        // uv
        values[index++] = uvX;
        values[index++] = uvY;

        // debug
				values[index++] = 1;
        values[index++] = 1; // need to start w/ 1 for new collision stuff
        values[index++] = 1;


        // triIDs
				values[index++] = tri0;
        values[index++] = tri1;
        values[index++] = tri2;

        // triWeights
				values[index++] = p0;
        values[index++] = p1;
        values[index++] = p2;
         

			}
		}

		_buffer.SetData(values);



	}










	Vector3 GetRandomPointInTriangle( int seed, Vector3 v1 , Vector3 v2 , Vector3 v3 ){
    /* Triangle verts called a, b, c */

    Random.InitState(seed* 14145);
    float r1 = Random.value;

    Random.InitState(seed* 19247);
    float r2 = Random.value;
    //float r3 = Random.value;

    return (1 - Mathf.Sqrt(r1)) * v1 + (Mathf.Sqrt(r1) * (1 - r2)) * v2 + (Mathf.Sqrt(r1) * r2) * v3;
     
    ///return (r1 * v1 + r2 * v2 + r3 * v3) / (r1 + r2 + r3);
  }

  float AreaOfTriangle( Vector3 v1 , Vector3 v2 , Vector3 v3 ){
     Vector3 v = Vector3.Cross(v1-v2, v1-v3);
     float area = v.magnitude * 0.5f;
     return area;
  }


  Vector3 ToV3( Vector4 parent)
  {
     return new Vector3(parent.x, parent.y, parent.z);
  }

  float getRandomFloatFromSeed( int seed ){
  	Random.InitState(seed);
		return Random.value;
  }
}
