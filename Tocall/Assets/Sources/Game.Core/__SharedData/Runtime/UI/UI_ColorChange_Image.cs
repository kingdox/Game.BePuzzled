using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UI_ColorChange_Image : MonoBehaviour
{
    private Image value;

    private void Awake() => value = GetComponent<Image>();

    private void OnEnable() => this.Subscribe(true);

    private void OnDisable() => this.Subscribe(false);

    private void Subscribe(bool condition) => condition.Subscribe(ref WORLD_Global_Volume.OnChangeColor_Outline, OnColorChange_Outline);

    private void OnColorChange_Outline(Color c_oultine) => value.color = c_oultine;

    [ContextMenu("Refresh Color")]
    private void __RefreshColor() => OnColorChange_Outline(WORLD_Global_Volume.color_Outline);
}
