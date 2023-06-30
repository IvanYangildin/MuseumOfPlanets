using UnityEngine;

public class PortalPlace : MonoBehaviour
{
    [SerializeField]
    Vector3 portalLocalPos;

    public void SummonPortal(Portal portal)
    {
        portal.transform.parent = transform;
        portal.transform.localPosition = portalLocalPos;
        portal.transform.localEulerAngles = Vector3.zero;
    }
}
