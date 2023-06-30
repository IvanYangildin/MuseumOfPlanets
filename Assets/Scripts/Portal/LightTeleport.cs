using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LightTeleport : MonoBehaviour
{
    Collider zone;
    Collider lightOwner = null;
    Light lightClone = null;

    [SerializeField]
    Portal portal;

    private void Awake()
    {
        zone = GetComponent<Collider>();
    }

    public void TryLight(Collider other)
    {
        if (other == null) return;

        Light light = other.GetComponentInChildren<Light>();
        if (light != null)
        {
            ClearLight();

            lightOwner = other;
            lightClone = Instantiate(light);

            lightClone.transform.SetParent(portal.Other.PortalCamera.transform);
            lightClone.transform.localPosition =
                Camera.main.transform.worldToLocalMatrix.MultiplyPoint3x4(light.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TryLight(other);
    }

    public void ClearLight()
    {
        if (lightClone != null)
        {
            Destroy(lightClone.gameObject);
            lightOwner = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ClearLight();
    }

    private void Update()
    {
        if ((lightOwner != null) && !lightOwner.bounds.Intersects(zone.bounds))
        {
            ClearLight();
        }
    }
}
