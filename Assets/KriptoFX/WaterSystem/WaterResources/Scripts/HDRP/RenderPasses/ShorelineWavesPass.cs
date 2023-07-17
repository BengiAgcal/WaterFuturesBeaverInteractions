using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace KWS
{
    public class ShorelineWavesPass : WaterPass
    {
        ShorelineWavesPassCore _pass;
        CommandBuffer _cmd;

        protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
        {
            if (_pass == null)
            {
                _pass = new ShorelineWavesPassCore(WaterInstance);
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
            _pass.Execute(cam, _cmd);

        }

        private void OnSetRenderTarget(CommandBuffer cmd, Camera cam, RenderTexture rt1, RenderTexture rt2)
        {
            KWS_SPR_CoreUtils.SetRenderTarget(cmd, rt1, rt2, rt1.depthBuffer, ClearFlag.Color, Color.black);
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
