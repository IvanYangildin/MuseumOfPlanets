using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDoorway : MonoBehaviour
{
    [SerializeField]
    DoorObject door;
    public DoorObject Door => door;

    [SerializeField]
    TriggerZone trigger;
    public TriggerZone Trigger => trigger;
    
    [SerializeField]
    Vector3 portalLocalPos;

    [SerializeField]
    SatelliteRoom nextRoom;
    public SatelliteRoom NextRoom => nextRoom;

    [SerializeField]
    ArrowType doorType;
    public ArrowType DoorType => doorType;


    public void SummonPortal(Portal portal)
    {
        portal.transform.parent = transform;
        portal.transform.localPosition = portalLocalPos;
        portal.transform.localEulerAngles = Vector3.zero;
    }
}
