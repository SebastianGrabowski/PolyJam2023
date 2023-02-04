Shader "Unlit/FadeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Value("Value", Float) = 0.0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma alpha
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Value;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float2 rotateUV(float2 uv, float2 pivot, float rotation) {
                float sine = sin(rotation);
                float cosine = cos(rotation);

                uv -= pivot;
                return float2(
                    cosine * uv.x - sine * uv.y,
                    cosine * uv.y + sine * uv.x
                ) + pivot;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float value2 = clamp(_Value / 1.0f, 0.0f, 1.0f);
                fixed4 col = tex2D(_MainTex, i.uv);
                if (col.r <= value2)
                {
                    fixed4 c2 = fixed4(1, 1, 1, 1);
                    fixed4 c1 = fixed4(0, 0, 0, 1);
                    return lerp(c1, c2, _Value);
                }
                return fixed4(1, 1, 1, _Value);
                col = fixed4(i.uv.x, 1.0f, 1.0f, 1.0f);
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
