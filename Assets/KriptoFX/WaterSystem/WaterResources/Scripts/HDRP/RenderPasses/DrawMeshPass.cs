using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

namespace KWS
{
    public class DrawMeshPass: WaterPass
    {
        DrawMeshPassCore _pass;
        CommandBuffer _cmd;

        private CustomPassContext _context;

        protected override void Setup(ScriptableRenderContext renderContext, CommandBuffer cmd)
        {
            if (_pass == null)
            {
                _pass = new DrawMeshPassCore(WaterInstance);
            }
            name = _pass.PassName;
        }

        protected override void Execute(CustomPassContext ctx)
        {
            var cam = ctx.hdCamera.camera;
            _cmd = ctx.cmd;
          
            if (!WaterInstance.IsWaterVisible || !KWS_CoreUtils.CanRenderWaterForCurrentCamera(WaterInstance, cam)) return;

            IsInitialized = true;

            _context = ctx;
            _pass.Execute(cam, _cmd);
        }

        public override void Release()
        {
            if (_pass != null)
            {
                _pass.Release();
            }
            IsInitialized = false;
        }
    }
}