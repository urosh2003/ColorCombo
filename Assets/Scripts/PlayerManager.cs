using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum WizardColor { WHITE, RED, YELLOW, BLUE, ORANGE, PURPLE, GREEN, BLACK }

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private SpriteRenderer spriteRenderer;
    private IState colorState;

    [SerializeField] Transform projectilePrefab;
    [SerializeField] float projectileSpeed;
    [SerializeField] ProjectileColor whiteProjectile;
    [SerializeField] ProjectileColor redProjectile;
    [SerializeField] ProjectileColor blueProjectile;
    [SerializeField] ProjectileColor yellowProjectile;
    [SerializeField] ProjectileColor purpleProjectile;
    [SerializeField] ProjectileColor orangeProjectile;
    [SerializeField] ProjectileColor greenProjectile; 
    [SerializeField] ProjectileColor blackProjectile; 
    [SerializeField] ProjectileColor currentProjectileColor; 
    [SerializeField] Sprite whiteWizard;
    [SerializeField] Sprite redWizard;
    [SerializeField] Sprite blueWizard;
    [SerializeField] Sprite yellowWizard;
    [SerializeField] Sprite purpleWizard;
    [SerializeField] Sprite orangeWizard;
    [SerializeField] Sprite greenWizard;
    [SerializeField] Sprite blackWizard;

    private void Awake()
    {
        instance = this; 
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        colorState = new WhiteState();
        colorState.Enter();
    }

    public void SetWhite()
    {
        spriteRenderer.sprite = whiteWizard;
        currentProjectileColor = whiteProjectile;
    }
    public void SetRed()
    {
        spriteRenderer.sprite = redWizard;
        currentProjectileColor = redProjectile;

    }
    public void SetBlue()
    {
        spriteRenderer.sprite = blueWizard;
        currentProjectileColor = blueProjectile;

    }
    public void SetYellow()
    {
        spriteRenderer.sprite = yellowWizard; 
        currentProjectileColor = yellowProjectile;

    }

    public void SetGreen()
    {
        spriteRenderer.sprite = greenWizard;
        currentProjectileColor = greenProjectile;

    }
    public void SetOrange()
    {
        spriteRenderer.sprite = orangeWizard;
        currentProjectileColor = orangeProjectile;

    }
    public void SetPurple()
    {
        spriteRenderer.sprite = purpleWizard;
        currentProjectileColor = purpleProjectile;

    }
    public void SetBlack()
    {
        spriteRenderer.sprite = blackWizard;
        currentProjectileColor = blackProjectile;

    }

    public void Shoot(Vector3 projectileSpawnpoint, Quaternion projectileRotation, Vector2 aimDirection)
    {
        Transform spawnedProjectile = Instantiate(projectilePrefab, projectileSpawnpoint, projectileRotation);
        spawnedProjectile.GetComponent<Projectile>().ChangeProjectileColor(currentProjectileColor);
        spawnedProjectile.GetComponent<Rigidbody2D>().velocity = aimDirection * projectileSpeed;
        IState newState = colorState.ResetColor();

        if (!newState.Equals(colorState))
        {
            colorState.Exit();
            colorState = newState;
            colorState.Enter();
        }
    }

    public void AddBlue(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            IState newState = colorState.AddBlue();
            if (!newState.Equals(colorState))
            {
                colorState.Exit();
                colorState = newState;
                colorState.Enter();
            }
        }
    }
    public void AddRed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IState newState = colorState.AddRed();
            if (!newState.Equals(colorState))
            {
                colorState.Exit();
                colorState = newState;
                colorState.Enter();
            }
        }
    }
    public void AddYellow(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IState newState = colorState.AddYellow();
            if (!newState.Equals(colorState))
            {
                colorState.Exit();
                colorState = newState;
                colorState.Enter();
            }
        }
    }
}
