using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TakenObject : Interactable
{
    protected PlayerHoldItem currentHolder = null;
    public delegate void Handler();
    public event Handler Destroyed;

    private void OnDestroy()
    {
        Destroyed?.Invoke();
    }

    [SerializeField]
    protected Vector3 holdOffset;
    [SerializeField]
    protected Quaternion holdAngle;

    [SerializeField]
    protected Quaternion defaultAngle;
    public Quaternion DefaultAngle => defaultAngle;

    [SerializeField]
    Vector3 defaultOffset;
    public Vector3 DefaultOffset => defaultOffset;

    protected Rigidbody body;

    [SerializeField]
    protected AudioClip takenSound = null;

    AudioClip getTakenSound(PlayerHoldItem holder)
    {
        return (takenSound == null) ? holder.TakeSoundDefault : takenSound;
    }

    [SerializeField]
    protected int basicLayer = 6;
    public int BasicLayer => basicLayer;

    private void Awake()
    {
        body = gameObject.GetComponent<Rigidbody>();
    }

    protected override void interact(PlayerInteract playerInteract)
    {
        PlayerHoldItem holdItem = playerInteract.gameObject.GetComponent<PlayerHoldItem>();
        Take(holdItem);
    }

    protected void setLayer(int layer)
    {
        gameObject.layer = layer;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.layer = gameObject.layer;
        }
    }

    protected void recreateRigidbody()
    {
        if (body == null)
        {
            body = gameObject.AddComponent<Rigidbody>();
            body.interpolation = RigidbodyInterpolation.Interpolate;
            body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
    }

    public virtual bool CanTake(PlayerHoldItem playerHold)
    {
        return playerHold.Item == null;
    }

    protected virtual void take(PlayerHoldItem playerHold)
    {
        playerHold.Item = this;
    }

    public virtual string TookText(PlayerHoldItem playerHold)
    {
        return playerHold.StandardDropText;
    }

    public virtual string DroppedText(PlayerHoldItem playerHold)
    {
        return string.Empty;
    }

    public void Take(PlayerHoldItem playerHold)
    {
        if (CanTake(playerHold))
        {
            Destroy(body);
            setLayer(LayerMask.NameToLayer("InfrontOthers"));

            transform.SetParent(playerHold.ParentForItem);

            transform.localPosition = holdOffset;
            transform.localRotation = holdAngle;

            currentHolder = playerHold;
            AudioSource.PlayClipAtPoint(getTakenSound(playerHold), playerHold.transform.position);
            playerHold.PlayerUser.StandardText = TookText(playerHold);

            take(playerHold);
        }
    }

    protected virtual void drop()
    {
        currentHolder.Item = null;
    }

    public void Drop()
    {
        if (currentHolder != null)
        {
            setLayer(BasicLayer);
            transform.SetParent(currentHolder.GetComponent<PlayerInteract>().PlayerCamera.transform);
            transform.localRotation = defaultAngle;
            
            Vector3 offset = currentHolder.GetComponent<PlayerHoldItem>().DropOffset(GetComponent<Collider>());
            transform.position = currentHolder.GetComponent<PlayerHoldItem>().DropLocalPosition();
            transform.position += offset;

            recreateRigidbody();
            transform.SetParent(currentHolder.transform);
            transform.localRotation = defaultAngle;
            transform.SetParent(null);

            currentHolder.PlayerUser.StandardText = DroppedText(currentHolder);

            drop();
            currentHolder = null;
        }
    }

    public virtual TakenObject ItemToDrop => this;

}
