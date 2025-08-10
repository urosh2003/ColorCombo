using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AudioSettings : MonoBehaviour
{
    public enum Type { Music, SFX }
    public Type type;
    Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0.0001f; // avoid 0 on log
        slider.maxValue = 1f;
    }

    void Start()
    {
        if (SettingsManager.Instance == null) return;
        if (type == Type.Music)
            slider.value = SettingsManager.Instance.GetSavedMusicVolume();
        else
            slider.value = SettingsManager.Instance.GetSavedSFXVolume();

        slider.onValueChanged.AddListener(OnSliderChanged);
    }

    private void OnSliderChanged(float v)
    {
        if (SettingsManager.Instance == null) return;
        if (type == Type.Music)
            SettingsManager.Instance.SetMusicVolume(v);
        else
            SettingsManager.Instance.SetSFXVolume(v);
    }
}
