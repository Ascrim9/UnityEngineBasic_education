Shader "Custom/WaveEffect" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _DistortionSpeed("Distortion Speed", Range(0.1, 10.0)) = 1.0
        _DistortionAmount("Distortion Amount", Range(0.0, 0.5)) = 0.1
    }

        SubShader{
            Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
            LOD 200

            CGPROGRAM
            #pragma surface surf Lambert alpha

            sampler2D _MainTex;
            float _DistortionSpeed;
            float _DistortionAmount;

            struct Input {
                float2 uv_MainTex;
            };

            void surf(Input IN, inout SurfaceOutput o) {
                // 이미지 좌표에 시간에 따른 랜덤한 값을 더하여 왜곡 효과를 줍니다.
                float time = _Time.y * _DistortionSpeed;
                float2 distortion = float2(sin(time), cos(time)) * _DistortionAmount;

                // 왜곡된 이미지의 텍스처 좌표를 계산합니다.
                float2 distortedUV = IN.uv_MainTex + distortion;

                // 텍스처에서 색상을 샘플링하여 출력합니다.
                o.Albedo = tex2D(_MainTex, distortedUV).rgb;
                o.Alpha = tex2D(_MainTex, distortedUV).a;
            }
            ENDCG
        }

            FallBack "Diffuse"
}