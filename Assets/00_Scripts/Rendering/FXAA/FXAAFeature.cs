using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FXAAFeature : ScriptableRendererFeature
{
    class FXAAPass : ScriptableRenderPass
    {
        static readonly string ProfilerTag = "FXAA Pass";
        static readonly int TempTargetId = Shader.PropertyToID("_TempTargetFXAA");

        Material fxaaMaterial;

        public FXAAPass(Material material)
        {
            fxaaMaterial = material;
            renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
        }

        [Obsolete("Obsolete")]
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (fxaaMaterial == null) return;

            CommandBuffer cmd = CommandBufferPool.Get(ProfilerTag);
            RenderTargetIdentifier source = renderingData.cameraData.renderer.cameraColorTarget;

            RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
            cmd.GetTemporaryRT(TempTargetId, descriptor);
            cmd.Blit(source, TempTargetId, fxaaMaterial);
            cmd.Blit(TempTargetId, source);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }

    [System.Serializable]
    public class FXAASettings
    {
        public Material FXAAMaterial = null;
    }

    public FXAASettings settings = new FXAASettings();
    FXAAPass fxaaPass;

    public override void Create()
    {
        if (settings.FXAAMaterial == null)
        {
            Debug.LogError("FXAA Material is missing!");
            return;
        }
        fxaaPass = new FXAAPass(settings.FXAAMaterial);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (fxaaPass != null)
        {
            renderer.EnqueuePass(fxaaPass);
        }
    }
}
