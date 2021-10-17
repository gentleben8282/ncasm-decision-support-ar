﻿// Weather Maker for Unity
// (c) 2016 Digital Ruby, LLC
// Source code may be used for personal or commercial projects.
// Source code may NOT be redistributed or sold.
// 
// *** A NOTE ABOUT PIRACY ***
// 
// If you got this asset from a pirate site, please consider buying it from the Unity asset store at https://assetstore.unity.com/packages/slug/60955?aid=1011lGnL. This asset is only legally available from the Unity Asset Store.
// 
// I'm a single indie dev supporting my family by spending hundreds and thousands of hours on this and other assets. It's very offensive, rude and just plain evil to steal when I (and many others) put so much hard work into the software.
// 
// Thank you.
//
// *** END NOTE ABOUT PIRACY ***
//

#ifndef __WEATHER_MAKER_CLOUD_SHADER_UNIFORMS__
#define __WEATHER_MAKER_CLOUD_SHADER_UNIFORMS__

#include "WeatherMakerLightShaderInclude.cginc"

static const float CLOUD_COLOR_FORCE_REDRAW = fixed4(0.0, 0.0, 0.0, 0.0);

// inverse fade for soft cloud near depth
uniform float _CloudInvFade;

// flat
sampler2D _CloudNoise1;
sampler2D _CloudNoise2;
sampler2D _CloudNoise3;
sampler2D _CloudNoise4;
//UNITY_DECLARE_TEX2D_NOSAMPLER(_CloudNoiseMask1);
//UNITY_DECLARE_TEX2D_NOSAMPLER(_CloudNoiseMask2);
//UNITY_DECLARE_TEX2D_NOSAMPLER(_CloudNoiseMask3);
//UNITY_DECLARE_TEX2D_NOSAMPLER(_CloudNoiseMask4);
uniform fixed _CloudNoiseLod;
uniform fixed4 _CloudNoiseScale;
uniform fixed4 _CloudNoiseMultiplier;
uniform fixed4 _CloudNoiseAdder;
uniform float _CloudNoiseRotation[8]; // first 4 cos, second 4 sin
uniform float3 _CloudNoiseVelocity[4];
//uniform float _CloudNoiseMaskScale[4];
//uniform float2 _CloudNoiseMaskOffset[4];
//uniform float3 _CloudNoiseMaskVelocity[4];
//uniform float _CloudNoiseMaskRotation[8]; // first 4 cos, second 4 sin
uniform fixed4 _CloudColor[4];
uniform fixed4 _CloudEmissionColor[4];
uniform fixed _CloudAmbientGroundMultiplier[4];
uniform fixed _CloudAmbientSkyMultiplier[4];
uniform fixed4 _CloudScatterMultiplier[4];
uniform fixed4 _CloudCover;
uniform float4 _CloudHeight;
uniform float4 _CloudLightAbsorption;
uniform float _CloudShadowThreshold[4];
uniform float _CloudShadowPower[4];
uniform float _CloudShadowMultiplier;
uniform float _CloudRayOffset[4]; // brings clouds down at the horizon at the cost of stretching them over the top
uniform float4 _CloudNoiseDither;

uniform float _CloudDirLightMultiplierVolumetric;
uniform float _CloudPointSpotLightMultiplierVolumetric;

uniform float _CloudLightDitherLevel;

// per fragment state
struct DirLightPrecomputation
{
	float eyeDot;
	float intensity;
	float intensitySquared;
	float hg;
	float powderMultiplier;
	float powderAngle;
	float shadowPower;
	float lightConeRadius;
	float3 lightDir;
	fixed3 indirectLight;
};

struct CloudState
{
	DirLightPrecomputation dirLight[MAX_LIGHT_COUNT];
	float dithering;
	float lightStepSize;
	float lightColorDithering;
	float fade;
};

struct CloudColorResult
{
	fixed4 color;
	float fade;
	float hitCloud;
};

static const DirLightPrecomputation emptyDirLight = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

#endif // __WEATHER_MAKER_CLOUD_SHADER_UNIFORMS__