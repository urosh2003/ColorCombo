using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSound : MonoBehaviour
{
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayerManager.instance.SuperUsed += PlaySuperSound;
    }

    void PlaySuperSound()
    {
        audioSource.Play();
    }

    private void OnDestroy()
    {
        PlayerManager.instance.SuperUsed -= PlaySuperSound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
