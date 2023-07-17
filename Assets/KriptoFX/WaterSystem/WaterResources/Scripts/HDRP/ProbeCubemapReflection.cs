using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace KWS
{
    public class ProbeCubemapReflection: ReflectionPass
    {
        private ReflectionProbe _probe;
        private HDAdditionalReflectionData _probeData;
        private GameObject _probeGO;
        private Transform _probeTransform;

        private Dictionary<WaterSystem.CubemapReflectionResolutionQualityEnum, PlanarReflectionAtlasResolution> _resolutions =
            new Dictionary<WaterSystem.CubemapReflectionResolutionQualityEnum, PlanarReflectionAtlasResolution>()
            {
                {WaterSystem.CubemapReflectionResolutionQualityEnum.High, PlanarReflectionAtlasResolution.Resolution1024},
                {WaterSystem.CubemapReflectionResolutionQualityEnum.Medium, PlanarReflectionAtlasResolution.Resolution512},
                {WaterSystem.CubemapReflectionResolutionQualityEnum.Low, PlanarReflectionAtlasResolution.Resolution256}
            };

        public override void RenderReflection(WaterSystem waterInstance, Camera currentCamera)
        {
            if (!waterInstance.Settings.EnabledMeshRendering) return;
            if (!KWS_CoreUtils.CanRenderWaterForCurrentCamera(waterInstance, currentCamera)) return;

            if (_probeGO == null) CreateProbe(waterInstance);

            var cameraPos = KW_Extensions.GetCameraPositionFast(currentCamera);
            _probeTransform.position = new Vector3(cameraPos.x, waterInstance.WaterRelativeWorldPosition.y, cameraPos.z);
            _probe.RequestRenderNextUpdate();
            var rt = _probeData.realtimeTexture;
            if (rt == null) return;

            waterInstance.SharedData.CubemapReflection = rt;
        }

        public override void OnEnable()
        {
            WaterSystem.OnWaterSettingsChanged += OnWaterSettingsChanged;
        }

        private void OnWaterSettingsChanged(WaterSystem waterInstance, WaterSystem.WaterTab changedTab)
        {
            if (!changedTab.HasFlag(WaterSystem.WaterTab.Reflection)) return;

            UpdateProbeSettings(waterInstance);
        }

        public override void Release()
        {
            WaterSystem.OnWaterSettingsChanged -= OnWaterSettingsChanged;
            KW_Extensions.SafeDestroy(_probeGO);
        }

        void CreateProbe(WaterSystem waterInstance)
        {
            _probeGO = new GameObject("CubemapReflectionProbe");
            _probeTransform = _probeGO.transform;
            _probeTransform.parent = waterInstance.WaterTemporaryObject.transform;
            _probe = _probeGO.AddComponent<ReflectionProbe>();

            _probe.mode = ReflectionProbeMode.Realtime;
            _probe.refreshMode = ReflectionProbeRefreshMode.ViaScripting;
            _probe.cullingMask = waterInstance.Settings.CubemapCullingMask;
        

            _probeData = _probeGO.AddComponent<HDAdditionalReflectionData>();
            _probeData.mode = ProbeSettings.Mode.Realtime;
            _probeData.realtimeMode = ProbeSettings.RealtimeMode.OnDemand;
            _probeData.influenceVolume.shape = InfluenceShape.Box;
            _probeData.influenceVolume.boxSize = new Vector3(100000, float.MinValue, 100000);

            _probeData.SetFrameSetting(FrameSettingsField.VolumetricClouds, true);
            UpdateProbeSettings(waterInstance);
        }

        void UpdateProbeSettings(WaterSystem waterInstance)
        {
            var field = _probeData.GetType().GetField("m_ProbeSettings", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
            if (field != null)
            {
                var settings = _probeData.settings;
                settings.roughReflections                       = false;
                settings.resolutionScalable                     = new ProbeSettings.PlanarReflectionAtlasResolutionScalableSettingValue();
                settings.resolutionScalable.useOverride         = true;
                settings.resolutionScalable.@override = _resolutions[waterInstance.Settings.CubemapReflectionResolutionQuality];
                settings.cameraSettings.customRenderingSettings = true;

                field.SetValue(_probeData, settings);
            }
        }
    }
}