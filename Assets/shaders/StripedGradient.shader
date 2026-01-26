Shader "StripedGradient"
{
    Properties
    {
        _TopColor ("Top Color", Color) = (1,1,1,1)
        _MiddleColor ("Middle Color", Color) = (0.5,0.5,0.5,1)
        _BottomColor ("Bottom Color", Color) = (0,0,0,1)
        _StripeCount ("Stripe Count", Float) = 20
        _StripeWidth ("Stripe Width", Range(0,1)) = 0.5
        
        _TopStripeAlpha ("Top Stripe Alpha", Range(0,1)) = 0
        _MiddleStripeAlpha ("Middle Stripe Alpha", Range(0,1)) = 0
        _BottomStripeAlpha ("Bottom Stripe Alpha", Range(0,1)) = 0
        
        _FillTop ("Fill Top", Range(0,1)) = 0
        _FillMiddle ("Fill Middle", Range(0,1)) = 0
        _FillBottom ("Fill Bottom", Range(0,1)) = 0
        
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
    }
    
    SubShader
    {
        Tags 
        { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
            "PreviewType"="Plane"
        }
        
        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }
        
        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"
            
            struct appdata_t
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            
            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };
            
            float4 _ClipRect;
            
            float4 _TopColor;
            float4 _MiddleColor;
            float4 _BottomColor;
            float _StripeCount;
            float _StripeWidth;
            float _TopStripeAlpha;
            float _MiddleStripeAlpha;
            float _BottomStripeAlpha;
            float _FillTop;
            float _FillMiddle;
            float _FillBottom;
            
            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);
                OUT.texcoord = v.texcoord;
                OUT.color = v.color;
                return OUT;
            }
            
            fixed4 frag(v2f IN) : SV_Target
            {
                float verticalPos = IN.texcoord.y; // «· œ—Ã «·—√”Ì („‰  Õ  ·›Êﬁ)
                float4 color;
                float currentStripeAlpha;
                float currentFill;
                
                //  ﬁ”Ì„ ·‹ 3 √Ã“«¡ („‰  Õ  ·›Êﬁ)
                if (verticalPos < 0.333) // Bottom third
                {
                    float t = verticalPos / 0.333;
                    color = lerp(_BottomColor, _MiddleColor, t);
                    currentStripeAlpha = _BottomStripeAlpha;
                    currentFill = _FillBottom;
                }
                else if (verticalPos < 0.666) // Middle third
                {
                    float t = (verticalPos - 0.333) / 0.333;
                    color = lerp(_MiddleColor, _TopColor, t);
                    currentStripeAlpha = _MiddleStripeAlpha;
                    currentFill = _FillMiddle;
                }
                else // Top third
                {
                    float t = (verticalPos - 0.666) / 0.334;
                    color = lerp(_MiddleColor, _TopColor, t);
                    currentStripeAlpha = _TopStripeAlpha;
                    currentFill = _FillTop;
                }
                
                // «·ŒÿÊÿ «·—√”Ì… (⁄„ÊœÌ… - ⁄·Ï X axis)
                float stripe = frac(IN.texcoord.x * _StripeCount);
                float stripePattern = step(stripe, _StripeWidth);
                
                float stripeDarkness = lerp(1.0, 0.85, stripePattern);
                stripeDarkness = lerp(1.0, stripeDarkness, currentStripeAlpha * currentFill);
                
                color.rgb *= stripeDarkness;
                color *= IN.color;
                
                #ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif
                
                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif
                
                return color;
            }
            ENDCG
        }
    }
}