using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Goal : MonoBehaviour
{
    private static int index = 0;
    [SerializeField] private int displayIndex = 0;

    public static Action<Goal> OnCreated;
    public static Action<(Goal,Collision)> OnCollision;
    public static Action<Goal> OnDestroyGoal;

    private void Start()
    {
        displayIndex = index++;
        name = $"[{displayIndex}] {nameof(Goal)}";
        OnCreated?.Invoke(this);
    }

    private void OnDestroy() => OnDestroyGoal?.Invoke(this);
    private void OnCollisionEnter(Collision collision) => OnCollision?.Invoke((this, collision));
}
