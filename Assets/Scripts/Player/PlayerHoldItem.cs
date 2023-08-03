using UnityEngine;

public class PlayerHoldItem : MonoBehaviour
{
    [HideInInspector]
    public TakenObject Item;
    public GameObject ItemGameObject => (Item != null) ? Item.gameObject : null;
    
    [SerializeField]
    private PlayerInteract playerInteract;
    
    [SerializeField]
    private PlayerUI playerUser;
    public PlayerUI PlayerUser => playerUser;
    [SerializeField]
    private string standardDropText;
    public string StandardDropText => standardDropText;

    [SerializeField]
    float dropDistance;

    [SerializeField]
    AudioClip takeSoundDefault;
    public AudioClip TakeSoundDefault => takeSoundDefault;

    public Transform ParentForItem
    {
        get 
        {
            return (Item == null)? playerInteract.PlayerCamera.transform : Item.transform;
        }
    }

    public Vector3 DropLocalPosition()
    {
        return playerInteract.ExtractPoint(dropDistance);
    }

    public Vector3 DropOffset(Collider collider)
    {
        if (playerInteract.InteractData.distance > dropDistance + 10f) return Vector3.zero;

        Vector3 normal = playerInteract.InteractData.normal;

        Vector3 pointItemSurface = collider.ClosestPoint(collider.transform.position - normal * 10f);
        float height = (collider.transform.position - pointItemSurface).magnitude;

        Vector3 offset = normal * height;
        return offset;
    }

}

