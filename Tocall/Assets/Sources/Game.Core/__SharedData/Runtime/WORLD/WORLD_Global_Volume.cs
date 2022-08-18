using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.Rendering;

[RequireComponent(typeof(Volume))]
public class WORLD_Global_Volume : MonoBehaviour
{
    private Volume volume;
    private Outline c_outline;
    //
    public static Color color_Outline;
    public static Color color_Fill;
    //
    public static ActionSuscriber<Color> OnChangedColor_Outline = new ActionSuscriber<Color>();
    public static ActionSuscriber<Color> OnChangedColor_Fill = new ActionSuscriber<Color>();

    private void Awake()
    {
        volume = GetComponent<Volume>();
        if (volume.isGlobal)
        {
            c_outline = volume.sharedProfile.components.Find(c => c is Outline) as Outline;
            color_Outline = c_outline.outlineColor.value;
            color_Fill = c_outline.overwriteColor.value;

            RefreshOutline();
        }
        else
        {
            Debug.LogError($"{name} NO ES VOLUME GLOBAL");
            Destroy(gameObject);
        }
    }

    private void Start() => RefreshOutline();

    private void RefreshOutline()
    {
        OnChangedColor_Outline?.Invoke(c_outline.outlineColor.value);
        OnChangedColor_Fill?.Invoke(c_outline.overwriteColor.value);
    }
}
