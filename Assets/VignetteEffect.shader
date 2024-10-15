Shader "Custom/VignetteShaderInverted" {
    Properties {
        _Color ("Vignette Color", Color) = (0,0,0,1)
        _Radius ("Radius", Range(0.1, 1)) = 0.8
        _Smoothness ("Smoothness", Range(0.01, 1)) = 0.5
    }
    SubShader {
        Tags { "Queue"="Overlay" "RenderType"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _Color;
            float _Radius;
            float _Smoothness;

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                float2 center = float2(0.5, 0.5);
                float dist = distance(i.uv, center);
                float vignette = 1.0 - smoothstep(_Radius, _Radius - _Smoothness, dist);

                fixed4 color = _Color;
                color.a = vignette;

                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
