using UnityEngine;

[System.Serializable]
public class GameSettings
{
    [Range(5f, 80f)]
    public float sensitivity = 30;
    [Range(-80f, 0f)]
    public float masterVolume, ambientVolume, effectsVolume;
}