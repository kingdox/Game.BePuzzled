using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UI_ColorChange_TMP : MonoBehaviour
{
    private TextMeshProUGUI tmp;

    private void Awake() => tmp = GetComponent<TextMeshProUGUI>();

    private void OnEnable() => this.Subscribe(true);

    private void OnDisable() => this.Subscribe(false);

    private void Subscribe(bool condition) => WORLD_Global_Volume.OnChangedColor_Outline.Subscribe(condition, OnColorChange_Outline);

    private void OnColorChange_Outline(Color c_oultine) => tmp.color = c_oultine;

    [ContextMenu("Refresh Color")]
    private void __RefreshColor() => OnColorChange_Outline(WORLD_Global_Volume.color_Outline);
}
