using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PasswordSequential<T> : MonoBehaviour
{
    [SerializeField]
    protected List<T> trueCombination = new List<T>();
    protected List<T> currentCombination = new List<T>();

    protected int currentInput = 0;
    public int CurrentInput => currentInput;

    public virtual bool IsFinished => currentCombination.Count >= trueCombination.Count;
    public virtual bool IsSolved => trueCombination.SequenceEqual(currentCombination);

    protected virtual void processFailure(T symbol) { ResetCombination(); }
    protected virtual void processReset() { }

    public void ResetCombination()
    {
        processReset();
        currentCombination.Clear();
    }

    public bool Enter(T symbol)
    {
        bool finish = IsFinished;

        currentCombination.Add(symbol);
        if (finish)
        {
            currentCombination.RemoveAt(0);
            if (!IsSolved)
            {
                processFailure(symbol);
                return false;
            }
            else
                return true;
        }
        else if (Equals(symbol, trueCombination[currentCombination.Count - 1]))
        {
            return IsSolved;
        }

        processFailure(symbol);
        return false;
    }
}
