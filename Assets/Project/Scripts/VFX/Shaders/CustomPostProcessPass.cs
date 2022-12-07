using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class CustomPostProcessPass : ScriptableRenderPass
{
    // Used to render from camera to post processings
    // back and forth, until we render the final image to
    // the camera
    RenderTargetIdentifier source;
    RenderTargetIdentifier destinationA;
    RenderTargetIdentifier destinationB;
    RenderTargetIdentifier latestDest;

    readonly int temporaryRTIdA = Shader.PropertyToID("_TempRT");
    readonly int temporaryRTIdB = Shader.PropertyToID("_TempRTB");

    public CustomPostProcessPass()
    {
        // Set the render pass event
        renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }

    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        // Grab the camera target descriptor. We will use this when creating a temporary render texture.
        RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
        descriptor.depthBufferBits = 0;

        var renderer = renderingData.cameraData.renderer;
        source = renderer.cameraColorTarget;

        // Create a temporary render texture using the descriptor from above.
        cmd.GetTemporaryRT(temporaryRTIdA, descriptor, FilterMode.Bilinear);
        destinationA = new RenderTargetIdentifier(temporaryRTIdA);
        cmd.GetTemporaryRT(temporaryRTIdB, descriptor, FilterMode.Bilinear);
        destinationB = new RenderTargetIdentifier(temporaryRTIdB);
    }

    // The actual execution of the pass. This is where custom rendering occurs.
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        // Skipping post processing rendering inside the scene View
        if (renderingData.cameraData.isSceneViewCamera)
            return;

        // Here you get your materials from your custom class
        // (It's up to you! But here is how I did it)
        var materials = CustomMaterialManager.instance;
        if (materials == null)
        {
            return;
        }

        CommandBuffer cmd = CommandBufferPool.Get("Custom Post Processing");
        cmd.Clear();

        // This holds all the current Volumes information
        // which we will need later
        var stack = VolumeManager.instance.stack;

        #region Local Methods

        // Swaps render destinations back and forth, so that
        // we can have multiple passes and similar with only a few textures
        void BlitTo(Material mat, int pass = 0)
        {
            var first = latestDest;
            var last = first == destinationA ? destinationB : destinationA;
            Blit(cmd, first, last, mat, pass);

            latestDest = last;
        }

        #endregion

        // Starts with the camera source
        latestDest = source;

        //---Custom effect here---
        var glitchEffect = stack.GetComponent<GlitchComponent>();
        if (glitchEffect.IsActive())
        {
            var customMaterial = materials.GetCustomEffectMaterials()[0];
            customMaterial.material.SetFloat(customMaterial.propertyIds["_Intensity"], glitchEffect.intensity.value);
            customMaterial.material.SetColor(customMaterial.propertyIds["_OverlayColor"], glitchEffect.overlayColor.value);

            BlitTo(customMaterial.material);
        }

        //---Custom effect here---
        var wrongEffect = stack.GetComponent<WrongComponent>();
        if (wrongEffect.IsActive())
        {
            var customMaterial = materials.GetCustomEffectMaterials()[1];
            customMaterial.material.SetFloat(customMaterial.propertyIds["_Intensity"], wrongEffect.intensity.value);
            customMaterial.material.SetColor(customMaterial.propertyIds["_OverlayColor"], wrongEffect.overlayColor.value);

            BlitTo(customMaterial.material);
        }

        //---Custom effect here---
        var speedEffect = stack.GetComponent<SpeedComponent>();
        if (speedEffect.IsActive())
        {
            var customMaterial = materials.GetCustomEffectMaterials()[2];
            customMaterial.material.SetFloat(customMaterial.propertyIds["_Intensity"], speedEffect.intensity.value);
            customMaterial.material.SetColor(customMaterial.propertyIds["_OverlayColor"], speedEffect.overlayColor.value);

            BlitTo(customMaterial.material);
        }


        // DONE! Now that we have processed all our custom effects, applies the final result to camera
        Blit(cmd, latestDest, source);

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }

    //Cleans the temporary RTs when we don't need them anymore
    public override void OnCameraCleanup(CommandBuffer cmd)
    {
        cmd.ReleaseTemporaryRT(temporaryRTIdA);
        cmd.ReleaseTemporaryRT(temporaryRTIdB);
    }
}