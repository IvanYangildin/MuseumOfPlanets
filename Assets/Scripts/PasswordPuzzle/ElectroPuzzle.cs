using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArrowType { Left, Right, Front, Back, Noone};

public class ElectroPuzzle : PasswordSequential<ArrowType>
{
    [SerializeField]
    SatelliteRoom finishRoom;
    public SatelliteRoom FinishRoom => finishRoom;

    public bool IsFail { private set; get; } = false;

    public override bool IsFinished => base.IsFinished || IsFail;
    public override bool IsSolved => base.IsSolved && !IsFail;

    private void tryAgain() => IsFail = false;

    protected override void processFailure(ArrowType symbol)
    {
        IsFail = true;
        if ((symbol == ArrowType.Back) || (symbol == ArrowType.Noone))
        {
            ResetCombination();
            tryAgain();
        }
    }
}
