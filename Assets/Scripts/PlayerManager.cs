using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum WizardColor { WHITE, RED, YELLOW, BLUE, ORANGE, PURPLE, GREEN, BLACK, ALL }

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private SpriteRenderer staffSpriteRenderer;
    private IState colorState;
    private bool isSuperReady;

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
    [SerializeField] ProjectileColor superProjectile; 
    [SerializeField] ProjectileColor currentProjectileColor; 
    [SerializeField] Sprite whiteStaff;
    [SerializeField] Sprite redStaff;
    [SerializeField] Sprite blueStaff;
    [SerializeField] Sprite yellowStaff;
    [SerializeField] Sprite purpleStaff;
    [SerializeField] Sprite orangeStaff;
    [SerializeField] Sprite greenStaff;
    [SerializeField] Sprite blackStaff;

    private HashSet<WizardColor> superColors;

    public event Action SuperUsed;
    
    private Animator animator;

    private void Awake()
    {
        instance = this; 
        animator = GetComponentInChildren<Animator>();
        staffSpriteRenderer = GetComponentsInChildren<SpriteRenderer>()[1];
        colorState = new WhiteState();
        colorState.Enter();
        GameManager.instance.ColorEnemyFell += AddSuperColor;
        isSuperReady = true;
        superColors = new HashSet<WizardColor>();
    }

    private void AddSuperColor(WizardColor color)
    {
        superColors.Add(color);
        if (superColors.Count >= 6)
        {
            isSuperReady = true;
        }
    }

    public void SetColor(WizardColor color)
    {
        this.animator.SetInteger("wizardColor", (int) color);
        switch (color)
        {
            case WizardColor.WHITE: 
                currentProjectileColor = whiteProjectile;
                staffSpriteRenderer.sprite = whiteStaff;
                break;
            case WizardColor.BLACK:
                currentProjectileColor = blackProjectile;
                staffSpriteRenderer.sprite = blackStaff;
                break;
            case WizardColor.BLUE: 
                currentProjectileColor = blueProjectile;
                staffSpriteRenderer.sprite = blueStaff;
                break;
            case WizardColor.YELLOW:
                currentProjectileColor = yellowProjectile;
                staffSpriteRenderer.sprite = yellowStaff;
                break;
            case WizardColor.RED: 
                currentProjectileColor = redProjectile;
                staffSpriteRenderer.sprite = redStaff;
                break;
            case WizardColor.GREEN:
                currentProjectileColor = greenProjectile;
                staffSpriteRenderer.sprite = greenStaff;
                break;
            case WizardColor.ORANGE:
                currentProjectileColor = orangeProjectile;
                staffSpriteRenderer.sprite = orangeStaff;
                break;
            case WizardColor.PURPLE:
                currentProjectileColor = purpleProjectile;
                staffSpriteRenderer.sprite = purpleStaff;
                break;
        }
    }

    public void Shoot(Vector3 projectileSpawnpoint, Quaternion projectileRotation, Vector2 aimDirection)
    {
        Transform spawnedProjectile = Instantiate(projectilePrefab, projectileSpawnpoint, projectileRotation);
        spawnedProjectile.GetComponent<Projectile>().ChangeProjectileColor(currentProjectileColor);
        spawnedProjectile.GetComponent<Rigidbody2D>().velocity = aimDirection.normalized * projectileSpeed;
        IState newState = colorState.ResetColor();

        if (!newState.Equals(colorState))
        {
            colorState.Exit();
            colorState = newState;
            colorState.Enter();
        }
    }

    public void ShootSuper(Vector3 projectileSpawnpoint, Quaternion projectileRotation, Vector2 aimDirection)
    {
        if(!isSuperReady)
        {
            return;
        }
        
        Transform spawnedProjectile = Instantiate(projectilePrefab, projectileSpawnpoint, projectileRotation);
        spawnedProjectile.GetComponent<Projectile>().ChangeProjectileColor(superProjectile);
        spawnedProjectile.GetComponent<Rigidbody2D>().velocity = aimDirection.normalized * projectileSpeed;
        spawnedProjectile.localScale = new Vector3(4, 4, 1);

        isSuperReady = false;
        superColors.Clear();
        SuperUsed?.Invoke();
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.instance.GameOver();
        gameObject.SetActive(false);
    }

    public void OnDestroy()
    {
        GameManager.instance.ColorEnemyFell -= AddSuperColor;
    }
}
