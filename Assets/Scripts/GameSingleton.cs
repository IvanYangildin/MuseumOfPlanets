using UnityEngine;

public class GameSignleton<TSelf> : MonoBehaviour where TSelf : GameSignleton<TSelf>
{
    static GameSignleton<TSelf> instance = null;

    public static TSelf Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject _inst = new GameObject("Singleton" + typeof(TSelf).Name);
                instance = _inst.AddComponent<TSelf>();
            }
            return instance as TSelf;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}