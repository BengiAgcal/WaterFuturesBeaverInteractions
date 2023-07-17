using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace KWS
{
    class MaskDepthNormalPass : WaterPass
    {
        MaskDepthNormalPassCore _pass;

        protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
        {
            if (_pass == null)
            {
                _pass                           =  new MaskDepthNormalPassCore(WaterInstance);
                _pass.OnInitializedRenderTarget += OnInitializedRenderTarget;
            }
            name = _pass.PassName;
        }

        protected override void Execute(CustomPassContext ctx)
        {
            var cam = ctx.hdCamera.camera;
            if (!WaterInstance.IsWaterVisible || !KWS_CoreUtils.CanRenderWaterForCurrentCamera(WaterInstance, cam)) return;

            IsInitialized = true;
            _pass.Execute(cam, ctx.cmd);

        }

        private void OnInitializedRenderTarget(CommandBuffer cmd, KWS_RTHandle rt1, KWS_RTHandle rt2, KWS_RTHandle rt3)
        {
            KWS_SPR_CoreUtils.SetRenderTarget(cmd, rt1, rt2, rt3, ClearFlag.All, Color.black);
        }

        public override void Release()
        {
            if (_pass != null)
            {
                _pass.OnInitializedRenderTarget -= OnInitializedRenderTarget;
                _pass.Release();
            }
            IsInitialized = false;
        }
    }
}
