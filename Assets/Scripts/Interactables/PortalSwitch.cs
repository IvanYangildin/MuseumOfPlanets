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
    SceneRenderControl src;

    [SerializeField]
    DoorObject button;

    public void Switch()
    {
        if (mainPuzzle.Enter(destinationLabel))
        {
            mainPuzzle.Entry(PlanetLabel.Contract).SummonPortal(mainPuzzle.DestinationPortal);
            src.SwitchRender(mainPuzzle.Place(PlanetLabel.Contract));
        }
        else
        {
            mainPuzzle.Entry(destinationLabel).SummonPortal(mainPuzzle.DestinationPortal);
            src.SwitchRender(mainPuzzle.Place(destinationLabel));
        }
    }

    protected override void interact(PlayerInteract playerInteract)
    {
        if (button == null)
        {
            Switch();
        }
        else if (button.Openness == OpennessType.Close)
        {
            button.Open();
            button.OnOpen += button.Close;
            // portalSwitch();
        }
    }
}
