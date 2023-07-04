using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneRemoveType { Deactivate, Unload };

public class SceneActivator
{

    public static void Activate(Scene scene, bool active)
    {
        foreach (var obj in scene.GetRootGameObjects())
        {
            obj.SetActive(active);
        }
    }

    public static void RemoveScene(Scene scene, SceneRemoveType mode)
    {
        if (mode == SceneRemoveType.Deactivate)
        {
            Activate(scene, false);
        }
        else
        {
            var async = SceneManager.UnloadSceneAsync(scene);
        }
    }

}