using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LampTrigger : MonoBehaviour
{
    Collider innerCollider;
    [SerializeField]
    Collider lamp;
    [SerializeField]
    SunEnviroment enviroment;

    bool isUnderLamp = false;

    private void Awake()
    {
        innerCollider = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other != null) && (other.transform == lamp.transform))
        {
            enviroment.SetDay();
            isUnderLamp = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other != null) && (other.transform == lamp.transform))
        {
            enviroment.SetNight();
            isUnderLamp = false;
        }
    }

    private void Update()
    {
        if (isUnderLamp && !innerCollider.bounds.Intersects(lamp.bounds)) 
        { 
            enviroment.SetNight(); 
            isUnderLamp = false;
        }
    }
}
