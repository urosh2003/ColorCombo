using Unity.Burst.Intrinsics;
using UnityEngine;

public class HitSound : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip comboSound;
    [SerializeField] private AudioClip missSound;
    [SerializeField] private int maxComboSound;



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        GameManager.instance.RefreshCombo += PlaySound;
        GameManager.instance.FailHit += PlayFail;
    }

    private void PlayFail()
    { 
        audioSource.pitch = 1;
        audioSource.clip = missSound;
        audioSource.Play();

    }

    private void PlaySound(int combo)
    {
        if (combo == 0)
            return;
        if(combo < 3)
        {
            audioSource.pitch = 1;
            audioSource.clip = hitSound;
        }
        else
        {
            audioSource.pitch = 1f + (combo-3f) / maxComboSound;
            audioSource.clip = comboSound;
        }
        audioSource.Play();
    }

    private void OnDestroy()
    {
        GameManager.instance.RefreshCombo -= PlaySound;
        GameManager.instance.FailHit -= PlayFail;
    }
}