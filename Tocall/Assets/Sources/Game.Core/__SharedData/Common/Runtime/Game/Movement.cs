using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct VectorAnimation
{
    private float counter;
    [SerializeField, Range(0,2)] private int index;
    [SerializeField] private bool isRunning;
    [SerializeField] private float duration;
    [SerializeField] public Vector2 value;
    [SerializeField] private AnimationCurve curve_movement;
    [SerializeField] private float currentValue;

    public float x => value.x;
    public float y => value.y;

    public float Reset()
    {
        counter = .5f;
        Calculate();
        return 0;
    }

    public float Update(float currentPos)
    {

        if (isRunning)
        {
            Calculate();
            return currentValue;
        }
        else
        {
            return currentPos;
        }
        
    }

    private void Calculate()
    {
        counter += Time.deltaTime / duration;
        if (duration == 0 || counter > 2) counter = 0;
        currentValue = Mathf.Lerp(value.x, value.y, curve_movement.Evaluate(Mathf.PingPong(counter, 1)));
    }

    public void __DrawGizmo(in Transform transform)
    {
        if (!isRunning) return;
        Vector3 v_min = Vector3.zero;
        Vector3 v_max = Vector3.zero;
        v_min[index] = value.x;
        v_max[index] = value.y;
        Gizmos.DrawLine(transform.position + v_min, transform.position + v_max);
    }
}

public class Movement : MonoBehaviour
{
    private Vector3 pos_init = Vector3.zero;
    private Vector3 pos_movement = Vector3.zero;

    [Header("Vectors")]
    [Space]
    [SerializeField] private VectorAnimation vAnim_X;
    [SerializeField] private VectorAnimation vAnim_Y;
    [SerializeField] private VectorAnimation vAnim_Z;

    private void Awake()
    {
        pos_init = transform.position;
        pos_movement.x = vAnim_X.Reset();
        pos_movement.y = vAnim_Y.Reset();
        pos_movement.z = vAnim_Z.Reset();
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        UpdateMovement();

        transform.position = pos_init + pos_movement;
    }

    private void UpdateMovement()
    {
        pos_movement.x = vAnim_X.Update(pos_movement.x);
        pos_movement.y = vAnim_Y.Update(pos_movement.y);
        pos_movement.z = vAnim_Z.Update(pos_movement.z);
    }

    private void OnDrawGizmosSelected(){
        if (Application.isPlaying) return;


        UpdateMovement();

        Gizmos.color = Color.gray;
        Gizmos.DrawCube(transform.position + pos_movement, transform.localScale);

        //X
        Gizmos.color = Color.red;
        vAnim_X.__DrawGizmo(transform);

        //Y
        Gizmos.color = Color.green;
        vAnim_Y.__DrawGizmo(transform);

        //Z
        Gizmos.color = Color.blue;
        vAnim_Z.__DrawGizmo(transform);


        //// MIN POS
        //Gizmos.color = Color.gray;
        //Gizmos.DrawLine(transform.position, transform.position + new Vector3(vAnim_X.x, vAnim_Y.x, vAnim_Z.x));

        //// MAX POS
        //Gizmos.color = Color.white;
        //Gizmos.DrawLine(transform.position, transform.position + new Vector3(vAnim_X.y, vAnim_Y.y, vAnim_Z.y));

    }
}
