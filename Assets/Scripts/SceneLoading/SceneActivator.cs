using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneActivator
{
    public static void Activate(Scene scene, bool active)
    {
        foreach (var obj in scene.GetRootGameObjects())
        {
            obj.SetActive(active);
        }
    }
}