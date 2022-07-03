using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
[CreateAssetMenu(menuName = "自定义管线/CreateCustomRenderPipline")]
public class CustomRenderPipeAsset : RenderPipelineAsset
{
    protected override RenderPipeline CreatePipeline()
    {
        //throw new System.NotImplementedException();
        //新建一个 CustomRenderPipeline 实例并返回
        return new CustomRenderPipeline();
    }
}
