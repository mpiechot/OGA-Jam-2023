Shader "Custom/CircularWaveEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Center ("Center", Vector) = (0.5, 0.5, 0, 0)
        _WaveRadius ("Wave Radius", Range(0, 1)) = 0.5
        _WaveStrength ("Wave Strength", Range(0, 1)) = 0.5
        _EmissiveColor ("Emissive Color", Color) = (1, 1, 1, 1)
        _EmissiveStrength ("Emissive Strength", Range(0, 1)) = 0.1
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float4 _Center;
            float _WaveRadius;
            float _WaveStrength;
            float4 _EmissiveColor;
            float _EmissiveStrength;

            fixed4 frag (v2f i) : SV_Target
            {
                // Calculate the distance from the center
                float2 center = _Center.xy;
                float2 distance = i.uv - center;
                float dist = length(distance);

                // Calculate the distortion amount
                float distortion = smoothstep(_WaveRadius - _WaveStrength, _WaveRadius, dist);

                // Apply the distortion to the pixel coordinates
                float2 offset = distortion * distance;

                // Sample the texture with the distorted coordinates
                fixed4 col = tex2D(_MainTex, i.uv + offset);

                // If the pixel is outside the wave radius, display the original color
                if (dist > _WaveRadius)
                {
                    col = tex2D(_MainTex, i.uv);
                }
                else
                {
                    // Calculate the emissive color based on the distance from the center
                    float emissiveDist = dist / _WaveRadius;
                    float4 emissive = _EmissiveColor * emissiveDist * _EmissiveStrength;

                    // Add emissive effect to the inside of the wave radius
                    col.rgb += emissive.rgb;
                }

                return col;
            }
            ENDCG
        }
    }
}