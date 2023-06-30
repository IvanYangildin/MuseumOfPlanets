using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public delegate void HandlerTimer();
    public event HandlerTimer TimeUp;

    public float time { private set; get; }

    public void StartTimer(float time)
    {
        this.time = time;
    }

    void FixedUpdate()
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            TimeUp.Invoke();
            Destroy(this);
        }
    }
}
