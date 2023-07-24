using System.Collections.Generic;
using UnityEngine;


public class SceneRenderControl : MonoBehaviour
{
    [SerializeField]
    List<GameObject> controled;
    [SerializeField]
    GameObject currentRendered;

    private void Start()
    {
        foreach (GameObject go in controled)
        {
            RenderSwitch.unrender(go);
        }
        RenderSwitch.render(currentRendered);
    }

    public void SwitchRender(GameObject nextRendered)
    {
        if (currentRendered != nextRendered)
        {
            RenderSwitch.render(nextRendered);
            RenderSwitch.unrender(currentRendered);
            currentRendered = nextRendered;
        }
    }
}