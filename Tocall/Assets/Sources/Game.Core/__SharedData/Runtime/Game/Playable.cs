using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Playable : MonoBehaviour
{
    private static int index = 0;
    [SerializeField] private int displayIndex = 0;

    public static Action<Playable> OnCreated;
    public static Action<Playable> OnMouseClick;

    private void Start()
    {
        displayIndex = index++;
        name = $"[{displayIndex}] {nameof(Playable)}";
        OnCreated?.Invoke(this);
    }

//#if UNITY_EDITOR
//    private void OnDrawGizmos()
//    {
//        if (! Application.isPlaying) name = $"{nameof(Playable)}";
//    }
//#endif

    private void OnMouseDown() => OnMouseClick?.Invoke(this);
    private void Update() => enabled = transform.position.y > -1000;
}
