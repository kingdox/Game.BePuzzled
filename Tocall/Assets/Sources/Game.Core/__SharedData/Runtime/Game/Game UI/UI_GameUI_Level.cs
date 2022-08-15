using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Text))]
public class UI_GameUI_Level : MonoBehaviour
{
    private void Start() => this.GetComponent<Text>().text = gameObject.scene.buildIndex.ToString();
}
