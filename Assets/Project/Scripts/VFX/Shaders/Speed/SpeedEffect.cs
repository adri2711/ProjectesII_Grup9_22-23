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
        /*VolumeParameter<float> p = new VolumeParameter<float>();
        p.Override(Mathf.Min(percentage, 1f) * (0.4f));
        profiles[0].components[0].parameters[0].SetValue(p);//*/
        SetParameter<float>(Mathf.Min(percentage, 1f) * (0.4f), 0);
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
