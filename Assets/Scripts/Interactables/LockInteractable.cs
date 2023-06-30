using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockInteractable : MonoBehaviour
{
    [SerializeField]
    private Interactable interact;
    [SerializeField]
    private LayerMask lockedLayer;

    public void Lock()
    {
        interact.gameObject.layer = lockedLayer;
    }

    public void Unlock()
    {
        interact.gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

}
