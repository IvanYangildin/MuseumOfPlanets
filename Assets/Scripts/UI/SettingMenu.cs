using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField]
    Slider sensitivitySlider;

    private void Awake()
    {
        SetUI();
        GlobalManager.Instance.OnLoaded += SetUI;
    }

    public void SetUI()
    {
        sensitivitySlider.SetValueWithoutNotify(GlobalManager.Settings.sensitivity);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetSensitivity(float sens)
    {
        GlobalManager.Settings.sensitivity = sens;
    }
}
