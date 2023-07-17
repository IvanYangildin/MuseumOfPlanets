using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Camera PlayerCamera;
    [SerializeField]
    private PlayerUI playerUI;
    public PlayerUI PlayerUI => playerUI;
    [SerializeField]
    private float distance;
    public float Distance => distance;
    [SerializeField]
    private LayerMask mask;

    public delegate void InteractionHandler(PlayerInteract playerInteract);
    public event InteractionHandler CancelInteract;
    public event InteractionHandler PerformInteract;

    public void OnPerformInteract()
    {
        if (PerformInteract != null)
        {
            PerformInteract.Invoke(this);
        }
        if (Target != null)
        {
            Target.Interact(this);
            CancelInteract += Target.OutInteract;
        }
    }

    public void OnCancelInteract()
    {
        if (CancelInteract != null)
        {
            CancelInteract.Invoke(this);
        }
    }

    private void Update()
    {
        playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(PlayerCamera.transform.position, PlayerCamera.transform.forward);

        SetInteraction(PlayerCamera.transform.position, PlayerCamera.transform.forward, distance);
    }

    public void SetInteraction(Vector3 position, Vector3 direction, float distance)
    {
        Target = null;

        Ray ray = new Ray(position, direction);
        // Debug.DrawRay(ray.origin, ray.direction * distance);

        if (Physics.Raycast(ray, out InteractData, distance, mask))
        {
            Debug.DrawLine(ray.origin, InteractData.point);
            ExtractPoint = (dist) => SimpleExctract(dist, InteractData.distance);
            Target = InteractData.collider.GetComponent<Interactable>();
            if (Target != null)
            {
                Target.LookInteract(this);
            }
        }
        else
        {
            ExtractPoint = (dist) => SimpleExctract(dist, distance);
        }
    }

    public delegate Vector3 DistancePointGetter(float dist);
    public DistancePointGetter ExtractPoint;

    public Vector3 SimpleExctract(float dist, float maxDist)
    {
        dist = Mathf.Clamp(dist, 0, maxDist);
        return PlayerCamera.transform.position + (PlayerCamera.transform.forward * dist);
    }

    public Interactable Target { get; private set; }
    public RaycastHit InteractData;
}
