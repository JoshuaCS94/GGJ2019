Shader "Hidden/DepthMask" {
    Properties {
		_MainTex ("Albedo", 2D) = "white" {}
		_Cutoff("Cutoff",Range(0,1)) = 0.5
		_MaskDepth("Depth", Range(0,1)) = 0.1
	}
	
	SubShader {
		Tags{"Border"="Solid"}
		Pass
		{
			Tags
			{
				"Queue"="AlphaTest"
                "IgnoreProjector"="True"
                "PreviewType"="Plane"
                "CanUseSpriteAtlas"="True"
			}

			Cull Off
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			#include "DepthMaskPass.cginc"
			
			ENDCG
		}
		Pass
		{
			Tags
			{
				"LightMode" = "ShadowCaster"
				"Queue"="AlphaTest"
				"IgnoreProjector"="True"
				"CanUseSpriteAtlas"="True"
			}

			ZWrite On
			ZTest LEqual
			Cull Off
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile_shadowcaster
			#include "UnityCG.cginc"
			
			#include "SpriteDepthWrite.cginc"
			
			ENDCG
		}
	}
	
	SubShader {
		Tags{"Border"="Texture"}
		Pass
		{
			Tags
			{
				"Queue"="AlphaTest"
                "IgnoreProjector"="True"
                "PreviewType"="Plane"
                "CanUseSpriteAtlas"="True"
			}

			Cull Off
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			#include "DepthMaskPass.cginc"
			
			ENDCG
		}
		Pass
		{
			Tags
			{
				"LightMode" = "ShadowCaster"
				"Queue"="AlphaTest"
				"IgnoreProjector"="True"
				"CanUseSpriteAtlas"="True"
			}

			ZWrite On
			ZTest LEqual
			Cull Off
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile_shadowcaster
			#include "UnityCG.cginc"
			
			#include "SpriteDepthWrite.cginc"
			
			ENDCG
		}
	}
}