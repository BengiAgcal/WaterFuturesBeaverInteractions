using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace KWS
{
    class UnderwaterPass: WaterPass
    {
        UnderwaterPassCore _pass;
        private RTHandle _cameraColorBuffer;
        protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
        {
            if (_pass == null)
            {
                _pass = new UnderwaterPassCore(WaterInstance);
                _pass.OnSetRenderTarget += OnSetRenderTarget;
            }
            name = _pass.PassName;
        }

        protected override void Execute(CustomPassContext ctx)
        {
            var cam = ctx.hdCamera.camera;
            var cmd = ctx.cmd;

            if (!WaterInstance.IsCameraUnderwater || !WaterInstance.IsWaterVisible || !KWS_CoreUtils.CanRenderWaterForCurrentCamera(WaterInstance, cam)) return;

            IsInitialized = true;
            _cameraColorBuffer = ctx.cameraColorBuffer;
            _pass.Execute(cam, cmd, ctx.cameraColorBuffer);
        }

        private void OnSetRenderTarget(CommandBuffer cmd, RenderTargetIdentifier rt)
        {
            KWS_SPR_CoreUtils.SetRenderTarget(cmd, rt);
        }

    
        public override void Release()
        {
            if (_pass != null)
            {
                _pass.OnSetRenderTarget -= OnSetRenderTarget;
                _pass.Release();
            }
            IsInitialized = false;
        }
    }
}