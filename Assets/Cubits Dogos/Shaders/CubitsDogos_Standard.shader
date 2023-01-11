// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/CubitsDogos_Standard"
{
	Properties
	{
		_FurAlpha("FurAlpha", 2D) = "white" {}
		_FurColor1("FurColor1", Color) = (0.945098,0.827451,0.3882353,1)
		_FurColor2("FurColor2", Color) = (0.1333333,0.1333333,0.1333333,1)
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _FurAlpha;
		uniform float4 _FurAlpha_ST;
		uniform float4 _FurColor1;
		uniform float4 _FurColor2;
		uniform float _Smoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_FurAlpha = i.uv_texcoord * _FurAlpha_ST.xy + _FurAlpha_ST.zw;
			float layeredBlendVar4 = tex2D( _FurAlpha, uv_FurAlpha ).a;
			float4 layeredBlend4 = ( lerp( _FurColor1,_FurColor2 , layeredBlendVar4 ) );
			o.Albedo = layeredBlend4.rgb;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17000
0;0;1536;803;1252.339;359.4057;1.3;True;True
Node;AmplifyShaderEditor.ColorNode;3;-717.8984,-431.0523;Float;False;Property;_FurColor1;FurColor1;1;0;Create;True;0;0;False;0;0.945098,0.827451,0.3882353,1;1,0.6933816,0.2311321,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;2;-1061.546,-253.2739;Float;False;Property;_FurColor2;FurColor2;2;0;Create;True;0;0;False;0;0.1333333,0.1333333,0.1333333,1;0.2358491,0.2358491,0.2358491,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-1013.971,-17.67872;Float;True;Property;_FurAlpha;FurAlpha;0;0;Create;True;0;0;False;0;None;e88f874a83e1eb24eb46ae04b3cdee01;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LayeredBlendNode;4;-372.0809,-224.3142;Float;False;6;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-445.9596,140.0391;Float;False;Property;_Smoothness;Smoothness;3;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Custom/CubitsDogos_Standard;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;0;1;4
WireConnection;4;1;3;0
WireConnection;4;2;2;0
WireConnection;0;0;4;0
WireConnection;0;4;5;0
ASEEND*/
//CHKSM=74FD6288680EC51A2F3F8F13EDFD3F406908FE56