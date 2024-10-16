Shader "Custom/VignetteShaderInverted" 
{
    Properties 
    {
        _Color ("Vignette Color", Color) = (0,0,0,1)
        _Radius ("Radius", Range(0.1, 1)) = 0.8
        _Smoothness ("Smoothness", Range(0.01, 1)) = 0.5
    }

    SubShader 
    {
        Tags { "Queue"="Overlay" "RenderType"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha

        Pass 
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma multi_compile UNITY_STEREO_MULTIVIEW_ON UNITY_STEREO_MULTIVIEW_OFF

            #include "UnityCG.cginc"

            struct appdata 
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f 
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            float4 _Color;
            float _Radius;
            float _Smoothness;

            v2f vert (appdata v) 
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_OUTPUT(v2f, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target 
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

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
