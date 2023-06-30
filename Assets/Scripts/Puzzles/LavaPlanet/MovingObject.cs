using UnityEngine;
using UnityEngine.Events;

public class MovingObject : MonoBehaviour
{
    Vector3 fromPoint;
    Vector3 step;
    float maxLength;
    float currLength => (transform.position - fromPoint).magnitude;

    public UnityEvent OnStartMove = new UnityEvent();
    public UnityEvent OnEndMove = new UnityEvent();

    protected virtual void startMove()
    {
        transform.position = fromPoint;
        movingStyle = move;

        GetComponent<Rigidbody>().isKinematic = true;

        OnStartMove?.Invoke();
    }

    protected virtual void endMove()
    {
        movingStyle = still;
        
        OnEndMove?.Invoke();
    }

    public void StartMove(Vector3 from, Vector3 to, float speed)
    {
        fromPoint = from;
        maxLength = (to - from).magnitude;
        step = (to - from).normalized * speed;

        startMove();
    }

    delegate bool MovingMod(float dt);
    MovingMod movingStyle;

    protected virtual bool move(float dt)
    {
        bool isFinished = false;

        transform.position = fromPoint + (currLength + dt) * step;
        if (currLength >= maxLength)
        {
            transform.position = fromPoint + maxLength * step;
            isFinished = true;
        }

        return isFinished;
    }

    protected virtual bool still(float dt) => false;

    private void FixedUpdate()
    {
        if (movingStyle(Time.deltaTime))
        {
            endMove();
        }
    }
}