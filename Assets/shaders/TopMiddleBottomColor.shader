Shader "Custom/TopMiddleBottomLocal"
{
    Properties
    {
        _TopColor("Top Color", Color) = (1,1,1,1)
        _MiddleColor("Middle Color", Color) = (0,1,0,1)
        _BottomColor("Bottom Color", Color) = (0,0,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float localY : TEXCOORD0;
            };

            float4 _TopColor;
            float4 _MiddleColor;
            float4 _BottomColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.localY = v.vertex.y; // Use local Y instead of world Y
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                // Find normalized Y position in object (0 = bottom, 1 = top)
                float minY = 0.0; // bottom in local space
                float maxY = 1.0; // top in local space
                float y = saturate((i.localY - minY) / (maxY - minY));

                // Blend three colors
                float4 color;
                if (y < 0.5)
                {
                    // Bottom → Middle
                    color = lerp(_BottomColor, _MiddleColor, y * 2);
                }
                else
                {
                    // Middle → Top
                    color = lerp(_MiddleColor, _TopColor, (y - 0.5) * 2);
                }

                return color;
            }
            ENDCG
        }
    }
}
