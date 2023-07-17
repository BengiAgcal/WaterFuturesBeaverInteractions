using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace KWS
{
    public class OrthoDepthPass: WaterPass
    {
        OrthoDepthPassCore _pass;

        protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
        {
            if (_pass == null)
            {
                _pass          =  new OrthoDepthPassCore(WaterInstance);
                _pass.OnRender += OnRender;
                _pass.OnInitializedDepthCamera += OnInitializedDepthCamera;
                WaterInstance.OnUpdate += OnUpdate;
            }
            name = _pass.PassName;
        }

        private void OnInitializedDepthCamera(Camera depthCamera)
        {
            var data = depthCamera.GetComponent<HDAdditionalCameraData>();
            if (data == null) data = depthCamera.gameObject.AddComponent<HDAdditionalCameraData>();

            data.DisableAllCameraFrameSettings();
            data.SetCameraFrameSetting(FrameSettingsField.OpaqueObjects, true);
            data.clearColorMode = HDAdditionalCameraData.ClearColorMode.None;
            data.customRenderingSettings = true;
        }

        private void OnUpdate(WaterSystem waterSystem, Camera camera)
        {
            IsInitialized = true;
            if (!WaterInstance.IsWaterVisible || !KWS_CoreUtils.CanRenderWaterForCurrentCamera(WaterInstance, camera)) return;
            _pass.Execute(camera);
        }
    

        protected override void Execute(CustomPassContext ctx)
        {
          
        }

        private void OnRender(OrthoDepthPassCore.PassData passData, Camera depthCamera)
        {
            var shadows = QualitySettings.shadows;
            QualitySettings.shadows = ShadowQuality.Disable;

            var terrains   = Terrain.activeTerrains;
            var pixelError = new float[terrains.Length];
            for (var i = 0; i < terrains.Length; i++)
            {
                pixelError[i]                   = terrains[i].heightmapPixelError;
                terrains[i].heightmapPixelError = 1;
            }

            depthCamera.targetTexture = passData.DepthRT;
            depthCamera.Render();

            for (var i = 0; i < terrains.Length; i++)
            {
                terrains[i].heightmapPixelError = terrains[i].heightmapPixelError;
            }

            QualitySettings.shadows = shadows;
        }

        public override void Release()
        {
            if (_pass != null)
            {
                _pass.OnRender -= OnRender;
                _pass.OnInitializedDepthCamera -= OnInitializedDepthCamera;
                _pass.Release();
                WaterInstance.OnUpdate -= OnUpdate;
            }
            
            IsInitialized = false;
        }
    }
}