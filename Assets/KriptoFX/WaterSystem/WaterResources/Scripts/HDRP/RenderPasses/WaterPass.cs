using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

namespace  KWS
{
    public abstract class WaterPass : CustomPass
    {
        public WaterSystem WaterInstance;
        public bool IsInitialized;
        public abstract void Release();
    }
}
