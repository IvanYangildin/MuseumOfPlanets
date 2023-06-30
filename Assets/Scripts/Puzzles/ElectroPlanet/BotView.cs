using UnityEngine;

public class BotView : MonoBehaviour
{
    [SerializeField]
    private Camera botCamera;
    private MeshRenderer render;
    [SerializeField]
    int width, height;

    [SerializeField]
    BotPanel panel;

    private void Awake()
    {
        render = GetComponent<MeshRenderer>();
        if (botCamera.targetTexture == null)
        {
            botCamera.targetTexture = new RenderTexture(width, height, 24);
        }
        render.material.mainTexture = botCamera.targetTexture;
        render.material.SetTexture("_EmissionMap", botCamera.targetTexture);
    }

    private void Update()
    {
        if (panel.IsBotActive)
        {
            botCamera.cullingMask = -1;
        }
        else
        {
            botCamera.cullingMask = (1 << LayerMask.NameToLayer("UI"));
        }
    }
}