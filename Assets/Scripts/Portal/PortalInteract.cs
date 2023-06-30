using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalInteract : Interactable
{
    [SerializeField]
    Portal portal;

    protected override void lookInteract(PlayerInteract playerInteract)
    {
        Vector3 direction = -(playerInteract.PlayerCamera.transform.position - playerInteract.InteractData.point).normalized;
        Vector3 localPoint = portal.TeleportTransform(playerInteract.InteractData.point);
        Vector3 teleportPoint = portal.Other.transform.localToWorldMatrix.MultiplyPoint3x4(localPoint);

        float oldDistance = (playerInteract.transform.position - playerInteract.InteractData.point).magnitude;
        float newDistance = playerInteract.Distance - oldDistance;
        playerInteract.SetInteraction(teleportPoint, direction, newDistance);

        playerInteract.ExtractPoint = (dist) => (dist < oldDistance) ?
                                  playerInteract.SimpleExctract(dist, oldDistance) :
                                  teleportPoint + direction * Mathf.Clamp(dist - oldDistance, 0, newDistance);
    }
}
