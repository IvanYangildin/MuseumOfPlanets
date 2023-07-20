using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FullnessType { Empty, Filling, Full, Emptying }

public class FuelContainer : Interactable
{
    [SerializeField]
    float speed;
    [SerializeField]
    Vector3 fuelStart, fuelEnd;
    [SerializeField]
    DoorObject lid;

    Vector3 worldStart => transform.localToWorldMatrix.MultiplyPoint3x4(fuelStart);
    Vector3 worldEnd => transform.localToWorldMatrix.MultiplyPoint3x4(fuelEnd);

    FullnessType fullness;
    public FullnessType Fullness => fullness;

    public Fuel fuel;
    public bool IsUsing;

    protected void tofill(Fuel fuel)
    {
        if (fuel != null)
        {
            this.fuel = fuel;
            TakenObject taken = fuel.GetComponent<TakenObject>();
            
            taken.Drop();
            taken.gameObject.layer = LayerMask.NameToLayer("InteractIgnore");

            MovingObject moving = fuel.gameObject.AddComponent<MovingObject>();
            fuel.transform.rotation = Quaternion.identity;
            moving.StartMove(worldStart, worldEnd, speed);

            fullness = FullnessType.Filling;
            moving.OnEndMove.AddListener(() => 
            { 
                fullness = FullnessType.Full;
                Destroy(moving);
            });
        }
    }

    protected void toempty(PlayerHoldItem holder)
    {
        TakenObject taken = fuel.GetComponent<TakenObject> ();

        if (holder.Item == null)
        {
            taken.Take(holder);
            fullness = FullnessType.Empty;
            fuel = null;
        }
    }

    public void BurnFuel()
    {
        if (fullness == FullnessType.Full)
        {
            Destroy(fuel.gameObject);
            fullness = FullnessType.Empty;
            fuel = null;
        }
    }

    protected override void interact(PlayerInteract playerInteract)
    {
        PlayerHoldItem holder = playerInteract.GetComponent<PlayerHoldItem>();

        if ((holder != null) && (lid.Openness == OpennessType.Open))
        {
            if (Fullness == FullnessType.Empty)
            {
                Fuel fuel = holder.Item?.GetComponent<Fuel>();
                tofill(fuel);
            }
            if (Fullness == FullnessType.Full)
            {
                toempty(holder);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(worldEnd, worldStart);
    }

    [SerializeField]
    string strTakeFuel, strPutFuel, strNeedFuel, strOpenLid;

    public override string PromtMessage(PlayerInteract playerInteract)
    {
        if (lid.Openness == OpennessType.Open)
        {
            if (Fullness == FullnessType.Empty)
            {
                PlayerHoldItem holder = playerInteract.GetComponent<PlayerHoldItem>();
                return ((holder != null) && (holder.Item?.GetComponent<Fuel>() != null))? 
                    strPutFuel : strNeedFuel;
            }

            return strTakeFuel;
        }
        else if (lid.Openness == OpennessType.Close)
        {
            return strOpenLid;
        }

        return base.PromtMessage(playerInteract);
    }
}
