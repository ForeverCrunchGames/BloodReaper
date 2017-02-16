Shader "Effects/Distortion/CullBack" {
Properties {
        _TintColor ("Tint Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Gloss (A)", 2D) = "black" {}
        _BumpMap ("Normalmap", 2D) = "bump" {}
		_ColorStrength ("Color Strength", Float) = 1
		_BumpAmt ("Distortion", Float) = 10
		_InvFade ("Soft Particles Factor", Range(0,10)) = 1.0
}

Category {

	Tags { "Queue"="Transparent"  "IgnoreProjector"="True"  "RenderType"="Opaque" }
	Blend SrcAlpha OneMinusSrcAlpha
	Cull Back 
	Lighting Off 
	ZWrite Off 
	Fog { Mode Off}

	SubShader {
		GrabPass {							
			Name "BASE"
			Tags { "LightMode" = "Always" }
 		}
		Pass {
			Name "BASE"
			Tags { "LightMode" = "Always" }
			
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#pragma multi_compile_particles
#include "UnityCG.cginc"

struct appdata_t {
	float4 vertex : POSITION;
	float2 texcoord: TEXCOORD0;
	fixed4 color : COLOR;
};

struct v2f {
	float4 vertex : POSITION;
	float4 uvgrab : TEXCOORD0;
	float2 uvbump : TEXCOORD1;
	float2 uvmain : TEXCOORD2;
	fixed4 color : COLOR;
	#ifdef SOFTPARTICLES_ON
		float4 projPos : TEXCOORD3;
	#endif
};

sampler2D _MainTex;
sampler2D _BumpMap;

float _BumpAmt;
float _ColorStrength;
sampler2D _GrabTexture;
float4 _GrabTexture_TexelSize;
fixed4 _TintColor;


float4 _BumpMap_ST;
float4 _MainTex_ST;

v2f vert (appdata_t v)
{
	v2f o;
	o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
	#ifdef SOFTPARTICLES_ON
		o.projPos = ComputeScreenPos (o.vertex);
		COMPUTE_EYEDEPTH(o.projPos.z);
	#endif
	o.color = v.color;
	#if UNITY_UV_STARTS_AT_TOP
	float scale = -1.0;
	#else
	float scale = 1.0;
	#endif
	o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;
	o.uvgrab.zw = o.vertex.zw;
	o.uvbump = TRANSFORM_TEX( v.texcoord, _BumpMap );
	o.uvmain = TRANSFORM_TEX( v.texcoord, _MainTex );
	
	return o;
}

sampler2D _CameraDepthTexture;
float _InvFade;

half4 frag( v2f i ) : COLOR
{
	#ifdef SOFTPARTICLES_ON
		if(_InvFade > 0.0001)	{
		float sceneZ = LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos))));
		float partZ = i.projPos.z;
		float fade = saturate (_InvFade * (sceneZ-partZ));
		i.color.a *= fade;
	}
	#endif

	half2 bump = UnpackNormal(tex2D( _BumpMap, i.uvbump )).rg;
	float2 offset = bump * _BumpAmt * _GrabTexture_TexelSize.xy;
	i.uvgrab.xy = offset * i.uvgrab.z + i.uvgrab.xy;
	
	half4 col = tex2Dproj( _GrabTexture, UNITY_PROJ_COORD(i.uvgrab));
	fixed4 tex = tex2D(_MainTex, i.uvmain) * i.color;
	fixed4 emission = col * i.color + tex * _ColorStrength * _TintColor;
    emission.a = _TintColor.a * i.color.a;
	return emission;
}
ENDCG
		}
	}

	FallBack "Effects/Distortion/Free/CullOff"

}

}

