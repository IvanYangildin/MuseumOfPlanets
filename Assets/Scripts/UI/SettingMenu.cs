using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    [SerializeField]
    Slider  sensitivitySlider,
            volumeSlider,
            ambientSlider,
            effectsSlider;
    [SerializeField]
    AudioMixerGroup mixer;

    private void Awake()
    {
        SetUI();
        DataManager.Instance.OnLoaded += SetUI;
    }

    public void SetUI()
    {
        sensitivitySlider.SetValueWithoutNotify(DataManager.Settings.sensitivity);

        volumeSlider.SetValueWithoutNotify(DataManager.Settings.masterVolume);
        mixer.audioMixer.SetFloat("MasterVolume", DataManager.Settings.masterVolume);

        ambientSlider.SetValueWithoutNotify(DataManager.Settings.ambientVolume);
        mixer.audioMixer.SetFloat("AmbientVolume", DataManager.Settings.ambientVolume);

        effectsSlider.SetValueWithoutNotify(DataManager.Settings.effectsVolume);
        mixer.audioMixer.SetFloat("EffectsVolume", DataManager.Settings.effectsVolume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetSensitivity(float sens)
    {
        DataManager.Settings.sensitivity = sens;
    }

    public void SetMasterVolume(float vol)
    {
        mixer.audioMixer.SetFloat("MasterVolume", vol);
        DataManager.Settings.masterVolume = vol;
    }

    public void SetAmbientVolume(float vol)
    {
        mixer.audioMixer.SetFloat("AmbientVolume", vol);
        DataManager.Settings.ambientVolume = vol;
    }

    public void SetEffectsVolume(float vol)
    {
        mixer.audioMixer.SetFloat("EffectsVolume", vol);
        DataManager.Settings.effectsVolume = vol;
    }
}
