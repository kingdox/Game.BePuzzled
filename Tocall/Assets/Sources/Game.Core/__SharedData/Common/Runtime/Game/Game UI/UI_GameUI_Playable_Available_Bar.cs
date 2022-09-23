using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class UI_GameUI_Playable_Available_Bar : MonoBehaviour
{
    [SerializeField] private Image img_container = default;
    [SerializeField] private Image img = default;
    
    private void OnEnable() => Subscribe(true);
    private void OnDisable() => Subscribe(false);
    private void Subscribe(bool condition) => GameManager.OnCurrentPlayables.Subscribe(condition, OnCurrentPlayables);
    private void OnCurrentPlayables(Vector2Int v2)
    {
        img.enabled = !v2.y.Equals(0);
        img_container.enabled = img.enabled;
        if (v2.x + v2.y != 0)
        {
            img.fillAmount = (float)v2.x / (float)v2.y;
        }
        else
        {
            img.fillAmount = 0;
        }
    }
}
