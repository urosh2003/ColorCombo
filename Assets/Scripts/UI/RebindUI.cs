using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using TMPro;
using System.Collections.Generic;

[RequireComponent(typeof(Button))]
public class RebindUI : MonoBehaviour
{
    [Tooltip("Input Action Reference to rebind")] public InputActionReference actionReference;
    [Tooltip("Index of the binding on this action to rebind (0..n)")] public int bindingIndex = 0;

    [Header("UI References")]
    public TextMeshProUGUI bindingDisplayText; // shows current binding
    public Button rebindButton;

    private InputAction action => actionReference != null ? actionReference.action : null;
    private InputBinding? originalBinding;

    void Start()
    {
        if (action == null)
        {
            Debug.LogWarning("RebindUI: actionReference not set or not valid.");
            if (rebindButton != null) rebindButton.interactable = false;
            return;
        }

        //if (action.bindings[bindingIndex].isComposite)
        //    bindingDisplayText.text == action.GetBindingDisplayString(bindingIndex, ;
        if (bindingDisplayText != null)
            bindingDisplayText.text = action.GetBindingDisplayString(bindingIndex);

        if (rebindButton != null)
            rebindButton.onClick.AddListener(StartRebind);
    }

    private void OnEnable()
    {
        UpdateDisplay();
        SettingsManager.OnBindingsLoaded += UpdateDisplayIfEnabled;
    }

    public void StartRebind()
    {
        if (action == null) return;

        // If the selected binding index points to a composite (the parent), rebind its parts
        var bindings = action.bindings;
        if (bindingIndex >= 0 && bindingIndex < bindings.Count && bindings[bindingIndex].isComposite)
        {
            // collect child binding indices
            var parts = new List<int>();
            for (int i = bindingIndex + 1; i < bindings.Count && bindings[i].isPartOfComposite; ++i)
                parts.Add(i);

            StartRebindComposite(parts, 0);
            return;
        }

        // non-composite: single binding
        StartSingleRebind(bindingIndex);
    }

    private void StartSingleRebind(int bindIndex)
    {
        action.Disable();

        if (bindingDisplayText != null)
            bindingDisplayText.text = "...";

        action.PerformInteractiveRebinding(bindIndex)
            .WithControlsExcluding("<Mouse>/position")
            .WithControlsExcluding("<Mouse>/delta")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation =>
            {
                operation.Dispose();
                action.Enable();
                UpdateDisplay();
                if (SettingsManager.Instance != null && SettingsManager.Instance.inputActionsAsset != null)
                    SettingsManager.Instance.SaveBindingOverridesForAsset();
            })
            .Start();
    }

    private void StartRebindComposite(List<int> parts, int partIdx)
    {
        if (parts == null || parts.Count == 0) return;

        if (partIdx == 0) action.Disable();

        int childBindingIndex = parts[partIdx];

        string partName = action.bindings[childBindingIndex].name; 
        if (bindingDisplayText != null)
            bindingDisplayText.text = $"Press new binding for: {partName}";

        action.PerformInteractiveRebinding(childBindingIndex)
            .WithControlsExcluding("<Mouse>/position")
            .WithControlsExcluding("<Mouse>/delta")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation =>
            {
                operation.Dispose();
                if (partIdx + 1 < parts.Count)
                {
                    StartRebindComposite(parts, partIdx + 1);
                }
                else
                {
                    action.Enable();
                    UpdateDisplay();
                    if (SettingsManager.Instance != null && SettingsManager.Instance.inputActionsAsset != null)
                        SettingsManager.Instance.SaveBindingOverridesForAsset();
                }
            })
            .Start();
    }


    private void UpdateDisplay()
    {
        if (bindingDisplayText == null || action == null) return;

        var bindings = action.bindings;
        if (bindingIndex < 0 || bindingIndex >= bindings.Count)
        {
            bindingDisplayText.text = "-";
            return;
        }

        // If composite, show each child part's display (e.g. "Up: W  Left: A  Down: S  Right: D")
        if (bindings[bindingIndex].isComposite)
        {
            var parts = new List<string>();
            for (int i = bindingIndex + 1; i < bindings.Count && bindings[i].isPartOfComposite; ++i)
            {
                string partName = bindings[i].name; // usually "up","down","left","right"
                string display = action.GetBindingDisplayString(i);
                if (string.IsNullOrEmpty(display)) display = "Unbound";
                // Capitalize part name if present
                if (!string.IsNullOrEmpty(partName))
                    partName = char.ToUpper(partName[0]) + partName.Substring(1);
                else
                    partName = (i - bindingIndex).ToString();

                parts.Add($"{partName}: {display}");
            }

            bindingDisplayText.text = string.Join("   ", parts);
            return;
        }

        // non-composite simple binding
        string s = action.GetBindingDisplayString(bindingIndex);
        if (string.IsNullOrEmpty(s)) s = "Unbound";
        bindingDisplayText.text = s;
    }

    private void UpdateDisplayIfEnabled()
    {
        if (gameObject.activeInHierarchy) UpdateDisplay();
    }

    void OnDisable()
    {
        SettingsManager.OnBindingsLoaded -= UpdateDisplayIfEnabled;
    }
}