Shader "Unlit/Hologram"
{
    Properties
    {
        _MainTex ("Albedo Texture", 2D) = "white" {}
        _TintColor("Tint Colo", Color) = (1,1,1,1)
        _Transparency("Tramsperency", Range(0.0, 0.5)) = 0.25
        _CutoutThresh("Cutout Treshhold", Range(0.0, 1.0)) = 0.2
        _Distance("Distance", Float) = 1
        _Amplitude("Amplitude", Float) = 1
        _Speed("Speed", Float) = 1
        _Amount("Amount", Range(0.0, 1.0)) = 1
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            // Dataobjects
            struct appdata
            {
                float4 vertex : POSITION; // x, y, z, w
                float2 uv : TEXCOORD0; // TextureCoordinates
            };

            struct v2f // vert to frag
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION; // Screenspace Position
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _TintColor;
            float _Transparency;
            float _CutoutThresh;
            float _Amplitude;
            float _Distance;
            float _Speed;
            float _Amount;

            v2f vert (appdata v)
            {
                v2f o; // create output
                v.vertex.x += sin(_Time.y * _Speed * v.vertex.y * _Amplitude) * _Distance * _Amount; 
                o.vertex = UnityObjectToClipPos(v.vertex); // From localspace to Screenspace
                o.uv = TRANSFORM_TEX(v.uv, _MainTex); 
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) + _TintColor;
                col.a = _Transparency;
                clip(col.r - _CutoutThresh);
                return col;
            }
            ENDCG
        }
    }
}
