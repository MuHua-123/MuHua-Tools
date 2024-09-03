using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ROOutlineBlend : ScriptableRendererFeature {
    public RenderPassEvent renderPassEvent;
    private ROOutlineBlendRenderPass OutlineBlendRenderPass;
    public override void Create() {
        OutlineBlendRenderPass = new ROOutlineBlendRenderPass();
        OutlineBlendRenderPass.renderPassEvent = renderPassEvent;
    }
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) {
        OutlineBlendRenderPass.Setup();
        renderer.EnqueuePass(OutlineBlendRenderPass);
        Dispose();
    }
}
public class ROOutlineBlendRenderPass : ScriptableRenderPass {
    public const string ProfilerTag = "OutlineBlend";
    public const string OutlineBlend_Shader = "Shader Graphs/OutlineBlend";

    public Material outlineMaterial;
    public ROOutlineBlendRenderPass() {
        Shader outlineBlend = Shader.Find(OutlineBlend_Shader);
        outlineMaterial = CoreUtils.CreateEngineMaterial(outlineBlend);
    }
    public void Setup() {
        outlineMaterial.SetColor("_Color", ROModel.I.OutlineColor);
        outlineMaterial.SetTexture("_MainTex", ROModel.I.OutlineRT);
    }
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData) {
        CommandBuffer cmd = CommandBufferPool.Get(ProfilerTag);
        Blit(cmd, ref renderingData, outlineMaterial);
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
}