using UnityEngine;
using UnityEngine.Audio;
using Cinemachine;
using UnityEngine.InputSystem;
using System;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    [Header("Audio")]
    public AudioMixer audioMixer; // assign your AudioMixer

    [Header("Input")]
    public InputActionAsset inputActionsAsset; // optional: used if you want to save/load whole-asset bindings

    // Runtime caches
    private const string MUSIC_KEY = "musicVolume";
    private const string SFX_KEY = "sfxVolume";
    private const string SHAKE_KEY = "screenShake";

    public static event Action OnBindingsLoaded;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        LoadAudioSettings();
        LoadScreenShakeSetting();
        if (inputActionsAsset != null)
            LoadBindingOverridesForAsset();
    }

    #region Audio
    // value expected 0..1
    public void SetMusicVolume(float linear)
    {
        float db = LinearToDb(linear);
        audioMixer.SetFloat("MusicVolume", db);
        PlayerPrefs.SetFloat(MUSIC_KEY, linear);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float linear)
    {
        float db = LinearToDb(linear);
        audioMixer.SetFloat("SFXVolume", db);
        PlayerPrefs.SetFloat(SFX_KEY, linear);
        PlayerPrefs.Save();
    }

    private float LinearToDb(float linear)
    {
        // Avoid log(0)
        if (linear <= 0.0001f) return -80f; // effectively silence
        return Mathf.Log10(linear) * 20f;
    }

    public float GetSavedMusicVolume()
    {
        return PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
    }

    public float GetSavedSFXVolume()
    {
        return PlayerPrefs.GetFloat(SFX_KEY, 1f);
    }

    private void LoadAudioSettings()
    {
        SetMusicVolume(GetSavedMusicVolume());
        SetSFXVolume(GetSavedSFXVolume());
    }
    #endregion

    #region Screen Shake
    public void SetScreenShakeEnabled(bool enabled)
    {
        PlayerPrefs.SetInt(SHAKE_KEY, enabled ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log("set to: " + enabled);
    }

    public bool GetSavedScreenShakeEnabled()
    {
        return PlayerPrefs.GetInt(SHAKE_KEY, 1) == 1;
    }

    private void LoadScreenShakeSetting()
    {
        SetScreenShakeEnabled(GetSavedScreenShakeEnabled());
    }
    #endregion

    #region Bindings (Input System)
    // Save overrides for the whole InputActionAsset (if set)
    public void SaveBindingOverridesForAsset()
    {
        if (inputActionsAsset == null)
        {
            Debug.LogWarning("SettingsManager: inputActionsAsset not assigned — cannot save bindings for asset.");
            return;
        }
        string json = inputActionsAsset.SaveBindingOverridesAsJson();
        string key = GetBindingsKeyForAsset();
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    public void LoadBindingOverridesForAsset()
    {
        if (inputActionsAsset == null) return;
        string key = GetBindingsKeyForAsset();
        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            inputActionsAsset.LoadBindingOverridesFromJson(json);
        }
        OnBindingsLoaded?.Invoke();

    }

    private string GetBindingsKeyForAsset()
    {
        return "bindings_" + (inputActionsAsset == null ? "none" : inputActionsAsset.name);
    }
    #endregion

    #region Utilities
    public void ResetToDefaults()
    {
        PlayerPrefs.DeleteKey(MUSIC_KEY);
        PlayerPrefs.DeleteKey(SFX_KEY);
        PlayerPrefs.DeleteKey(SHAKE_KEY);
        if (inputActionsAsset != null)
            PlayerPrefs.DeleteKey(GetBindingsKeyForAsset());
        PlayerPrefs.Save();
        // re-apply defaults
        LoadAudioSettings();
        LoadScreenShakeSetting();
        if (inputActionsAsset != null)
            LoadBindingOverridesForAsset();
    }
    #endregion
}