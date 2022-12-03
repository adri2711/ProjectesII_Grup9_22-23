using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpeedEffect : CustomEffect<SpeedEffect>
{
    protected override void Start()
    {
        GameEvents.instance.finishLevel += End;
    }
    public void Run(float percentage, float dur = 0)
    {
        index = (int)(Mathf.Min(percentage, 1f) * (levels.Length - 1));
        base.Run(dur);
    }
}
