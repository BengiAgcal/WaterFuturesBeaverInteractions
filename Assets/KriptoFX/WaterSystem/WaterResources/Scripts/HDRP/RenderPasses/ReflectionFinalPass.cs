using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace KWS
{
    class ReflectionFinalPass: WaterPass
    {
        ReflectionFinalPassCore _pass;
        CommandBuffer _cmd;

        protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
        {
            if (_pass == null)
            {
                _pass = new ReflectionFinalPassCore(WaterInstance);
                _pass.OnInitializedRenderTarget += OnInitializedRenderTarget;
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

        private void OnInitializedRenderTarget(CommandBuffer cmd, Camera camera, RenderTexture rt)
        {
            KWS_SPR_CoreUtils.SetRenderTarget(cmd, rt, ClearFlag.Color, Color.black);
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