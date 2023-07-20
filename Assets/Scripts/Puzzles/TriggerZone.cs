using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public delegate void HandleTrigger(Collider enterer);
    public event HandleTrigger OnEnter;
    public event HandleTrigger OnExit;
    // this event invoke when object inside zone is being destroyed
    public event HandleTrigger OnVanished;
    public event HandleTrigger OnStay;

    List<Collider> entered = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if (OnEnter != null)
            OnEnter(other);
        entered.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (OnExit != null)
            OnExit(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (OnStay != null)
            OnStay(other);
    }

    Collider trigger;
    private void Awake()
    {
        trigger = GetComponent<Collider>();
    }

    private void Update()
    {
        List<Collider> updated = new List<Collider>();
        foreach (var obj in entered)
        {
            if ((obj != null) && (obj.bounds.Intersects(trigger.bounds))) updated.Add(obj);
            else
            {
                if (obj == null) OnVanished?.Invoke(obj);
                else OnExit?.Invoke(obj);
            }
        }
        entered = updated;
    }
}
