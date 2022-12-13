using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable, VolumeComponentMenuForRenderPipeline("Custom/SpeedComponent", typeof(UniversalRenderPipeline))]
public class SpeedComponent : CustomVolumeComponent
{
    public NoInterpColorParameter overlayColor = new NoInterpColorParameter(Color.cyan);
}