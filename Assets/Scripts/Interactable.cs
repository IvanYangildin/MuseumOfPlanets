using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    protected string promtMessage;
    public virtual string PromtMessage(PlayerInteract playerInteract) => promtMessage;

    protected virtual void lookInteract(PlayerInteract playerInteract)
    {
        playerInteract.PlayerUI.UpdateText(PromtMessage(playerInteract));
    }
    public void LookInteract(PlayerInteract playerInteract) => lookInteract(playerInteract);

    protected virtual void interact(PlayerInteract playerInteract) { }
    public void Interact(PlayerInteract playerInteract) => interact(playerInteract);

    protected virtual void outInteract() { }
    public void OutInteract(PlayerInteract playerInteract)
    {
        outInteract();
        playerInteract.CancelInteract -= OutInteract;
    }
}
