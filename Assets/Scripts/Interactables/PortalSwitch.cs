using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PortalSwitch : Interactable
{
    [SerializeField]
    MainPuzzle mainPuzzle;
    [SerializeField]
    PasswordSequential<PlanetLabel> passwordMain;
    [SerializeField]
    PlanetLabel destinationLabel;

    [SerializeField]
    DoorObject button;

    private void portalSwitch()
    {
        if (mainPuzzle.Enter(destinationLabel))
        {
            mainPuzzle.Entry(PlanetLabel.Contract).SummonPortal(mainPuzzle.DestinationPortal);
        }
        else
        {
            mainPuzzle.Entry(destinationLabel).SummonPortal(mainPuzzle.DestinationPortal);
        }
    }

    protected override void interact(PlayerInteract playerInteract)
    {
        if (button == null)
        {
            portalSwitch();
        }
        else if (button.Openness == OpennessType.Close)
        {
            button.Open();
            button.OnOpen += button.Close;
            portalSwitch();
        }
    }
}
