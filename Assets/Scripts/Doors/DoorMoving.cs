using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMoving : DoorObject
{
    [SerializeField]
    // in local coordinates
    Vector3 from, to;

    Vector3 worldFrom, worldTo;
    
    public Vector3 MoveDirection => (worldTo - worldFrom).normalized;
    public float MoveLength => (worldTo - worldFrom).magnitude;

    [SerializeField]
    float speed;

    public float ToProgress(Vector3 pos) => Vector3.Dot((pos - worldFrom), MoveDirection) / (worldTo - worldFrom).magnitude;
    public float Progress => ToProgress(transform.position);

    private void Awake()
    {
        worldFrom = transform.localToWorldMatrix.MultiplyPoint3x4(from);
        worldTo = transform.localToWorldMatrix.MultiplyPoint3x4(to);
        transform.position = worldFrom;
    }

    protected override bool opening(float dt)
    {
        bool isFinished = false;

        Vector3 newPos = transform.position;
        newPos += dt * speed * MoveDirection;
        if (ToProgress(newPos) >= 0.999f)
        {
            newPos = worldTo;
            isFinished = true;
        }
        transform.position = newPos;

        return isFinished;
    }

    protected override bool closing(float dt)
    {
        bool isFinished = false;

        Vector3 newPos = transform.position;
        newPos -= dt * speed * MoveDirection;
        if (ToProgress(newPos) <= 0.001f)
        {
            newPos = worldFrom;
            isFinished = true;
        }
        transform.position = newPos;

        return isFinished;
    }


    private void setProgressPos(float progress)
    {
        transform.position = worldFrom + progress * MoveLength * MoveDirection;
    }

    public void SetProgress(float progress)
    {
        progress = Mathf.Clamp(progress, 0f, 1f);
        setProgressPos(progress);

        if (progress == 0f)
        {
            openness = OpennessType.Open;
        }
        else if (progress == 1f)
        {
            openness = OpennessType.Close;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.localToWorldMatrix.MultiplyPoint3x4(from), 
            transform.localToWorldMatrix.MultiplyPoint3x4(to));
    }
}
