using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlanetLabel { Cold, Lava, Electro, Green, Contract };

public class MainPuzzle : PasswordSequential<PlanetLabel>
{
    [SerializeField]
    PortalPlace GreenEntry, LavaEntry, ColdEntry, ElectroEntry, ContractEntry;
    [SerializeField]
    GameObject GreenPlace, LavaPlace, ColdPlace, ElectroPlace, ContractPlace;

    [SerializeField]
    Portal mainPortal, destinationPortal;

    public Portal MainPortal => mainPortal;
    public Portal DestinationPortal => destinationPortal;
    protected override void processFailure(PlanetLabel symbol) { }

    public PortalPlace Entry(PlanetLabel label)
    {
        switch (label)
        {
            case PlanetLabel.Green:
                return GreenEntry;
            case PlanetLabel.Lava:
                return LavaEntry;
            case PlanetLabel.Cold:
                return ColdEntry;
            case PlanetLabel.Electro:
                return ElectroEntry;
            case PlanetLabel.Contract:
                return ContractEntry;
            default: return null;
        }
    }

    public GameObject Place(PlanetLabel label)
    {
        switch (label)
        {
            case PlanetLabel.Green:
                return GreenPlace;
            case PlanetLabel.Lava:
                return LavaPlace;
            case PlanetLabel.Cold:
                return ColdPlace;
            case PlanetLabel.Electro:
                return ElectroPlace;
            case PlanetLabel.Contract:
                return ContractPlace;
            default: return null;
        }
    }
}
