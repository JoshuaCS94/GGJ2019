Shader "Hidden/depth_lines"
{
	Properties
	{
	    _MainTex("MainTex",2D) = "white"{}
	    _MinDif("MinDif",Range(0,0.25)) = 0.1
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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;				
				return o;
			}
			
			sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _DepthMinDif;
            sampler2D _OutlineColorMask;
            sampler2D _OutlineDepthMask;
            float _MinDif;
            
            float _heightInPixel;
            
            fixed4 CrossDepthTest(sampler2D tex, float2 uv, float2 size){
                float linearDepth = tex2D(_OutlineDepthMask, uv).r;
                
                //up
                float rawDepth = tex2D(_OutlineDepthMask, uv + float2(0,size.y));                
                if(rawDepth > linearDepth && abs(rawDepth-linearDepth)>_MinDif)
                    return tex2D(_OutlineColorMask, uv + float2(0,size.y));
                    
                //down
                rawDepth = tex2D(_OutlineDepthMask, uv + float2(0,-size.y));
                if(rawDepth > linearDepth && abs(rawDepth-linearDepth)>_MinDif)
                    return tex2D(_OutlineColorMask, uv + float2(0,-size.y));
                    
                //left
                rawDepth = tex2D(_OutlineDepthMask, uv + float2(-size.x,0));
                if(rawDepth > linearDepth && abs(rawDepth-linearDepth)>_MinDif)
                    return tex2D(_OutlineColorMask, uv + float2(-size.x,0));
                
                //right
                rawDepth = tex2D(_OutlineDepthMask, uv + float2(size.x,0));
                if(rawDepth > linearDepth && abs(rawDepth-linearDepth)>_MinDif)
                    return tex2D(_OutlineColorMask, uv + float2(size.x,0));
                
                return tex2D(_MainTex, uv);
			}
            
			fixed4 frag (v2f i) : SV_Target
			{
                fixed4 col = CrossDepthTest(_MainTex,i.uv,_MainTex_TexelSize);

				return col;
			}
			ENDCG
		}
	}
}
