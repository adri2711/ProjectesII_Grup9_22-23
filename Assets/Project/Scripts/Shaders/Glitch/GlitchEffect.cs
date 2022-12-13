using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GlitchEffect : CustomEffect<GlitchEffect>
{
    [SerializeField] private float lowTimeDur = 0.2f;
    protected override void Start()
    {
        base.Start();
        GameEvents.instance.lowTimeEffect += LowTime;
    }
    public void Run(float dur = 0, float intensity = 1)
    {
        SetParameter<float>(intensity, 0, 0);
        base.Run(dur);
    }
    private void LowTime()
    {
        Run(lowTimeDur, 0.8f);
    }
}
