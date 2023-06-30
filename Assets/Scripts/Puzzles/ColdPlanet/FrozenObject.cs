using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenObject : MonoBehaviour
{
    Rigidbody body;
    Collider selfCollider;
    [SerializeField]
    Collider iceCollider;
    [SerializeField]
    Interactable interactable;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        if (body != null) body.isKinematic = true;

        selfCollider = GetComponent<Collider>();
        
        interactable = GetComponent<Interactable>();
        if (interactable != null) interactable.enabled = false;
    }

    private void FixedUpdate()
    {
        if (!iceCollider.bounds.Intersects(selfCollider.bounds))
        {
            if (body != null) body.isKinematic = false;
            if (interactable != null) interactable.enabled = true;
            Destroy(this);
        }
    }
}
