using UnityEngine;

public class Portal : MonoBehaviour
{
    public Camera PortalCamera;
    [SerializeField]
    private float planeOffset;


    [SerializeField]
    private GameObject PortalPlane;
    private MeshRenderer render;
    private Plane clipPlane;

    [SerializeField]
    private PlayerLook looker;

    [SerializeField]
    private Portal other;
    public Portal Other 
    { 
        get => other;
        set
        {
            other = value;
            refineTexture();
        }
    }

    private void Awake()
    {
        Material material = new Material(Shader.Find("Unlit/PortalShader"));
        render = PortalPlane.GetComponent<MeshRenderer>();
        render.material = material;
        clipPlane = new Plane(-transform.forward, transform.position);

        refineTexture();
        looker.OnLooked += CameraUpdate;
    }

    public Vector3 TeleportTransform(Vector3 position)
    {
        Vector3 outPos = transform.worldToLocalMatrix.MultiplyPoint3x4(position);
        outPos = Quaternion.Euler(0, 180, 0) * outPos;
        return outPos;
    }

    public Quaternion TeleportTransform(Quaternion rotation)
    {
        Quaternion outRot = Quaternion.Inverse(transform.rotation) * rotation;
        outRot = Quaternion.Euler(0, 180, 0) * outRot;
        return outRot;
    }

    public void TeleportTransform(Vector3 position, Quaternion rotation, out Vector3 outPos, out Quaternion outRot)
    {
        outPos = TeleportTransform(position);
        outRot = TeleportTransform(rotation);
    }

    public void TeleportTransform(Transform fromEnter, out Vector3 outPos, out Quaternion outRot)
    {
        TeleportTransform(fromEnter.position, fromEnter.rotation, out outPos, out outRot);
    }

    public void CameraUpdate()
    {
        Vector3 lookPos;
        Quaternion lookRot;
        Other.TeleportTransform(looker.PlayerCamera.transform, out lookPos, out lookRot);

        PortalCamera.transform.localPosition = lookPos;
        PortalCamera.transform.localRotation = lookRot;

        Vector4 clipPlaneWorld = new Vector4(clipPlane.normal.x, clipPlane.normal.y, clipPlane.normal.z, clipPlane.distance);
        Vector4 clipPlaneLocal = Matrix4x4.Transpose(Matrix4x4.Inverse(PortalCamera.worldToCameraMatrix)) * clipPlaneWorld;

        PortalCamera.projectionMatrix = PortalCamera.CalculateObliqueMatrix(clipPlaneLocal);
    }

    void LateUpdate()
    {
        clipPlane = new Plane(-transform.forward, transform.position + transform.forward * planeOffset);
        refineTexture();
        CameraUpdate();
    }

    private void refineTexture()
    {
        if ((Other.PortalCamera.targetTexture == null) ||
                (Other.PortalCamera.targetTexture.width != looker.PlayerCamera.pixelWidth) ||
                (Other.PortalCamera.targetTexture.height != looker.PlayerCamera.pixelHeight))
        {
            Other.PortalCamera.targetTexture =
                new RenderTexture(looker.PlayerCamera.pixelWidth, looker.PlayerCamera.pixelHeight, 24);
        }

        render.sharedMaterial.mainTexture = Other.PortalCamera.targetTexture;
    }
}
