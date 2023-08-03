using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaCoat : TakenObject
{
    [SerializeField]
    KeyItem tester;

    [SerializeField]
    string DenyMessage;

    public override string PromtMessage(PlayerInteract playerInteract)
    {
        PlayerHoldItem holder = playerInteract.gameObject.GetComponent<PlayerHoldItem>();

        if ((holder != null) && CanTake(holder))
            return promtMessage;
        else
            return DenyMessage;

    }

    public override bool CanTake(PlayerHoldItem playerHold)
    {
        KeyItem key = playerHold.Item?.GetComponent<KeyItem>();
        TakenItemHolder holder = playerHold.Item?.GetComponent<TakenItemHolder>();
        if ((key != null) && (holder != null))
        {
            return tester.IsRightKey(key) && !holder.IsFull;
        }
        return false;
    }

    protected override void take(PlayerHoldItem playerHold)
    {
        playerHold.Item?.GetComponent<TakenItemHolder>().HoldingItems.Add(this);
    }

    protected override void drop()
    {
        TakenItemHolder holder = currentHolder.Item?.GetComponent<TakenItemHolder>();
        holder.HoldingItems.Remove(this);
    }

    public override string DroppedText(PlayerHoldItem playerHold)
    {
        return playerHold.StandardDropText;
    }

}
