using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OpennessType { Open, Closing, Close, Opening }

public class DoorObject : MonoBehaviour
{
    [SerializeField]
    protected OpennessType openness;

    public OpennessType Openness => openness;
    // return true, if finished move
    delegate bool doorMove(float dt);

    doorMove moving = dt => false;

    public delegate void ReactDoor();
    public event ReactDoor OnOpen;
    public event ReactDoor OnClose;
    public event ReactDoor OnOpening;
    public event ReactDoor OnClosing;

    // OnOpeningClosed is called only when door is opening from closed state
    public event ReactDoor OnOpeningClosed;
    // OnClosingOpened is called only when door is closing from opened state
    public event ReactDoor OnClosingOpened;

    virtual protected bool opening(float dt) => true;
    virtual protected bool closing(float dt) => true;
    virtual protected bool motionless(float dt) => false;

    [SerializeField]
    bool isLock;
    public bool IsLock { get => isLock; private set => isLock = value; }

    public void Open()
    {
        if (!IsLock)
        {
            bool wasClosed = ((openness == OpennessType.Close) || (openness == OpennessType.Closing));

            openness = OpennessType.Opening;
            moving = opening;
            
            OnOpening?.Invoke();
            if (wasClosed) OnOpeningClosed?.Invoke();
        }
    }

    public void Close()
    {
        if (!IsLock)
        {
            bool wasOpen = ((openness == OpennessType.Open) || (openness == OpennessType.Opening));

            openness = OpennessType.Closing;
            moving = closing;

            OnClosing?.Invoke();
            if (wasOpen) OnClosingOpened?.Invoke();
        }
    }

    public void Switch()
    {
        if (!IsLock)
        switch (openness)
        {
            case OpennessType.Opening:
                Close();
                break;
            case OpennessType.Closing:
                Open();
                break;
            case OpennessType.Open:
                Close();
                break;
            case OpennessType.Close:
                Open();
                break;
            default:
                break;
        }
    }

    public void Lock() => IsLock = true;
    public void Unlock() => IsLock = false;

    private void FixedUpdate()
    {
        float dt = Time.deltaTime;
        if (moving(dt))
        {
            if (openness == OpennessType.Opening)
            {
                openness = OpennessType.Open;
                OnOpen?.Invoke();
            }
            else if (openness == OpennessType.Closing)
            {
                openness = OpennessType.Close;
                OnClose?.Invoke();
            }
            // if OnClose or OnOpen called Close or Open again
            if ((openness == OpennessType.Open) || (openness == OpennessType.Close))
                moving = motionless;
        }
    }
}
