Shader "Hidden/KriptoFX/KWS/VolumetricLighting"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" { }
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			HLSLPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma target 4.5

			#pragma multi_compile _ USE_CAUSTIC
			#pragma multi_compile _ USE_LOD1 USE_LOD2 USE_LOD3

			#pragma multi_compile _ LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
			#pragma multi_compile _ ENABLE_REPROJECTION
			#pragma multi_compile _ ENABLE_ANISOTROPY
			#pragma multi_compile _ VL_PRESET_OPTIMAL
			#pragma multi_compile _ SUPPORT_LOCAL_LIGHTS

			//#define LIGHT_EVALUATION_NO_CONTACT_SHADOWS // To define before LightEvaluation.hlsl
			// #define LIGHT_EVALUATION_NO_HEIGHT_FOG

			/*        #ifndef LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
			            #define USE_BIG_TILE_LIGHTLIST
			#endif*/



			#define PREFER_HALF 0
			#define GROUP_SIZE_1D           8
			#define SHADOW_USE_DEPTH_BIAS   0 // Too expensive, not particularly effective
			#define SHADOW_LOW          // Different options are too expensive.
			#define AREA_SHADOW_LOW
			#define SHADOW_AUTO_FLIP_NORMAL 0 // No normal information, so no need to flip
			#define SHADOW_VIEW_BIAS        1 // Prevents light leaking through thin geometry. Not as good as normal bias at grazing angles, but cheaper and independent from the geometry.
			#define USE_DEPTH_BUFFER        1 // Accounts for opaque geometry along the camera ray

			// Filter out lights that are not meant to be affecting volumetric once per thread rather than per voxel. This has the downside that it limits the max processable lights to MAX_SUPPORTED_LIGHTS
			// which might be lower than the max number of lights that might be in the big tile.
			//#define PRE_FILTER_LIGHT_LIST   1 && defined(USE_BIG_TILE_LIGHTLIST)
			// #define USE_CLUSTERED_LIGHTLIST
			#define USE_FPTL_LIGHTLIST // Use light tiles for contact shadows
			#define LIGHTLOOP_DISABLE_TILE_AND_CLUSTER

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/GeometricTools.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Filtering.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/VolumeRendering.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/EntityLighting.hlsl"


			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Builtin/BuiltinData.hlsl"

			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"

			// #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/VolumetricLighting/VolumetricLighting.cs.hlsl"
			// #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/VolumetricLighting/VBuffer.hlsl"

			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Sky/PhysicallyBasedSky/PhysicallyBasedSkyCommon.hlsl"

			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/LightLoopDef.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightEvaluation.hlsl"

			#include "../PlatformSpecific/Includes/KWS_HelpersIncludes.cginc"
			#include "../Common/CommandPass/KWS_VolumetricLight_Common.cginc"

			//#if PRE_FILTER_LIGHT_LIST
			//
			//#if MAX_NR_BIG_TILE_LIGHTS_PLUS_ONE > 48
			//#define MAX_SUPPORTED_LIGHTS 48
			//#else
			//#define MAX_SUPPORTED_LIGHTS MAX_NR_BIG_TILE_LIGHTS_PLUS_ONE
			//#endif
			//
			//int gs_localLightList[GROUP_SIZE_1D * GROUP_SIZE_1D][MAX_SUPPORTED_LIGHTS];
			//
			//#endif


			half4 RayMarchDirLight(RaymarchData raymarchData, uint rayMarchSteps, bool isUnderwater)
			{
				half4 result = 0;

				if (_DirectionalShadowIndex >= 0)
				{
					LightLoopContext context;
					context.shadowContext = InitShadowContext();
					PositionInputs posInput;
					float exposure = GetCurrentExposureMultiplier();

					DirectionalLightData dirLight = _DirectionalLightDatas[_DirectionalShadowIndex];
					float3 L = -dirLight.forward;
					float3 currentPos = raymarchData.currentPos;

					[loop]
					for (uint j = 0; j < KWS_RayMarchSteps; ++j)
					{
						
						int cascadeCount;

						
						posInput.positionWS = currentPos;
						if ((dirLight.volumetricLightDimmer > 0) && (dirLight.volumetricShadowDimmer > 0))
							context.shadowValue = GetDirectionalShadowAttenuation(context.shadowContext, raymarchData.uv, GetCameraRelativePositionWS(currentPos), 0, dirLight.shadowIndex, L);
						else context.shadowValue = 1;

						float4 lightColor = EvaluateLight_Directional(context, posInput, dirLight);
						lightColor.a *= dirLight.volumetricLightDimmer;
						lightColor.rgb *= lightColor.a; // Composite
						context.shadowValue = lerp(1, context.shadowValue, dirLight.volumetricShadowDimmer);
						float3 scattering = raymarchData.stepSize;
						#if defined(USE_CAUSTIC)
							float underwaterStrength = lerp(saturate((KW_Transparent - 1) / 5) * 0.5, 1, isUnderwater);
							scattering += scattering * RaymarchCaustic(raymarchData.rayStart, currentPos, L) * underwaterStrength;

						#endif
						float3 light = context.shadowValue * scattering * lightColor.xyz * exposure;
						result.rgb += light;
						currentPos += raymarchData.step;
					}
					float cosAngle = dot(dirLight.forward.xyz, -raymarchData.rayDir);
					result.rgb *= MieScattering(cosAngle);
					
					if (!isUnderwater) result.a = GetDirectionalShadowAttenuation(context.shadowContext, raymarchData.uv, GetCameraRelativePositionWS(raymarchData.rayStart), 0, dirLight.shadowIndex, L);
				}
				return result;
			}
			
			
			half4 RayMarchAdditionalLights(RaymarchData raymarchData, uint rayMarchSteps)
			{
				half4 result = 0;

				if (LIGHTFEATUREFLAGS_PUNCTUAL)
				{
					uint lightCount, lightStart;
					LightLoopContext context;
					context.shadowContext = InitShadowContext();
					PositionInputs posInput;
					float exposure = GetCurrentExposureMultiplier();


					#ifndef LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
						uint2 pixelCoord = uint2(raymarchData.uv * KWS_VolumeTexSceenSize.xy);
						int2 tileCoord = (float2)pixelCoord / GetTileSize();
						PositionInputs posInput = GetPositionInput(pixelCoord, KWS_VolumeTexSceenSize.zw, tileCoord);
						GetCountAndStart(posInput, LIGHTCATEGORY_PUNCTUAL, lightStart, lightCount);
					#else   // LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
						lightCount = _PunctualLightCount;
						lightStart = 0;
					#endif

					uint startFirstLane = 0;
					bool fastPath;

					fastPath = IsFastPath(lightStart, startFirstLane);
					if (fastPath)
					{
						lightStart = startFirstLane;
					}

					uint v_lightIdx = lightStart;
					uint v_lightListOffset = 0;
					while(v_lightListOffset < lightCount)
					{
						v_lightIdx = FetchIndex(lightStart, v_lightListOffset);
						uint s_lightIdx = ScalarizeElementIndex(v_lightIdx, fastPath);
						if (s_lightIdx == -1)
							break;

						LightData addLight = FetchLight(s_lightIdx);
						if (s_lightIdx >= v_lightIdx)
						{
							v_lightListOffset++;
							float3 currentPos = raymarchData.currentPos;

							[loop]
							for (uint i = 0; i < KWS_RayMarchSteps; ++i)
							{

								float3 L;
								float4 distances; // {d, d^2, 1/d, d_proj}
								GetPunctualLightVectors(GetCameraRelativePositionWS(currentPos), addLight, L, distances);

								float4 lightColor = float4(addLight.color, 1.0);
								lightColor.a = PunctualLightAttenuation(distances, addLight.rangeAttenuationScale, addLight.rangeAttenuationBias, addLight.angleScale, addLight.angleOffset);
								lightColor.a *= addLight.volumetricLightDimmer;
								lightColor.rgb *= lightColor.a;

								float shadow = GetPunctualShadowAttenuation(context.shadowContext, raymarchData.uv, currentPos, 0, addLight.shadowIndex, L, distances.x, addLight.lightType == GPULIGHTTYPE_POINT, addLight.lightType != GPULIGHTTYPE_PROJECTOR_BOX);

								lightColor.rgb *= ComputeShadowColor(shadow, addLight.shadowTint, addLight.penumbraTint);
								float3 scattering = raymarchData.stepSize * lightColor.rgb * exposure;
								float3 light = scattering;

								float cosAngle = dot(-raymarchData.rayDir, normalize(currentPos - addLight.positionRWS));
								light *= MieScattering(cosAngle) * 5;

								result.rgb += light;
								currentPos += raymarchData.step;
							}
						}
					}
				}

				return result;
			}

			inline float4 RayMarch(RaymarchData raymarchData, bool isUnderwater)
			{

				float4 result = 0;
				
				float extinction = 0;
				
				result += RayMarchDirLight(raymarchData, KWS_RayMarchSteps, isUnderwater);
				result += RayMarchAdditionalLights(raymarchData, KWS_RayMarchSteps);

				result.rgb /= KW_Transparent;
				result.rgb *= KWS_VolumeDepthFade;
				//result.rgb *= 4;
				result.rgb = max(MIN_THRESHOLD * 2, result.rgb);
				return result;
			}


			half4 frag(vertexOutput i) : SV_Target
			{
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				half mask = GetWaterMask(i.uv);
				
				UNITY_BRANCH
				if (EarlyDiscardUnderwaterPixels(mask)) return 0;

				float depthTop = GetWaterDepth(i.uv);
				float depthBot = GetSceneDepth(i.uv);

				//UNITY_BRANCH
				//if (EarlyDiscardDepthOcclusionPixels(depthBot, depthTop, mask)) return 0; //todo blur pass wil have black color leaking in this case. Maybe I need to add the same method for blur pass?

				bool isUnderwater = IsUnderwaterMask(mask);
				RaymarchData raymarchData = InitRaymarchData(i, depthTop, depthBot, isUnderwater);
				
				half4 finalColor = RayMarch(raymarchData, isUnderwater);
				
				return finalColor;
			}


			ENDHLSL
		}
	}
}