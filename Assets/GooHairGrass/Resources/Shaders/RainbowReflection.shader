Shader "Custom/RainbowReflection" {
 Properties {
        _CubeMap( "Cube Map" , Cube ) = "white" {}
        _TexMap( "Tex Map" , 2D ) = "white" {}
        	_HueStart( "_HueStart", Float ) = 0
        _HueSize( "_HueSize", Float ) = 1
        _Saturation( "_Saturation", Float ) = 1
    }
  SubShader{

  	


    Cull off
    Pass{


      CGPROGRAM
      #pragma target 5.0

      #pragma vertex vert
      #pragma fragment frag

      #include "UnityCG.cginc"
      #include "Chunks/hsv.cginc"
uniform samplerCUBE _CubeMap;
uniform sampler2D _TexMap;

uniform float _HueSize;
uniform float _HueStart;
uniform float _Saturation;




struct Vert{
   float used;
     float3 pos;
     float3 vel;
     float3 nor;
     float2 uv;
     float3 targetPos;
     float3 debug;
};



      StructuredBuffer<Vert> _vertBuffer;
      StructuredBuffer<int> _triBuffer;

      //A simple input struct for our pixel shader step containing a position.
      struct varyings {
          float4 pos 	  : SV_POSITION;
          float3 nor 	  : TEXCOORD0;
          float2 uv  	  : TEXCOORD1;
          float3 eye      : TEXCOORD5;
          float3 worldPos : TEXCOORD6;
          float3 debug    : TEXCOORD7;

      };


      varyings vert (uint id : SV_VertexID){


      	int floorID = id / 3;
      	int remain = id % 3;
      	int remain1 = (id+1)%3;
      	int remain2 = (id+2)%3;


        Vert v = _vertBuffer[ _triBuffer[floorID * 3 + remain] ];
        Vert v1 = _vertBuffer[ _triBuffer[floorID * 3 + remain1] ];
        Vert v2 = _vertBuffer[ _triBuffer[floorID * 3 + remain2] ];

        float3 dif = v.pos - v.targetPos;
        varyings o;

        o.nor = normalize(cross((v1.pos-v.pos) , (v2.pos - v.pos)));

    float3 fPos = v.pos;// + v.nor * length( v.debug );
		o.pos = mul (UNITY_MATRIX_VP, float4(fPos,1.0f));
		o.worldPos = fPos;
		o.eye = _WorldSpaceCameraPos - o.worldPos;
	
		//o.nor = v.nor;
		o.uv = v.uv;
        o.debug = v.debug;//normalize(v.debug) * .5 + .5;

        return o;


      }
      //Pixel function returns a solid color for each point.
      float4 frag (varyings v) : COLOR {
      	
      	//float3 col = v.debug;
      	float3 col = v.nor * .5 + .5;// v.debug;

        float3 eye = _WorldSpaceCameraPos - v.worldPos;

        float3 eyeRefl = reflect( -eye , v.nor );


        float3 cubeCol = texCUBE( _CubeMap , normalize(eyeRefl) ).xyz;
        float3 tex = tex2D( _TexMap , v.uv ).xyz;

        col = hsv( dot( normalize(eye) , v.nor ) * _HueSize + _HueStart, _Saturation, 1) *  cubeCol;
        col *= 1.5;
        col *= tex;

        //col = float3( v.uv.x , v.uv.y , 1 );
        return float4( col , 1. );


      }

      ENDCG

    }
  }

  Fallback Off
  
}