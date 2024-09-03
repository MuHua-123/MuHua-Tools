using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ROOutline : ScriptableRendererFeature {
    private ROOutlineRenderPass OutlineRenderPass;
    public override void Create() {
        OutlineRenderPass = new ROOutlineRenderPass();
        OutlineRenderPass.renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
    }
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) {
        OutlineRenderPass.Setup(renderingData);
        renderer.EnqueuePass(OutlineRenderPass);
    }
    protected override void Dispose(bool disposing) {
        OutlineRenderPass.Dispose();
    }
}
public class ROOutlineRenderPass : ScriptableRenderPass {
    public const string ProfilerTag = "Outline";
    public const string Unlit_Shader = "Universal Render Pipeline/Unlit";
    public const string Outline_Shader = "Shader Graphs/Outline";

    public RTHandle tempRT;
    public Material unlitMaterial;
    public Material outlineMaterial;
    public ROOutlineRenderPass() {
        Shader unlit = Shader.Find(Unlit_Shader);
        unlitMaterial = CoreUtils.CreateEngineMaterial(unlit);
        Shader outline = Shader.Find(Outline_Shader);
        outlineMaterial = CoreUtils.CreateEngineMaterial(outline);
    }
    public void Setup(in RenderingData renderingData) {
        outlineMaterial.SetFloat("_Size", ROModel.I.OutlineSize);
        RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
        descriptor.depthBufferBits = (int)DepthBits.None;
        RenderingUtils.ReAllocateIfNeeded(ref ROModel.I.OutlineRT, descriptor, name: "OutlineRT");
        RenderingUtils.ReAllocateIfNeeded(ref tempRT, descriptor, name: "TempRT");
    }
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData) {
        CommandBuffer cmd = CommandBufferPool.Get(ProfilerTag);
        CoreUtils.SetRenderTarget(cmd, tempRT);

        //DrawRenderer(cmd, unlitMaterial);

        outlineMaterial.SetTexture("_MainTex", tempRT);
        Blit(cmd, ref renderingData, outlineMaterial);
        RTHandle source = renderingData.cameraData.renderer.cameraColorTargetHandle;
        Blitter.BlitCameraTexture(cmd, source, ROModel.I.OutlineRT);

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
        tempRT.Release();
    }
    public void Dispose() {
        if (ROModel.I.OutlineRT != null) { ROModel.I.OutlineRT?.Release(); }
    }
}