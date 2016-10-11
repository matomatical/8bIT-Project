Shader "Tiled2Unity/Default"
{
    Properties
    {
        [PerRendererData] _MainTex ("Tiled Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
//        _Darker ("Darker", Color) = (0.75,0.75,0.75,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 1
//        [MaterialToggle] Darken ("Darken", Float) = 1
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

        Cull Off
        Lighting Off
        ZWrite Off
        Fog { Mode Off }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile DUMMY PIXELSNAP_ON
//            #pragma multi_compile DUMMY DARKEN_ON
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                half2 texcoord  : TEXCOORD0;
            };


            fixed4 _Color;
            fixed4 _Darker;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
                OUT.texcoord = IN.texcoord;
//                #ifdef DARKEN_ON
//                OUT.color = OUT.color * _Color * _Darker;
//                #else
                OUT.color = IN.color * _Color;
//                #endif
                #ifdef PIXELSNAP_ON
                OUT.vertex = UnityPixelSnap (OUT.vertex);
                #endif

                return OUT;
            }

            sampler2D _MainTex;

            fixed4 frag(v2f IN) : COLOR
            {
                half4 texcol = tex2D(_MainTex, IN.texcoord);
                texcol = texcol * IN.color;
                return texcol;
            }
        ENDCG
        }
    }

    Fallback "Sprites/Default"
}