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
                // �̹��� ��ǥ�� �ð��� ���� ������ ���� ���Ͽ� �ְ� ȿ���� �ݴϴ�.
                float time = _Time.y * _DistortionSpeed;
                float2 distortion = float2(sin(time), cos(time)) * _DistortionAmount;

                // �ְ�� �̹����� �ؽ�ó ��ǥ�� ����մϴ�.
                float2 distortedUV = IN.uv_MainTex + distortion;

                // �ؽ�ó���� ������ ���ø��Ͽ� ����մϴ�.
                o.Albedo = tex2D(_MainTex, distortedUV).rgb;
                o.Alpha = tex2D(_MainTex, distortedUV).a;
            }
            ENDCG
        }

            FallBack "Diffuse"
}