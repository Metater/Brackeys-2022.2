using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System;

[Serializable, PostProcess(typeof(OutlinePostProcess), PostProcessEvent.BeforeStack, "Custom/Outline")]

public sealed class OutlineBad : PostProcessEffectSettings
{
    [Range(1, 5), Tooltip("Outline thickness.")]
    public IntParameter thickness = new IntParameter { value = 2 };

    [Range(0, 5), Tooltip("Outline edge start.")]
    public FloatParameter edge = new FloatParameter { value = 0.1f };

    [Range(0, 1), Tooltip("Outline smoothness transittion on close objects.")]
    public FloatParameter transitionSmoothness = new FloatParameter { value = 0.2f };

    [Tooltip("Outline color.")]
    public ColorParameter color = new ColorParameter { value = Color.black };


}

public sealed class OutlinePostProcess : PostProcessEffectRenderer<OutlineBad>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Outline"));
        sheet.properties.SetInt("_Thickness", settings.thickness);
        sheet.properties.SetFloat("_TransitionSmoothness", settings.transitionSmoothness);
        sheet.properties.SetFloat("_Edge", settings.edge);
        sheet.properties.SetColor("_Color", settings.color);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}