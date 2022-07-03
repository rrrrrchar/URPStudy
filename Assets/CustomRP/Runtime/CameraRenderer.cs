using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class CameraRenderer 
{
    ScriptableRenderContext context;
    Camera camera;

    //CommandBuffer
    const string bufferName = "Render Camera";
    CommandBuffer buffer = new CommandBuffer
    {
        name = bufferName
    };

    //存储剔除后的结果数据
    CullingResults cullingResults;
    public void Render(ScriptableRenderContext context,Camera camera)
    {
        this.context = context;
        this.camera = camera;
        
        
        if(!Cull())
        {
            return;
        }
        
        //设置mvp矩阵 matrixVP
        Setup();

        //绘制天空盒
        DrawVisibleGeometry();
        Submit();
    }


    static ShaderTagId unlitShaderTagId =new ShaderTagId("SRPDefaultUnlit");
    ///<summary>
    ///绘制几何体
    ///</summary>
    ///
    void DrawVisibleGeometry()
    {
        //设置绘制顺序和指定渲染相机
        var sortingSetting = new SortingSettings(camera)
        {
            criteria = SortingCriteria.CommonOpaque
        };
        //设置渲染的1ShaderPass 和排序顺序
        var drawingSettings = new DrawingSettings();
        //设置哪些类型的渲染队列可以被绘制
        var filteringSettings=new FilteringSettings();
        //图像绘制
        context.DrawRenderers(
            cullingResults, ref drawingSettings, ref filteringSettings);
        context.DrawSkybox(camera);
    }

    ///<summary>
    ///提交缓冲区渲染命令
    ///</summary>
    ///
    void Submit()
    {
        buffer.EndSample(bufferName);
        ExcuteBuffer();
        context.Submit();
    }

    ///<summary>
    ///设置相机的属性和矩阵
    ///</summary>
    ///
    void Setup()
    {
        context.SetupCameraProperties(camera);
        buffer.ClearRenderTarget(true, true, Color.clear);
        buffer.BeginSample(bufferName);
        ExcuteBuffer();
        
    }
    ///<summary>
    ///????
    ///</summary>
    ///
    void ExcuteBuffer()
    {
        context.ExecuteCommandBuffer(buffer);
        buffer.Clear();
    }

    ///<summary>
    ///剔除
    ///</summary>
    ///
    bool Cull()
    {
        ScriptableCullingParameters p;
        if(camera.TryGetCullingParameters(out p))
        {
            cullingResults=context.Cull(ref p);
            return true;
        }
        return false;
    }
}
