Shader "Custom/LineBufferRenderer" {

	Properties {

    }
  SubShader{

  	


    Cull off
    Pass{


      CGPROGRAM
      #pragma target 5.0

      #pragma vertex vert
      #pragma fragment frag

      #include "UnityCG.cginc"





struct Vert{
	 float used;
     float3 pos;
     float3 vel;
     float3 nor;
     float2 uv;
     float3 targetPos;
     float3 debug;
};

sampler2D _Audio;

      StructuredBuffer<Vert> _vertBuffer;
      StructuredBuffer<int> _triBuffer;

      //A simple input struct for our pixel shader step containing a position.
      struct varyings {
          float4 pos 			: SV_POSITION;
          float3 nor 			: TEXCOORD0;
          float2 uv  			: TEXCOORD1;
          float3 eye      : TEXCOORD5;
          float3 worldPos : TEXCOORD6;
          float3 debug    : TEXCOORD7;

      };


      varyings vert (uint id : SV_VertexID){

        varyings o;

        int id1; int id2;

        /*

        int tID = id / 6;

        int lID = id % 2;
        int inTid = tID / 2;

        


        if( inTid == 0 ){
        	id1 = tID + 0;
        	id2 = tID + 1;
        }else if( inTid ==1 ){
        	id1 = tID +1;
        	id2 = tID +2;
        }else{
        	id1 = tID +2;
        	id2 = tID + 0;
        }

        Vert v; 

        if( lID == 0 ){
        	v = _vertBuffer[_triBuffer[id1]];
        }else{
        	v = _vertBuffer[_triBuffer[id2]];

        		//float3 fPos = float3(0,0,0);//mul(float4(0,0,0,1),b.bindPose ).xyz;

       	//fPos = v.pos;
        }*/

        int idDiv = (id /2);
        int mVal = id % 2;

        int tID = idDiv % 3;

        id1 = idDiv;
        id2 = idDiv + 1;

        if( tID == 2 ){
        	id2 = idDiv -1;
        }


        //id1 = id / 2;
        //id2 = (id /2 ) +1;

        Vert v = _vertBuffer[ _triBuffer[id1]];
       	float3 fPos = _vertBuffer[ _triBuffer[id1]].pos;

       	//int mVal = id % 2;
        if( mVal == 1 ){
          v = _vertBuffer[ _triBuffer[id2]];
          fPos = _vertBuffer[ _triBuffer[id2]].pos;
        }

       //mul( b.transform, float4(fPos,1) ).xyz;


				o.pos = mul (UNITY_MATRIX_VP, float4(fPos,1.0f));
				o.worldPos = fPos;
				o.eye = _WorldSpaceCameraPos - o.worldPos;
	
				o.nor = float3(0,0,0);
				o.uv = float2(float( mVal),0);
        o.debug = normalize(v.debug) * .5 + .5;

        return o;


      }
      //Pixel function returns a solid color for each point.
      float4 frag (varyings v) : COLOR {
      	float3 col = float3(1,0,0);//v.debug;
        //col = tex2D(_Audio, v.uv  * .6).xyz *.3* float3( 2 , .6 , .3);
        return float4( col , 1. );


      }

      ENDCG

    }
  }

  Fallback Off
  
}