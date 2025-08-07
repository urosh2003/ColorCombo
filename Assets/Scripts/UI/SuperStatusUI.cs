using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperStatusUI : MonoBehaviour
{
    [SerializeField] GameObject blueStatus;
    [SerializeField] GameObject redStatus;
    [SerializeField] GameObject yellowStatus;
    [SerializeField] GameObject greenStatus;
    [SerializeField] GameObject orangeStatus;
    [SerializeField] GameObject purpleStatus;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.ColorEnemyFell += ColorAdded;
        PlayerManager.instance.SuperUsed += ResetColors;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ColorAdded(WizardColor color)
    {
        switch (color)
        {
            case WizardColor.RED: redStatus.SetActive(true); break;
            case WizardColor.BLUE: blueStatus.SetActive(true); break;
            case WizardColor.YELLOW: yellowStatus.SetActive(true); break;
            case WizardColor.ORANGE: orangeStatus.SetActive(true); break;
            case WizardColor.GREEN: greenStatus.SetActive(true); break;
            case WizardColor.PURPLE: purpleStatus.SetActive(true); break;
        }
    }

    private void ResetColors()
    {
        redStatus.SetActive(false);
        blueStatus.SetActive(false);
        yellowStatus.SetActive(false);
        greenStatus.SetActive(false);
        orangeStatus.SetActive(false);
        purpleStatus.SetActive(false);
    }
}
