using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUpping : DoorObject
{
    [SerializeField]
    float height;
    float basicY;
    float upY => basicY + height;

    [SerializeField]
    float speed;

    private void Awake()
    {
        basicY = transform.position.y;
    }

    protected override bool openning(float dt)
    {
        bool isFinished = false;

        Vector3 newPos = transform.position;
        newPos.y += dt * speed;
        if (newPos.y >= upY)
        {
            newPos.y = upY;
            isFinished = true;
        }
        transform.position = newPos;
        
        return isFinished;
    }

    protected override bool closing(float t)
    {
        bool isFinished = false;

        Vector3 newPos = transform.position;
        newPos.y -= t * speed;
        if (newPos.y <= basicY)
        {
            newPos.y = basicY;
            isFinished = true;
        }
        transform.position = newPos;

        return isFinished;
    }
}
