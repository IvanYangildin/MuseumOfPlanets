using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractTrigger : Interactable
{
    public UnityEvent OnInteract;
    public UnityEvent OnOutInteract;

    protected override void interact(PlayerInteract playerInteract)
    {
        OnInteract.Invoke();
    }

    protected override void outInteract()
    {
        OnOutInteract.Invoke();
    }
}
