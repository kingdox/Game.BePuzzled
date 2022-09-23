using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Text))]
public class UI_GameUI_Playable_Available_Text : MonoBehaviour
{
    private Text text = default;
    private void Awake() => text = this.GetComponent<Text>();
    private void OnEnable() => Subscribe(true);
    private void OnDisable() => Subscribe(false);
    private void Subscribe(bool condition) => GameManager.OnCurrentPlayables.Subscribe(condition, OnCurrentPlayables);
    private void OnCurrentPlayables(Vector2Int v2)
    {
        text.enabled = !v2.y.Equals(0);
        
        text.text = (v2.y - v2.x).ToString();
    }
}
