using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakenItemHolder : TakenObject
{
    [SerializeField]
    // negative value - infinite items to take is avaliable
    int itemLimit = -1;
    [HideInInspector]
    public List<TakenObject> HoldingItems;

    public bool IsFull => (itemLimit >= 0) && (HoldingItems.Count >= itemLimit);

    public override TakenObject ItemToDrop
    {
        get
        {
            if (HoldingItems.Count == 0)
            {
                return this;
            }
            else
            {
                return HoldingItems[HoldingItems.Count - 1];
            }
        }
    }
}