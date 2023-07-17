using System;
using UnityEngine;

public abstract class Password<T> : MonoBehaviour
{
    public event Action<T> OnEnter;

    // true, when password contains mistakes
    public abstract bool IsMistake { get; }
    // true. when password is right
    public abstract bool IsSolved { get; }

    protected abstract bool enter(T symbol);

    // return true, if entered password is correct;
    // return false otherwise or when password isn't full
    public bool Enter(T symbol)
    {
        enter(symbol);
        OnEnter?.Invoke(symbol);
        return IsSolved;
    }
    
    public abstract void PasswordReset();
}