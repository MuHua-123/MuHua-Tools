using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ROOutlineRendererFeature : ScriptableRendererFeature {
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

    public RTHandle TempRTHandel;
    public Material UnlitMaterial;
    public Material OutlineMaterial;

    public ROOutlineRenderPass() {
        Shader unlit = Shader.Find(Unlit_Shader);
        UnlitMaterial = CoreUtils.CreateEngineMaterial(unlit);
        Shader outline = Shader.Find(Outline_Shader);
        OutlineMaterial = CoreUtils.CreateEngineMaterial(outline);
    }
    public void Setup(in RenderingData renderingData) {
        OutlineMaterial.SetFloat("_Size", ROModel.I.OutlineSize);
        RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;
        descriptor.depthBufferBits = (int)DepthBits.None;
        RenderingUtils.ReAllocateIfNeeded(ref ROModel.I.OutlineRT, descriptor, name: "OutlineRT");
        RenderingUtils.ReAllocateIfNeeded(ref TempRTHandel, descriptor, name: "TempRT");
    }
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData) {
        CommandBuffer command = CommandBufferPool.Get(ProfilerTag);
        CoreUtils.SetRenderTarget(command, TempRTHandel);

        DrawRenderer(command, UnlitMaterial);

        OutlineMaterial.SetTexture("_MainTex", TempRTHandel);
        Blit(command, ref renderingData, OutlineMaterial);
        RTHandle source = renderingData.cameraData.renderer.cameraColorTargetHandle;
        Blitter.BlitCameraTexture(command, source, ROModel.I.OutlineRT);

        context.ExecuteCommandBuffer(command);
        CommandBufferPool.Release(command);
        TempRTHandel.Release();
    }
    public void DrawRenderer(CommandBuffer command, Material material) {
        ROModel.I.RenderObjs.RemoveAll(obj => obj == null);
        for (int i = 0; i < ROModel.I.RenderObjs.Count; i++) {
            Transform obj = ROModel.I.RenderObjs[i];
            DrawRenderer(obj, command, material);
        }
    }
    public void DrawRenderer(Transform obj, CommandBuffer command, Material material) {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++) {
            command.DrawRenderer(renderers[i], material, 0, 0);
        }
        if (obj.TryGetComponent(out Renderer renderer)) {
            command.DrawRenderer(renderer, material, 0, 0);
        }
    }
    public void Dispose() {
        if (ROModel.I.OutlineRT != null) { ROModel.I.OutlineRT?.Release(); }
    }
}