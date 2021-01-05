Shader "Custom/Sepiatone"
{
	Properties
	{
		_MainTex("Main Texture", 2D) = "white" {}
	}
		SubShader
		{
			// No culling or depth
			Cull Off ZWrite Off ZTest Always

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				sampler2D _MainTex;

				fixed4 frag(v2f IN) : SV_Target
				{

				fixed4 col = tex2D(_MainTex, IN.uv);

				float r = col[0] * 0.3922 + col[1] * 0.7686 + col[2] * 0.1882;
				float g = col[0] * 0.3490 + col[1] * 0.6863 + col[2] * 0.1686;
				float b = col[0] * 0.2706 + col[1] * 0.5333 + col[2] * 0.1294;

				fixed4 sepiaCol = fixed4(r, g, b, 1);

				return sepiaCol;

				}
			ENDCG
		}
		}
}