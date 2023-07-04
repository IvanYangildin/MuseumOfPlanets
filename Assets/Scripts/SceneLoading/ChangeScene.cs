using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    string sceneNext, sceneLoading;
    [SerializeField]
    SceneRemoveType removeType = SceneRemoveType.Unload;
    [SerializeField]
    bool reload;

    public void Change()
    {
        SceneLoader.LoadWith(sceneNext, sceneLoading, removeType, reload);
    }
}