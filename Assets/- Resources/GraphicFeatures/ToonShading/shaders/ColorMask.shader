Shader "Hidden/BorderColorMask" {
    Properties {
		_Tint ("Tint", Color) = (1, 1, 1, 1)
		_MainTex ("Albedo", 2D) = "white" {}
		_Cutoff("Cutoff",Range(0,1)) = 0.5
		_BorderColor("BorderColor", Color)=(1,1,1,1)
		_BorderTexture("Border Texture", 2D) = "white" {}
	}
	
	//COLOR
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
            //ZTest LEqual
            
            Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			#include "SolidColorBorder.cginc"
			
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
	//TEXTURE
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
            //ZTest LEqual
            
            Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			#include "TextureBorder.cginc"
			
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