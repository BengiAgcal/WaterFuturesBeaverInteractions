using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace KWS
{
    public class ShorelineDrawFoamToScreenPass : WaterPass
    {
        ShorelineDrawFoamToScreenPassCore _pass;
        CommandBuffer _cmd;

        protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
        {
            if (_pass == null)
            {
                _pass = new ShorelineDrawFoamToScreenPassCore(WaterInstance);
                _pass.OnSetRenderTarget += OnSetRenderTarget;
            }
            name = _pass.PassName;
        }

        protected override void Execute(CustomPassContext ctx)
        {
            var cam = ctx.hdCamera.camera;
            _cmd = ctx.cmd;
            if (!WaterInstance.IsWaterVisible || !KWS_CoreUtils.CanRenderWaterForCurrentCamera(WaterInstance, cam)) return;

            IsInitialized = true;
            _pass.Execute(cam, _cmd, ctx.cameraColorBuffer);

        }

        private void OnSetRenderTarget(CommandBuffer cmd, Camera cam, RenderTexture rt)
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
