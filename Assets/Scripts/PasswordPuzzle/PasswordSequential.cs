using System;
using System.Collections.Generic;
using UnityEngine;

public class PasswordSequential<T> : MonoBehaviour
{
    [SerializeField]
    protected List<T> trueCombination = new List<T>();

    protected int currentInput = 0;
    public int CurrentInput => currentInput;

    public virtual bool IsFinished => CurrentInput >= trueCombination.Count;
    public virtual bool IsSolved => IsFinished;

    protected virtual void processFailure(T symbol) { ResetCombination(); }
    protected virtual void processReset() { }
    protected virtual void processOverabundance() { }

    public void ResetCombination()
    {
        processReset();
        currentInput = 0;
    }

    public bool Enter(T symbol)
    {
        if (IsFinished)
        {
            processOverabundance();
            return IsSolved;
        }
        else if (Equals(symbol, trueCombination[currentInput]))
        {
            currentInput++;
            return IsSolved;
        }

        processFailure(symbol);
        return false;
    }
}
