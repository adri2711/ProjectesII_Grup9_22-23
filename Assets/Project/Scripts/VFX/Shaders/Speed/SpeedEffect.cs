using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpeedEffect : CustomEffect<SpeedEffect>
{
    protected override void Start()
    {
        ResetAll();
        GameEvents.instance.finishLevel += ResetAll;
    }
    public void Run(float percentage, float dur = 0)
    {
        index = (int)(Mathf.Min(percentage, 1f) * (profiles.Length - 1));
        base.Run(dur);
    }
    private void ResetAll()
    {
        foreach (VolumeProfile p in profiles)
        {
            foreach (VolumeComponent component in p.components)
            {
                component.SetAllOverridesTo(false);
            }
        }
    }
}
