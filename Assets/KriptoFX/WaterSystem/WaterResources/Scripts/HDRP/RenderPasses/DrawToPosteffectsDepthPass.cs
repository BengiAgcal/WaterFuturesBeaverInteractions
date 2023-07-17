using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace KWS
{
    class DrawToPosteffectsDepthPass : WaterPass
    {
        DrawToPosteffectsDepthPassCore _pass;
        private CustomPassContext _context;
        protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
        {
            if (_pass == null)
            {
                _pass = new DrawToPosteffectsDepthPassCore(WaterInstance);
                _pass.OnSetRenderTarget += OnSetRenderTarget;
            }
            name = _pass.PassName;
        }

        private void OnSetRenderTarget(CommandBuffer cmd, Camera cam)
        {
            KWS_SPR_CoreUtils.SetRenderTarget(cmd, _context.cameraColorBuffer, _context.cameraDepthBuffer);
        }

        protected override void Execute(CustomPassContext ctx)
        {
            var cam = ctx.hdCamera.camera;
            if (!WaterInstance.IsWaterVisible || !KWS_CoreUtils.CanRenderWaterForCurrentCamera(WaterInstance, cam)) return;

            IsInitialized = true;
            _context = ctx;
            _pass.Execute(cam, ctx.cmd, ctx.cameraDepthBuffer);
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
