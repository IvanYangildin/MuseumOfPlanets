using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeltTrigger : PutPlace
{
    [SerializeField]
    KeyItem melterTest;
    [SerializeField]
    IceCube ice;

    Collider trigger;
    List<Collider> melters = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        KeyItem melter = other.GetComponent<KeyItem>();
        if (melter != null)
        {
            if (melterTest.IsRightKey(melter))
            {
                melters.Add(other);
            }
        }
    }

    private void Awake()
    {
        trigger = GetComponent<Collider>();
    }

    private void Update()
    {
        List<Collider> updated = new List<Collider>();
        foreach (var melter in melters)
        {
            if (melter.bounds.Intersects(trigger.bounds)) updated.Add(melter);
        }
        melters = updated;

        ice.IsMelting = melters.Count > 0;
    }
}
