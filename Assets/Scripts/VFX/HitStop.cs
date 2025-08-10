using System.Collections;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    [SerializeField] float hitStopDuration;

    private void Start()
    {
        Debug.Log("got: " + SettingsManager.Instance.GetSavedScreenShakeEnabled());

        GameManager.instance.RefreshCombo += StopGame;
    }

    private void StopGame(int combo)
    {
        if (!SettingsManager.Instance.GetSavedScreenShakeEnabled())
        {
            return;
        }

        if (combo < 4)
            return;

        StartCoroutine(StopTime());
    }

    IEnumerator StopTime()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(hitStopDuration);
        Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        GameManager.instance.RefreshCombo -= StopGame;
    }
}