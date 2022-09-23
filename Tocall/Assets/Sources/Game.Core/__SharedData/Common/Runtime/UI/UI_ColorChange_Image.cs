using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UI_ColorChange_Image : MonoBehaviour
{
    private Image value;

    [SerializeField] private bool isColorFill = default;

    private void Awake() => value = GetComponent<Image>();

    private void OnEnable() => this.Subscribe(true);

    private void OnDisable() => this.Subscribe(false);

    private void Subscribe(bool condition)
    {
        if (isColorFill)
        {
            WORLD_Global_Volume.OnChangedColor_Fill.Subscribe(condition, OnColorChange_Fill);
        }
        else
        {
            WORLD_Global_Volume.OnChangedColor_Outline.Subscribe(condition, OnColorChange_Outline);
        }
    }

    private void OnColorChange_Fill(Color c_fill) => value.color = c_fill;

    private void OnColorChange_Outline(Color c_oultine) => value.color = c_oultine;

    [ContextMenu("__RefreshColor_Outline")]
    private void __RefreshColor_Outline() => OnColorChange_Outline(WORLD_Global_Volume.color_Outline);

    [ContextMenu("__RefreshColor_Fill")]
    private void __RefreshColor_Fill() => OnColorChange_Fill(WORLD_Global_Volume.color_Fill);
}
