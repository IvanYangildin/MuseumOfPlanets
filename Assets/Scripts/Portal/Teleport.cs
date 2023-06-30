using UnityEngine;

public class Teleport : MonoBehaviour
{
    public const float teleportDepth = 0.25f;
    public Teleport Destination;

    private void OnTriggerEnter(Collider other)
    {
        Teleportate(other.gameObject);
    }

    void Teleportate(GameObject passenger)
    {
        CharacterController cc = passenger.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;
        
        Vector3 localPos = transform.worldToLocalMatrix.MultiplyPoint3x4(passenger.transform.position);
        localPos = Quaternion.Euler(0, 180, 0) * localPos;
        Quaternion lookRot = Quaternion.Inverse(transform.rotation) * passenger.transform.rotation;
        lookRot = Quaternion.Euler(0, 180, 0) * lookRot;

        passenger.transform.position = Destination.transform.localToWorldMatrix.MultiplyPoint3x4(localPos);
        passenger.transform.rotation = Destination.transform.rotation * lookRot;

        LightTeleport lighting = GetComponent<LightTeleport>();
        if (lighting) lighting.TryLight(passenger.GetComponent<Collider>());

        foreach (var portal in FindObjectsOfType<Portal>()) 
        { 
            portal.CameraUpdate(); 
            portal.PortalCamera.Render();
        }

        if (cc != null) cc.enabled = true;
    }
}
