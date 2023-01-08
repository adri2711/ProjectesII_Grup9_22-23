using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable, VolumeComponentMenuForRenderPipeline("Custom/WrongComponent", typeof(UniversalRenderPipeline))]
public class WrongComponent : CustomVolumeComponent
{
    public NoInterpColorParameter overlayColor = new NoInterpColorParameter(Color.cyan);
}