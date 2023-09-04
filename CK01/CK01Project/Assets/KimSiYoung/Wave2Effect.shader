Shader "Custom/WaveEffect2" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _BumpMap("Normal Map", 2D) = "bump" {}
        _DistortionSpeed("Distortion Speed", Range(0.1, 10.0)) = 1.0
        _DistortionAmount("Distortion Amount", Range(0.0, 0.5)) = 0.1
        _ParallaxStrength("Parallax Strength", Range(0.0, 0.1)) = 0.02
    }

        SubShader{
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            LOD 200

            CGPROGRAM
            #pragma surface surf Lambert alpha

            sampler2D _MainTex;
            sampler2D _BumpMap;
            float _DistortionSpeed;
            float _DistortionAmount;
            float _ParallaxStrength;

            struct Input {
                float2 uv_MainTex;
                float2 uv_BumpMap;
            };

            void surf(Input IN, inout SurfaceOutput o) {
                // Parallax mapping
                float2 viewDir = normalize(IN.viewDir.xy);
                float3 viewDirTangent = float3(viewDir, 0.0);
                float2 parallaxOffset = normalize(tex2D(_BumpMap, IN.uv_BumpMap).rg - 0.5) * _ParallaxStrength;
                IN.uv_MainTex += parallaxOffset * viewDirTangent.xy;

                // Wavy Distortion
                float time = _Time.y * _DistortionSpeed;
                float2 distortion = float2(sin(time), cos(time)) * _DistortionAmount;
                float2 distortedUV = IN.uv_MainTex + distortion;

                // Sample texture and apply the color
                fixed4 texColor = tex2D(_MainTex, distortedUV);
                o.Albedo = texColor.rgb;
                o.Alpha = texColor.a;
            }
            ENDCG
        }

            FallBack "Diffuse"
}