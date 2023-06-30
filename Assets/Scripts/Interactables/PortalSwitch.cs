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

    protected override void interact(PlayerInteract playerInteract)
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
}
