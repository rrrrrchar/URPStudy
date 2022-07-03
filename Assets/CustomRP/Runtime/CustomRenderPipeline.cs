﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class CustomRenderPipeline : RenderPipeline
{
    CameraRenderer renderer=new CameraRenderer();

    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        //throw new System.NotImplementedException();

        foreach (Camera camera in cameras)
        {
            renderer.Render(context, camera);
        }
    }


    
}
