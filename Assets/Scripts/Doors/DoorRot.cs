using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRot : DoorObject
{
    [SerializeField]
    float rotSpeed;

    [SerializeField]
    float minAngle, maxAngle;
    float angle = 0;

    [SerializeField]
    Vector3 pointOfRot;
    [SerializeField]
    Vector3 axisOfRot;

    Vector3 worldPoint;

    private void Awake()
    {
        worldPoint = transform.localToWorldMatrix.MultiplyPoint3x4(pointOfRot);
    }

    protected override bool openning(float dt)
    {
        bool isFinished = false;
        float prevAngle = angle;

        angle += dt * rotSpeed;
        if (angle >= maxAngle)
        {
            angle = maxAngle;
            isFinished = true;
        }

        float deltaAngle = angle - prevAngle;
        transform.RotateAround(worldPoint, axisOfRot, deltaAngle);

        return isFinished;
    }

    protected override bool closing(float dt)
    {
        bool isFinished = false;
        float prevAngle = angle;

        angle -= dt * rotSpeed;
        if (angle <= minAngle)
        {
            angle = minAngle;
            isFinished = true;
        }

        float deltaAngle = angle - prevAngle;
        transform.RotateAround(worldPoint, axisOfRot, deltaAngle);

        return isFinished;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(worldPoint - axisOfRot * 2, worldPoint + axisOfRot * 2);
        worldPoint = transform.localToWorldMatrix.MultiplyPoint3x4(pointOfRot);
    }
}
