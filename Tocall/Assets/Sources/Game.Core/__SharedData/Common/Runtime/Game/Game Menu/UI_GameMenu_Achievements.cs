using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UnityEngine.UI.Button))]
public class UI_GameMenu_Achievements : MonoBehaviour, IPointerClickHandler
{
    public static System.Action OnClick;
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log(name);
        OnClick?.Invoke();
    }
}
