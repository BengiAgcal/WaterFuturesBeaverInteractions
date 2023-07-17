using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace KWS
{
    [ExecuteAlways]
    public class KWS_WaterPassHandler : MonoBehaviour
    {
        public WaterSystem WaterInstance;

        OrthoDepthPass _orthoDepthPass;
        ShorelineWavesPass _shorelineWavesPass;
        MaskDepthNormalPass _maskDepthNormalPass;
        CausticPass _causticPass;
        ReflectionFinalPass _reflectionFinalPass;
        ScreenSpaceReflectionPass _ssrPass;
        VolumetricLightingPass _volumetricLightingPass;
        DrawMeshPass _drawMeshPass;
        ShorelineFoamPass _shorelineFoamPass;
        ShorelineDrawFoamToScreenPass _shorelineDrawFoamToScreenPass;
        UnderwaterPass _underwaterPass;
        DrawToPosteffectsDepthPass _drawToDepthPass;

        private List<WaterPass> _waterPasses = new List<WaterPass>();
        Dictionary<CustomPassInjectionPoint, CustomPassVolume> _volumes = new Dictionary<CustomPassInjectionPoint, CustomPassVolume>();

        public void OnEnable()
        {
            RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
            _waterPasses.Clear();
            Initialize();
        }

        public void OnDisable()
        {
            RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
            foreach (var customPassVolume in _volumes) KW_Extensions.SafeDestroy(customPassVolume.Value.gameObject);
            _volumes.Clear();

            foreach (var waterPass in _waterPasses) if (waterPass != null) waterPass.Release();

        }

        private void OnBeginCameraRendering(ScriptableRenderContext ctx, Camera cam)
        {
            //"volume.customCamera" and "volumePass.enabled" just ignored for other cameras in the current frame... Typical unity HDRiP rendering.
            UpdateWaterPasses(cam);
        }

        void CreateVolume(CustomPassInjectionPoint injectionPoint)
        {
            var tempGO = new GameObject("WaterVolume_" + injectionPoint) { hideFlags = HideFlags.DontSave };
            tempGO.transform.parent = transform;
            var volume = tempGO.AddComponent<CustomPassVolume>();
            volume.injectionPoint = injectionPoint;
            _volumes.Add(injectionPoint, volume);
        }

        void Initialize()
        {
            CreateVolume(CustomPassInjectionPoint.BeforeRendering);
            CreateVolume(CustomPassInjectionPoint.BeforePreRefraction);
            CreateVolume(CustomPassInjectionPoint.BeforeTransparent);
        }

        void InitializePass<T>(ref T pass, CustomPassInjectionPoint injectionPoint, bool isUsed) where T : WaterPass
        {
            if (isUsed && pass == null)
            {
                var volumePass = _volumes[injectionPoint];
                pass = (T)volumePass.AddPassOfType<T>();
                pass.WaterInstance = WaterInstance;
                _waterPasses.Add(pass);
            }

            if (pass != null) pass.enabled = isUsed;
        }


        void UpdateWaterPasses(Camera cam)
        {
            var cameraSize = KWS_CoreUtils.GetScreenSizeLimited(WaterSystem.IsSinglePassStereoEnabled);
            KWS_RTHandles.SetReferenceSize(cameraSize.x, cameraSize.y, KWS.MSAASamples.None);

            bool isFirstWater = WaterSystem.VisibleWaterInstances.Count > 0 && WaterSystem.VisibleWaterInstances[0] == WaterInstance;

            InitializePass(ref _orthoDepthPass, CustomPassInjectionPoint.BeforeRendering, true);
            InitializePass(ref _shorelineWavesPass, CustomPassInjectionPoint.BeforePreRefraction, WaterInstance.Settings.UseShorelineRendering);
            InitializePass(ref _maskDepthNormalPass, CustomPassInjectionPoint.BeforePreRefraction, isFirstWater);
            InitializePass(ref _causticPass, CustomPassInjectionPoint.BeforePreRefraction, isFirstWater);
            InitializePass(ref _reflectionFinalPass, CustomPassInjectionPoint.BeforeTransparent, WaterInstance.Settings.UsePlanarReflection);
            InitializePass(ref _ssrPass, CustomPassInjectionPoint.BeforeTransparent, WaterInstance.Settings.UseScreenSpaceReflection);
            InitializePass(ref _volumetricLightingPass, CustomPassInjectionPoint.BeforeTransparent, WaterInstance.Settings.UseVolumetricLight);
            InitializePass(ref _shorelineFoamPass, CustomPassInjectionPoint.BeforeTransparent, WaterInstance.Settings.UseShorelineRendering);
            InitializePass(ref _drawMeshPass, CustomPassInjectionPoint.BeforeTransparent, true);
            InitializePass(ref _shorelineDrawFoamToScreenPass, CustomPassInjectionPoint.BeforeTransparent, WaterInstance.Settings.UseShorelineRendering);

            InitializePass(ref _underwaterPass, CustomPassInjectionPoint.BeforeTransparent, WaterInstance.Settings.UseUnderwaterEffect);
            InitializePass(ref _drawToDepthPass, CustomPassInjectionPoint.BeforeTransparent, WaterInstance.Settings.DrawToPosteffectsDepth);
        }

    }
}