using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public delegate void HandlerTimer();
    public event HandlerTimer TimeUp;
    // remain in diapason of 0f to 1f
    public delegate void HandleGoing(float remain);
    public event HandleGoing TimeGoing;

    bool isGoing = false;

    public float time { private set; get; }
    public float Duration { private set; get; }
    public float progress { private set; get; }

    public void StartTimer(float time)
    {
        this.time = time;
        Duration = time;
        isGoing = true;
    }

    void FixedUpdate()
    {
        if (isGoing)
        {
            time -= Time.deltaTime;

            TimeGoing?.Invoke(time / Duration);

            if (time <= 0)
            {
                TimeUp?.Invoke();
                Destroy(this);
            }
        }
    }
}
