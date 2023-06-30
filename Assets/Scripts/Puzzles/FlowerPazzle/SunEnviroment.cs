using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DaytimeMode { Day, Night };

public class SunEnviroment : MonoBehaviour
{
    [SerializeField]
    Texture night, day;
    [SerializeField]
    Light Sun;
    [SerializeField]
    Animator Flower;
    
    [SerializeField]
    Collider invisibleBox;
    Vector3 boxDefault;
    Vector3 boxOut => boxDefault + 2 * Vector3.down;

    DaytimeMode mode;
    public bool IsNight => mode == DaytimeMode.Night;
    public bool IsDay => mode == DaytimeMode.Day;

    [SerializeField]
    MeshRenderer skybox;

    private void Awake()
    {
        boxDefault = invisibleBox.transform.position;
        mode = DaytimeMode.Day;
        SetNight();
    }

    public void SetNight()
    {
        if (!IsNight)
        {
            Sun.gameObject.SetActive(false);
            skybox.material.mainTexture = night;
            mode = DaytimeMode.Night;
            Flower.SetTrigger("NightTime");
            invisibleBox.transform.position = boxDefault;
        }
    }

    public void SetDay()
    {
        if (!IsDay)
        {
            Sun.gameObject.SetActive(true);
            skybox.material.mainTexture = day;
            mode = DaytimeMode.Day;
            Flower.SetTrigger("DayTime");
            invisibleBox.transform.position = boxOut;
        }
    }
}
