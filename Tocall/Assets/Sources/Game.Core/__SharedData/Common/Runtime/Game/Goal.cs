using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Goal : MonoBehaviour
{
    private static int index = 0;
    [SerializeField] private int displayIndex = 0;

    public static Action<Goal> OnCreated;
    public static Action<(Goal,Collision)> OnCollision;
    public static Action<(Goal, Collider)> OnTrigger;
    public static Action<Goal> OnDestroyGoal;

    private void Start()
    {
        displayIndex = index++;
        name = $"[{displayIndex}] {nameof(Goal)}";
        OnCreated?.Invoke(this);
    }

//#if UNITY_EDITOR
//    private void OnDrawGizmos()
//    {
//        if (! Application.isPlaying) name = $"{nameof(Goal)}";
//    }
//#endif
    private void OnDestroy() => OnDestroyGoal?.Invoke(this);
    private void OnCollisionEnter(Collision collision) => OnCollision?.Invoke((this, collision));
    private void OnTriggerEnter(Collider other) => OnTrigger?.Invoke((this, other));
}
