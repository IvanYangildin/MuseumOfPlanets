using UnityEngine;

public static class RenderSwitch
{
    static public void unrender(GameObject go)
    {
        render(go, false);
    }

    static public void render(GameObject go, bool mode = true)
    {
        var rends = go.gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rends)
        {
            r.enabled = mode || (r.gameObject.layer == LayerMask.NameToLayer("RenderAlways"));
        }

        var ters = go.gameObject.GetComponentsInChildren<Terrain>();
        foreach (Terrain t in ters)
        {
            bool cur_mode = mode || (t.gameObject.layer == LayerMask.NameToLayer("RenderAlways"));
            t.drawHeightmap = cur_mode;
            t.drawInstanced = cur_mode;
            t.drawTreesAndFoliage = cur_mode;
        }
    }
}
