using UnityEngine;
using UnityEngine.UI;

// Optional glue component if you prefer to hook UI to this one script instead of attaching small components per control.
public class SettingsUI : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    public Toggle screenShakeToggle;

    void Start()
    {
        ApplySavedSettings();

        if (musicSlider != null)
            musicSlider.onValueChanged.AddListener(v => SettingsManager.Instance.SetMusicVolume(v));

        if (sfxSlider != null)
            sfxSlider.onValueChanged.AddListener(v => SettingsManager.Instance.SetSFXVolume(v));

        if (screenShakeToggle != null)
            screenShakeToggle.onValueChanged.AddListener(v => SettingsManager.Instance.SetScreenShakeEnabled(v));
    }

    public void ApplySavedSettings()
    {
        if (SettingsManager.Instance == null) return;
        if (musicSlider != null) musicSlider.value = SettingsManager.Instance.GetSavedMusicVolume();
        if (sfxSlider != null) sfxSlider.value = SettingsManager.Instance.GetSavedSFXVolume();
        if (screenShakeToggle != null) screenShakeToggle.isOn = SettingsManager.Instance.GetSavedScreenShakeEnabled();
    }
}
