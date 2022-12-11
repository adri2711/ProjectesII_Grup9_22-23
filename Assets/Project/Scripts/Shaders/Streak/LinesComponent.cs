using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable, VolumeComponentMenuForRenderPipeline("Custom/LinesComponent", typeof(UniversalRenderPipeline))]
public class LinesComponent : CustomVolumeComponent
{
    public NoInterpColorParameter overlayColor = new NoInterpColorParameter(Color.white);
    public ClampedFloatParameter speed = new ClampedFloatParameter(value: 0.2f, min: 0.2f, max: 1, overrideState: true);
    public ClampedFloatParameter density = new ClampedFloatParameter(value: 0.5f, min: 0.4f, max: 0.8f, overrideState: true);
}