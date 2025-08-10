using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("got: " + SettingsManager.Instance.GetSavedScreenShakeEnabled());

        GetComponent<Toggle>().SetIsOnWithoutNotify(SettingsManager.Instance.GetSavedScreenShakeEnabled());
    }

    public void OnValueChange(bool newValue)
    {
        SettingsManager.Instance.SetScreenShakeEnabled(newValue);
    }
}
