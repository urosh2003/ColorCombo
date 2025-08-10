using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimAndShoot : MonoBehaviour
{
    private const float ANGLE_OFFSET = 90;
    private Vector3 mousePosition;
    private Vector2 aimDirection;
    private float aimAngle;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private Transform projectilePrefab;
    [SerializeField] private Transform projectileSpawnpoint;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        aimDirection = mousePosition - transform.position;

        aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - ANGLE_OFFSET;

        transform.rotation = Quaternion.Euler(0, 0, aimAngle);
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            audioSource.Play();
            PlayerManager.instance.Shoot(projectileSpawnpoint.position, transform.rotation, aimDirection);
        }
    }

    public void ShootSuper(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PlayerManager.instance.ShootSuper(projectileSpawnpoint.position, transform.rotation, aimDirection);
        }
    }
}
