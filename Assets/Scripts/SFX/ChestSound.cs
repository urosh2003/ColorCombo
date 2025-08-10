using UnityEngine;

public class ChestSound : MonoBehaviour
{
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameManager.instance.ChestCollected += PlayChestSound;
    }

    void PlayChestSound(int chest)
    {
        audioSource.Play();
    }

    private void OnDestroy()
    {
        GameManager.instance.ChestCollected -= PlayChestSound;
    }
}