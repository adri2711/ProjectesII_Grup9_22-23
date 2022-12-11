using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable]
public class CustomVolumeComponent : VolumeComponent, IPostProcessComponent
{
    public ClampedFloatParameter intensity = new ClampedFloatParameter(value: 0, min: 0, max: 1, overrideState: true);

    // Tells when our effect should be rendered
    public virtual bool IsActive() => intensity.value > 0;

    // I have no idea what this does yet but I'll update the post once I find an usage
    public virtual bool IsTileCompatible() => true;
}