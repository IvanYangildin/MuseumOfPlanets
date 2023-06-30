using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutPlace : Interactable
{
    [SerializeField]
    Collider place;

    protected override void interact(PlayerInteract playerInteract) 
    {
        PlayerHoldItem holder = playerInteract.gameObject.GetComponent<PlayerHoldItem>();

        if (holder != null)
        {
            TakenObject item = holder.Item?.ItemToDrop;
            if (item != null)
            {
                Vector3 normal = playerInteract.InteractData.normal;

                if (Vector3.Dot(Vector3.up, normal) > 0)
                {
                    item.Drop();
                    item.transform.position = playerInteract.InteractData.point + item.DefaultOffset;
                    item.transform.rotation = item.DefaultAngle;
                }
            }
        }
    }
}
