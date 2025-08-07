using System.Collections;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    [SerializeField] float hitStopDuration;

    private void Start()
    {
        GameManager.instance.RefreshCombo += StopGame;
    }

    private void StopGame(int combo)
    {
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
}