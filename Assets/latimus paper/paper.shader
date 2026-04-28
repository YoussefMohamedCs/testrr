Shader "Custom/LiquidPaperShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PaperColor ("Paper Color", Color) = (1,1,1,1)
        _FillAmount ("Fill Amount", Range(0, 1)) = 0
        _BottomColor ("Bottom Color", Color) = (0.2, 0.5, 1.0, 1.0)
        _TopColor ("Top Color", Color) = (0.0, 1.0, 0.6, 1.0)
        _EdgeSoftness ("Edge Softness", Range(0.001, 0.1)) = 0.02
        _WaveSpeed ("Wave Speed", Float) = 2.0
        _WaveAmplitude ("Wave Amplitude", Range(0, 0.05)) = 0.01
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

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

            sampler2D _MainTex;
            float4 _PaperColor;
            float _FillAmount;
            float4 _BottomColor;
            float4 _TopColor;
            float _EdgeSoftness;
            float _WaveSpeed;
            float _WaveAmplitude;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // «··Ê‰ «·√”«”Ì ··Ê—ﬁ…
                fixed4 texColor = tex2D(_MainTex, i.uv) * _PaperColor;

                // „ÊÃ… ⁄‰œ Õ«›… «·„«Ì…
                float wave = sin(i.uv.x * 20.0 + _Time.y * _WaveSpeed) * _WaveAmplitude;
                float fillEdge = _FillAmount + wave;

                // «·Ã“¡ «·€«—ﬁ
                float inWater = 1.0 - smoothstep(fillEdge - _EdgeSoftness, fillEdge + _EdgeSoftness, i.uv.y);

                // Ã—«œÌ«‰  ·Ê‰ «·„«Ì…
                float gradientT = clamp(i.uv.y / max(_FillAmount, 0.001), 0, 1);
                fixed4 gradientColor = lerp(_BottomColor, _TopColor, gradientT);

                // «·„“Ã »Ì‰ ·Ê‰ «·Ê—ﬁ… Ê·Ê‰ «·„«Ì…
                fixed4 finalColor = lerp(texColor, gradientColor, inWater);
                finalColor.a = texColor.a;

                return finalColor;
            }
            ENDCG
        }
    }
}