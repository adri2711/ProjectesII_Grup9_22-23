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

    private void LowTime()
    {
        Run(lowTimeDur);
    }
}
