using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakenSpawner : Interactable
{
    [SerializeField]
    TakenObject itemToSpawn;
    [SerializeField]
    int spawnCountMax;
    int spawnCount = 0;

    public override string PromtMessage(PlayerInteract playerInteract) => itemToSpawn.PromtMessage(playerInteract);

    protected override void interact(PlayerInteract playerInteract)
    {
        PlayerHoldItem playerHold = playerInteract.GetComponent<PlayerHoldItem>();
        if (playerHold != null)
        {
            if (itemToSpawn.CanTake(playerHold) && (spawnCount < spawnCountMax))
            {
                TakenObject obj = Instantiate(itemToSpawn);
                obj.Take(playerHold);
                obj.Destroyed += () => spawnCount--;
                spawnCount++;
            }
        }
    }
}
