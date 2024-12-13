Shader "Custom/CurvedWorld_YAxis"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _CurveAmount ("Curve Amount", Float) = 0.01
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            // Properties
            sampler2D _MainTex;
            float _CurveAmount;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata_t v)
            {
                v2f o;

                // Get the world position
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                // Apply curvature along the Y-axis (parabola based on X position)
                worldPos.y += _CurveAmount * pow(worldPos.x, 2.0);

                // Transform to clip space
                o.vertex = mul(UNITY_MATRIX_VP, float4(worldPos, 1.0));
                o.uv = v.uv;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}