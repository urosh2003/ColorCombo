using UnityEngine;
using Cinemachine;
public class ScreenShake : MonoBehaviour
{
    private CinemachineVirtualCamera camera;

    [SerializeField] private float superShakeAmplitude;
    [SerializeField] private float superShakeFrequency;
    private float currentShakeAmplitude;
    private float currentShakeDuration;
    [SerializeField] private float superShakeDuration;
    [SerializeField] private float timeElapsed;

    [SerializeField] private float comboShakeDuration;
    [SerializeField] private float comboBaseShakeAmplitude;
    [SerializeField] private float perComboShakeAmplitude;
    [SerializeField] private float comboShakeFrequency;

    CinemachineBasicMultiChannelPerlin channelPerlin;
    private bool isShaking;

    private void Awake()
    {
        camera = GetComponent<CinemachineVirtualCamera>();
        channelPerlin = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        PlayerManager.instance.SuperUsed += SuperShake;
        GameManager.instance.RefreshCombo += ComboShake;
    }

    private void SuperShake()
    {
        channelPerlin.m_AmplitudeGain += superShakeAmplitude;
        channelPerlin.m_FrequencyGain = superShakeFrequency;

        currentShakeAmplitude = superShakeAmplitude;
        currentShakeDuration = superShakeDuration;
        timeElapsed = 0;
    }

    private void ComboShake(int combo)
    {
        if (combo == 0)
        {
            return;
        }
        combo -= 3;
        if (combo < 0)
            combo = 0;

        channelPerlin.m_AmplitudeGain += combo * perComboShakeAmplitude + comboBaseShakeAmplitude;
        channelPerlin.m_FrequencyGain = comboShakeFrequency;

        currentShakeAmplitude = channelPerlin.m_AmplitudeGain;
        currentShakeDuration = comboShakeDuration;
        timeElapsed = 0;
    }

    private void OnDestroy()
    {
        PlayerManager.instance.SuperUsed -= SuperShake;
        GameManager.instance.RefreshCombo -= ComboShake;
    }

    private void FixedUpdate()
    {
        timeElapsed += Time.fixedDeltaTime;
        if(currentShakeAmplitude > 0)
        {
            currentShakeAmplitude = Mathf.Lerp(currentShakeAmplitude, 0, timeElapsed/currentShakeDuration);
            channelPerlin.m_AmplitudeGain = currentShakeAmplitude;
        }

    }
}