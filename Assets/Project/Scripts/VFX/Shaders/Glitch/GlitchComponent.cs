using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable, VolumeComponentMenuForRenderPipeline("Custom/GlitchComponent", typeof(UniversalRenderPipeline))]
public class GlitchComponent : CustomVolumeComponent
{
    public NoInterpColorParameter overlayColor = new NoInterpColorParameter(Color.cyan);
}