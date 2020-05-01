Shader "Custom/SurfaceToonShader 4.1"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		[HDR] _Emission ("Emission", Color) = (0,0,0)
		_RampTex ("Lighting Ramp", 2D) = "white" {}
		_SpecularColor ("Specular Color", Color) = (1,1,1,1)
		_SpecularIntensity ("Specular Intensity", Range(0,1)) = 0.5
		_FresnelHue ("Fresnel Hue", Color) = (1,1,1,1)
		[PowerSlider(4)] _FresnelIntensity ("Fresnel Intensity", Range(0.25,4)) = 1
		_FresnelStep ("Fresnel Step", Range(0,0.3)) = 0.1
		_FresnelTransparency ("Fresnel Transparency", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Custom fullforwardshadows
        #pragma target 3.0
		
		#include "UnityPBSLighting.cginc"
		#include "UnityCG.cginc"

//----------------------------------------------------------------------------//

        sampler2D _MainTex;
		sampler2D _RampTex;
        fixed4 _Color;
		fixed4 _Emission;
		fixed4 _SpecularColor;
		half _SpecularIntensity;
		float3 _FresnelHue;
		float _FresnelIntensity;
		half _FresnelStep;
		float _FresnelTransparency;
		
//----------------------------------------------------------------------------//

        struct Input
        {
            float2 uv_MainTex;
			float3 worldNormal;
			float3 viewDir;
			INTERNAL_DATA
        };

//----------------------------------------------------------------------------//
	
		
		//Old Standard Lighting Effects -Only Emission is adding to the shader?-
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			//--------------FresnelPart--------------//
			float fresnel = dot(IN.worldNormal, IN.viewDir);
			fresnel = saturate(1 - fresnel);
			fresnel = pow(fresnel, _FresnelIntensity);
			float3 fresnelColor = fresnel * _FresnelHue;
			fresnelColor = step(_FresnelStep,fresnelColor);
			o.Emission = _Emission + fresnelColor*_FresnelTransparency;
			
            // --------------AlbedoStuff--------------//
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
		
		
		//The custom lighting function
		float4 LightingCustom(SurfaceOutputStandard s, float3 lightDir, float3 viewDir, float atten)
		{
		//--------------LightingRampPart--------------//
			float towardsLight = dot(s.Normal, lightDir);
			towardsLight = towardsLight * 0.5 + 0.5;
			float3 lightIntensity = tex2D(_RampTex, towardsLight).rgb;
			float4 col;
			col.rgb = lightIntensity * s.Albedo * atten * _LightColor0.rgb;
			col.a = s.Alpha;
			
		//---------------SpecularPart---------------//

			float3 viewReflect = reflect(-viewDir, s.Normal);
			float specularFalloff = max(0, dot(viewReflect, lightDir));
			specularFalloff = pow(specularFalloff, 1);
			specularFalloff = step(_SpecularIntensity,specularFalloff);
			float3 directSpecular = specularFalloff * normalize(_LightColor0.rgb*_SpecularColor);
			
		//---------------MixingPart---------------//
		
			float3 finalSurfaceColor = col + directSpecular;
			return float4(finalSurfaceColor, 0);
		}
		

		
		
        ENDCG
    }
    FallBack "Standard"
}
