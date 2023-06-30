using UnityEngine;

public class SatelliteTaker : Interactable
{
    [SerializeField]
    GameObject satellite;
    [SerializeField]
    Vector3 satellitePos;

    [SerializeField]
    string putSatelliteLine;
    public override string PromtMessage(PlayerInteract playerInteract)
    {
        PlayerHoldItem playerHold = playerInteract.gameObject.GetComponent<PlayerHoldItem>();

        if (playerHold != null)
            return (playerHold.ItemGameObject == satellite) ? putSatelliteLine : promtMessage;
        else return promtMessage;
    }

    public bool IsSatelliteAround { private set; get; } = false;

    protected override void interact(PlayerInteract playerInteract)
    {
        PlayerHoldItem playerHold = playerInteract.gameObject.GetComponent<PlayerHoldItem>();
        if ((playerHold != null) && (playerHold.ItemGameObject == satellite))
        {
            playerHold.Item.Drop();
            Destroy(satellite.GetComponent<TakenObject>());
            
            Rigidbody satBody = satellite.GetComponent<Rigidbody>();
            if (satBody != null) Destroy(satBody);

            satellite.transform.parent = transform;
            satellite.transform.localPosition = satellitePos;

            IsSatelliteAround = true;
        }
    }
}
