using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace KWS
{
    public class ProbePlanarReflection: ReflectionPass
    {
        private PlanarReflectionProbe _probe;
        private GameObject _probeGO;
        private Transform _probeTransform;
        private Material _filteringMaterial;
        private CommandBuffer _cmdAnisoFiltering;
        RenderTexture _currentPlanarRT;
        RenderTexture _planarMipFilteredRT;
        private WaterSystem _waterInstance;

        private readonly Dictionary<WaterSystem.PlanarReflectionResolutionQualityEnum, PlanarReflectionAtlasResolution> _planarResolutions
            = new Dictionary<WaterSystem.PlanarReflectionResolutionQualityEnum, PlanarReflectionAtlasResolution>()

            {
                {WaterSystem.PlanarReflectionResolutionQualityEnum.Ultra, PlanarReflectionAtlasResolution.Resolution1024},
                {WaterSystem.PlanarReflectionResolutionQualityEnum.High, PlanarReflectionAtlasResolution.Resolution1024},
                {WaterSystem.PlanarReflectionResolutionQualityEnum.Medium, PlanarReflectionAtlasResolution.Resolution512},
                {WaterSystem.PlanarReflectionResolutionQualityEnum.Low, PlanarReflectionAtlasResolution.Resolution512},
                {WaterSystem.PlanarReflectionResolutionQualityEnum.VeryLow, PlanarReflectionAtlasResolution.Resolution256}
            };

        public override void RenderReflection(WaterSystem waterInstance, Camera currentCamera)
        {
            _waterInstance = waterInstance;
            if (!_waterInstance.Settings.UsePlanarReflection) return;

            if (!_waterInstance.Settings.EnabledMeshRendering) return;
            if (!KWS_CoreUtils.CanRenderWaterForCurrentCamera(waterInstance, currentCamera)) return;

            if (_probeGO == null) CreateProbe(waterInstance);

            var cameraPos = KW_Extensions.GetCameraPositionFast(currentCamera);
            _probeTransform.position = new Vector3(cameraPos.x, waterInstance.WaterRelativeWorldPosition.y, cameraPos.z);

            _currentPlanarRT = _probe.realtimeTexture;
            if (_currentPlanarRT == null) return;
            CreateTargetTexture(_currentPlanarRT.width, _currentPlanarRT.graphicsFormat);

            _waterInstance.SharedData.PlanarReflectionRaw = _currentPlanarRT;
        }

        public override void OnEnable()
        {
            WaterSystem.OnWaterSettingsChanged += OnWaterSettingsChanged;
        }

        private void OnWaterSettingsChanged(WaterSystem waterInstance, WaterSystem.WaterTab changedTab)
        {
            if (!changedTab.HasFlag(WaterSystem.WaterTab.Reflection) || !waterInstance.Settings.UsePlanarReflection || _probeGO == null) return;

            UpdateProbeSettings(waterInstance);
        }

        public override void Release()
        {
            WaterSystem.OnWaterSettingsChanged -= OnWaterSettingsChanged;

            KW_Extensions.SafeDestroy(_probeGO, _filteringMaterial);
        }

        void CreateProbe(WaterSystem waterInstance)
        {
            _probeGO = new GameObject("PlanarReflectionProbe");
            _probeGO.layer = KWS_Settings.Water.WaterLayer;
            _probeTransform = _probeGO.transform;
            _probeTransform.parent = waterInstance.WaterTemporaryObject.transform;
            _probe = _probeGO.AddComponent<PlanarReflectionProbe>();
            
            _probe.mode                    = ProbeSettings.Mode.Realtime;
            _probe.realtimeMode            = ProbeSettings.RealtimeMode.EveryFrame;
            _probe.influenceVolume.boxSize = new Vector3(100000, float.MinValue, 100000);

            UpdateProbeSettings(waterInstance);
        }

        void UpdateProbeSettings(WaterSystem waterInstance)
        {
            _probe.DisableAllCameraFrameSettings();
           
            _probe.SetFrameSetting(FrameSettingsField.OpaqueObjects,      true);
            _probe.SetFrameSetting(FrameSettingsField.TransparentObjects, true);

            _probe.SetFrameSetting(FrameSettingsField.VolumetricClouds, waterInstance.Settings.RenderPlanarClouds);
            _probe.SetFrameSetting(FrameSettingsField.AtmosphericScattering, waterInstance.Settings.RenderPlanarVolumetricsAndFog);
            _probe.SetFrameSetting(FrameSettingsField.Volumetrics, waterInstance.Settings.RenderPlanarVolumetricsAndFog);
            _probe.SetFrameSetting(FrameSettingsField.ShadowMaps,            waterInstance.Settings.RenderPlanarShadows);

            var field = _probe.GetType().GetField("m_ProbeSettings", BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
            if (field != null)
            {
                var settings = _probe.settings;
                settings.roughReflections = false;
                settings.resolutionScalable             = new ProbeSettings.PlanarReflectionAtlasResolutionScalableSettingValue();
                settings.resolutionScalable.useOverride = true;
                settings.resolutionScalable.@override   = _planarResolutions[waterInstance.Settings.PlanarReflectionResolutionQuality];
                settings.cameraSettings.culling.cullingMask = waterInstance.Settings.PlanarCullingMask;
                settings.cameraSettings.customRenderingSettings = true;
               
                field.SetValue(_probe, settings);
            }
        }

        void CreateTargetTexture(int size, GraphicsFormat graphicsFormat)
        {
            if (_planarMipFilteredRT != null && (_planarMipFilteredRT.width != size || _planarMipFilteredRT.graphicsFormat != graphicsFormat))
            {
                _planarMipFilteredRT.Release();
                _planarMipFilteredRT = null;
            }

            if(_planarMipFilteredRT == null) _planarMipFilteredRT = new RenderTexture(size, size, 0, graphicsFormat) { name = "_planarMipFilteredRT", autoGenerateMips = true, useMipMap = true };
        }

        //void RenderAnisotropicBlur(WaterSystem waterInstance, RenderTexture sourceRT, RenderTexture targetFilteredRT)
        //{
        //    if (_filteringMaterial == null) _filteringMaterial = KWS_CoreUtils.CreateMaterial(KWS_ShaderConstants.ShaderNames.ReflectionFiltering);
        //    _filteringMaterial.SetFloat(KWS_ShaderConstants.ReflectionsID.KWS_AnisoReflectionsScale, waterInstance.AnisoScaleRelativeToWind);
        //    _filteringMaterial.SetFloat(KWS_ShaderConstants.ReflectionsID.KWS_NormalizedWind,        Mathf.Clamp01(waterInstance.Settings.WindSpeed * 0.5f));

        //    if (_cmdAnisoFiltering == null) _cmdAnisoFiltering = new CommandBuffer() { name = "Water.AnisotropicReflectionPass" };
        //    else _cmdAnisoFiltering.Clear();
        //    _cmdAnisoFiltering.BlitTriangle(sourceRT, Vector4.one, targetFilteredRT, _filteringMaterial, waterInstance.Settings.AnisotropicReflectionsHighQuality ? 1 : 0);
        //    Graphics.ExecuteCommandBuffer(_cmdAnisoFiltering);
        //}
    }
}