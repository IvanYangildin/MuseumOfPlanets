using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalInteract : Interactable
{
    [SerializeField]
    Portal portal;

    protected override void lookInteract(PlayerInteract playerInteract)
    {
        Vector3 player = playerInteract.PlayerCamera.transform.position;
        Vector3 localPlayer = portal.TeleportTransform(player);
        Vector3 teleportPlayer = portal.Other.transform.localToWorldMatrix.MultiplyPoint3x4(localPlayer);

        Vector3 interaction = playerInteract.InteractData.point;
        Vector3 localInteraction = portal.TeleportTransform(interaction);
        Vector3 teleportInteraction = portal.Other.transform.localToWorldMatrix.MultiplyPoint3x4(localInteraction);

        Vector3 direction = -(teleportPlayer - teleportInteraction).normalized;

        float oldDistance = (playerInteract.transform.position - playerInteract.InteractData.point).magnitude;
        float newDistance = playerInteract.Distance - oldDistance;
        playerInteract.SetInteraction(teleportInteraction, direction, newDistance);

        playerInteract.ExtractPoint = (dist) => (dist < oldDistance) ?
                                  playerInteract.SimpleExctract(dist, oldDistance) :
                                  teleportInteraction + direction * Mathf.Clamp(dist - oldDistance, 0, 
                                  (playerInteract.InteractData.distance > 0)? playerInteract.InteractData.distance 
                                                                                                    : newDistance);
    }
}
