using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColorBinding : MonoBehaviour
{
    public InputActionReference actionReference;

    private InputAction action => actionReference != null ? actionReference.action : null;

    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = action.GetBindingDisplayString();
    }
}