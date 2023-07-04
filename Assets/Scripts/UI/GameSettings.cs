using UnityEngine;

[System.Serializable]
public class GameSettings
{
    [Range(5f, 80f)]
    public float sensitivity = 30;
}