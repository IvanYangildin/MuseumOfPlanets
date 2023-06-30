using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDevice : Interactable
{
    [SerializeField]
    DoorObject Door;
    [SerializeField]
    FuelContainer container;
    [SerializeField]
    DoorObject containerLid;

    private void fuelTimer(Fuel fuel)
    {
        float fuelTime = (fuel == null) ? 0 : fuel.BurningTime;
        if (fuelTime > 0)
        {
            Timer timer = gameObject.AddComponent<Timer>();

            timer.TimeUp += Door.Close;
            timer.TimeUp += containerLid.Unlock;
            timer.TimeUp += container.BurnFuel;

            timer.StartTimer(fuelTime);
        }
        else if (fuelTime == 0)
        {
            Door.Close();
            containerLid.Unlock();
        }
        else
        {
            containerLid.Unlock();
            containerLid.OnOpening += Door.Close;
        }
    }

    protected override void interact(PlayerInteract playerInteract)
    {
        if ((container.Fullness == FullnessType.Full) && (containerLid.Openness == OpennessType.Close))
        {
            Door.Open();
            containerLid.Lock();
            fuelTimer(container.fuel);
        }

    }
}
