using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : GameSignleton<SceneLoader>
{

    Func<IEnumerator> do_update = null;

    public static IEnumerator SetActive(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        while (!scene.isLoaded) yield return null;
        SceneManager.SetActiveScene(scene);
        yield break;
    }

    public static void TransitToScene(string sceneName)
    {
        SceneActivator.Activate(SceneManager.GetActiveScene(), false);

        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene.isLoaded)
        {
            SceneManager.SetActiveScene(scene);
            SceneActivator.Activate(scene, true);
        }
        else
        {
            Instance.do_update = () => 
            {
                return SetActive(sceneName); 
            };
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }

    private void Update()
    {
        if (do_update != null) 
        {
            StartCoroutine(do_update()); 
            do_update = null;
        }
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}