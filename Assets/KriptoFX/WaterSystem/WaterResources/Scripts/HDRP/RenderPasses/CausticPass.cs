using System.Collections.Generic;
using KWS;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace KWS
{

    public class CausticPass: WaterPass
    {
        CausticPassCore _pass;
        private RTHandle _currentCameraColorBuffer;
        protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
        {
            if (_pass == null)
            {
                _pass                         =  new CausticPassCore(WaterInstance);
                _pass.OnRenderToCausticTarget += OnRenderToCausticTarget;
                _pass.OnRenderToCameraTarget  += OnRenderToCameraTarget;
            }
            name = _pass.PassName;
        }

        protected override void Execute(CustomPassContext ctx)
        {
            var cam = ctx.hdCamera.camera;
            if (!WaterInstance.IsWaterVisible || !KWS_CoreUtils.CanRenderWaterForCurrentCamera(WaterInstance, cam)) return;

            IsInitialized = true;

            _currentCameraColorBuffer = ctx.cameraColorBuffer;
            _pass.Execute(cam, ctx.cmd);

        }

        private void OnRenderToCausticTarget(CommandBuffer cmd, RenderTexture rt)
        {
            KWS_SPR_CoreUtils.SetRenderTarget(cmd, rt, ClearFlag.Color, Color.black);
        }

        private void OnRenderToCameraTarget(CommandBuffer cmd)
        {
            KWS_SPR_CoreUtils.SetRenderTarget(cmd, _currentCameraColorBuffer);
        }

        public override void Release()
        {
            if (_pass != null)
            {
                _pass.OnRenderToCausticTarget -= OnRenderToCausticTarget;
                _pass.OnRenderToCameraTarget  -= OnRenderToCameraTarget;
                _pass.Release();
            }
            IsInitialized = false;
        }

    }
}
