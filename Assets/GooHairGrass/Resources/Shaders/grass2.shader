Shader "Custom/grass2" {
	
Properties {
        _CubeMap( "Cube Map" , Cube ) = "white" {}
}
SubShader{
//        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
	Cull off
	Pass{
	
	  Blend SrcAlpha OneMinusSrcAlpha // Alpha blending
	
	  CGPROGRAM
	  #pragma target 5.0
	
	  #pragma vertex vert
	  #pragma fragment frag
	
    #include "UnityCG.cginc"
	  #include "Chunks/hsv.cginc"
	
	  uniform samplerCUBE _CubeMap;

	  uniform sampler2D _Audio;


 
		struct Vert{
		
		  float3 pos;
		  float3 oPos;
		  float3 vel;
		  float3 nor;
		  float2 uv;
		  float3 debug;
		
		  float3 triIDs;
		  float3 triWeights;
		
		};


            

    StructuredBuffer<Vert> _vertBuffer;

    //uniform float4x4 worldMat;

    uniform int _VertsPerHair;
    uniform int _TotalHairs;
    uniform int _TotalVerts;
 		uniform float3 _Color;


            
    //A simple input struct for our pixel shader step containing a position.
    struct varyings {
      float4 pos      : SV_POSITION;
      float3 worldPos : TEXCOORD1;
      float3 nor      : TEXCOORD0;
      float3 eye      : TEXCOORD2;
      float3 debug    : TEXCOORD3;
      float2 uv       : TEXCOORD4;
    };

    
           

    //Our vertex function simply fetches a point from the buffer corresponding to the vertex index
    //which we transform with the view-projection matrix before passing to the pixel program.
    varyings vert (uint id : SV_VertexID){

      varyings o;
      /*int segID =floor( float(id) / 2);
      int offset = id % 2;
      int idInHair = segID % (_VertsPerHair-1);
      int hairID = int( floor( float(segID) / float(_VertsPerHair) ));*/
      	
      int halfID = int( floor( float(id) / 2) );
      int hairID = halfID / (_VertsPerHair);//int(floor(float(halfID)/float(_VertsPerHair)));
      
      int idInHair = halfID % (_VertsPerHair-1); 
      int offsetID = id % 2;
      
      int fID = offsetID + idInHair +(hairID * ((_VertsPerHair )));
      
      Vert v = _vertBuffer[fID];
      
      float3 nor = float3( 1,0,0 );
      
      if( offsetID + hairID == 0 ){
      	nor = mul( unity_ObjectToWorld , float4(v.nor,0)).xyz;
      	nor = normalize( nor );
      }else{
      	Vert vDown = _vertBuffer[fID-1];
      	nor = -normalize( v.pos - vDown.pos );
      }
      
      float3 dif =   - v.pos;
      
      o.worldPos = v.pos;// + float3( 0, float( idInHair)+ offsetID, 0 );//mul( worldMat , float4( v.pos , 1.) ).xyz;
      
      o.eye = _WorldSpaceCameraPos - o.worldPos;
      
      o.pos = mul (UNITY_MATRIX_VP, float4(o.worldPos,1.0f));
      
      o.debug = v.debug;// v.debug;//normalize(v.pos-v.vel) * .5+.5;//n * .5 + .5;
      
      o.uv = float2(float( idInHair) / ( 3 *float(_VertsPerHair)) , 0);
      o.nor = nor;
  
      return o;

    }
 
    //Pixel function returns a solid color for each point.
    float4 frag (varyings v) : COLOR {


      float3 eye = _WorldSpaceCameraPos - v.worldPos;

      float3 eyeRefl = reflect( -eye , v.nor );
			
      float3 cubeCol = texCUBE( _CubeMap , eyeRefl ).xyz;
      float3 audio  = tex2D(_Audio, float2( v.uv)).xyz;


      float3 col = cubeCol  * hsv( v.uv.x * .3 + v.uv.y * 1.3 + .6 , 1, 1);// float3(1,0,0);//v.debug * v.uv.x;//v.nor * .5 + .5;
      return float4( col , 1 );

    }
 
    ENDCG
 
    }
  }
 
  Fallback Off

}
	