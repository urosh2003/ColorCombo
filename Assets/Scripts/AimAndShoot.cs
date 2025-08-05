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

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure z-position is 0 for 2D

        // Calculate direction from gun to mouse
        aimDirection = mousePosition - transform.position;

        // Calculate the angle in degrees
        aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - ANGLE_OFFSET;

        // Apply the rotation to the gun
        transform.rotation = Quaternion.Euler(0, 0, aimAngle);
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Transform spawnedProjectile = Instantiate(projectilePrefab, projectileSpawnpoint.position, transform.rotation);
            spawnedProjectile.GetComponent<Rigidbody2D>().velocity = aimDirection * projectileSpeed;
        }
    }
}
