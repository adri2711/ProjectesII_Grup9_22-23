using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        //Glow Intensity
        SetParameter<float>(Mathf.Min(percentage, 1f) * (0.4f), 0, 0);
        //Lines Speed
        SetParameter<float>(Mathf.Min(percentage, 1f) * (0.6f) + 0.2f, 2, 1);
        //Lines Density
        SetParameter<float>(Mathf.Min(percentage, 0.8f) * (0.4f) + 0.4f, 3, 1);
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
