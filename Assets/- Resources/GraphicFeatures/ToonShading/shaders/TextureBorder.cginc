#if !defined(BORDERTEXTUREMASK_INCLUDED)
#define BORDERTEXTUREMASK_INCLUDED

struct appdata_t
{
    float4 vertex   : POSITION;
    float2 texcoord : TEXCOORD0;
};

struct v2f
{
    float4 vertex   : SV_POSITION;				
    half2 texcoord  : TEXCOORD0;
};

v2f vert(appdata_t v)
{
    v2f OUT;
    OUT.vertex = UnityObjectToClipPos(v.vertex);
    OUT.texcoord.xy = v.texcoord;
    #ifdef PIXELSNAP_ON
    OUT.vertex = UnityPixelSnap (OUT.vertex);
    #endif
    return OUT;
}

sampler2D _MainTex;
half _Cutoff;
sampler2D _BorderTexture;
half4 _BorderColor;
half4 _Tint;

fixed4 frag(v2f IN) : SV_Target
{
    clip(tex2D(_MainTex, IN.texcoord.xy).a-_Cutoff);
    return tex2D(_BorderTexture, IN.texcoord.xy)*_BorderColor;
}
#endif