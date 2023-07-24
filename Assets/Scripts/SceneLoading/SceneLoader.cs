using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

static public class SceneLoader
{
    private class Loader : GameSignleton<Loader>
    {
        public Action loading = null;
        public float progress { private set; get; } = 1f;

        public bool isLoading = false;


        public IEnumerator load(string sceneName, string loadingName, bool reload)
        {
            isLoading = true;

            Scene scene = SceneManager.GetSceneByName(sceneName);
            if (reload && scene.isLoaded)
            {
                var async = SceneManager.UnloadSceneAsync(sceneName);
                yield return new WaitUntil(() => async.isDone);
                scene = SceneManager.GetSceneByName(sceneName);
            }

            if (!scene.isLoaded)
            {
                var async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                while (!async.isDone)
                {
                    progress = async.progress;
                    yield return null;
                }
                scene = SceneManager.GetSceneByName(sceneName);
            }
            else
            {
                SceneActivator.Activate(scene, true);
            }
            SceneManager.SetActiveScene(scene);

            // destory loading scene

            SceneManager.UnloadSceneAsync(loadingName);

            isLoading = false;
            yield break;
        }

    }

    public static bool IsSceneLoaded(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        return scene != null && scene.isLoaded;
    }

    public static void LoadWithDeactivate(string sceneName, string loadingName, bool reload)
    {
        LoadWith(sceneName, loadingName, SceneRemoveType.Deactivate, reload);
    }

    public static void LoadWithUnload(string sceneName, string loadingName, bool reload)
    {
        LoadWith(sceneName, loadingName, SceneRemoveType.Unload, reload);
    }

    public static void LoadWith(string sceneName, string loadingName, SceneRemoveType remove_t, bool reload)
    {
        if (!Loader.Instance.isLoading)
        {
            Loader.Instance.isLoading = true;

            Scene prev = SceneManager.GetActiveScene();
            
            var _async = SceneManager.LoadSceneAsync(loadingName, LoadSceneMode.Additive);
            _async.allowSceneActivation = true;
            _async.completed += arg =>
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(loadingName));
                SceneActivator.RemoveScene(prev, remove_t);
                Loader.Instance.StartCoroutine(Loader.Instance.load(sceneName, loadingName, reload));
            };
        } 
    }
}