Shader "Color/VertexColor" {
	Properties {
	    _myColor ("Example Color", Color) = (1,1,1,1)
	}
	SubShader {
		CGPROGRAM

		#pragma surface surf Lambert
		
		fixed4 _myColor;
		
		struct Input{
		    float viewDir;
		    float3 worldPos;
		};

		void surf (Input IN, inout SurfaceOutput o) {
		    o.Albedo = _myColor.rgb;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
