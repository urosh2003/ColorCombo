using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyColor enemyColor;
    [SerializeField] private Sprite hitSprite;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody;
    private Vector2 movementDirection;
    private float movementSpeed;
    [SerializeField] private float movementSpeedLow;
    [SerializeField] private float movementSpeedHigh;
    [SerializeField] private float deadSpriteDuration;
    private Animator animator;
    private EnemyType type;
    private CapsuleCollider2D enemyCollider;


    private void Update()
    {
        movementDirection = PlayerManager.instance.transform.position - transform.position;
        rigidbody.velocity = movementDirection.normalized * movementSpeed;

        if (movementDirection.x > 0)
            spriteRenderer.flipX = false;
        else if (movementDirection.x < 0)
            spriteRenderer.flipX = true;
    }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        enemyCollider = GetComponent<CapsuleCollider2D>();
    }

    public void SetEnemy(EnemyColor enemyColor, EnemyType enemyType)
    {
        this.enemyColor = enemyColor;
        type = enemyType;
        enemyCollider.offset = enemyType.colliderOffset;
        enemyCollider.size = enemyType.colliderSize;

        switch (this.enemyColor.color)
        {
            case WizardColor.BLUE:
                animator.runtimeAnimatorController = type.blueAnimator;
                break;
            case WizardColor.YELLOW:
                animator.runtimeAnimatorController = type.yellowAnimator;
                break;
            case WizardColor.RED:
                animator.runtimeAnimatorController = type.redAnimator;
                break;
            case WizardColor.GREEN:
                animator.runtimeAnimatorController = type.greenAnimator;
                break;
            case WizardColor.ORANGE:
                animator.runtimeAnimatorController = type.orangeAnimator;
                break;
            case WizardColor.PURPLE:
                animator.runtimeAnimatorController = type.purpleAnimator;
                break;
        }

        movementSpeed = Random.Range(type.minSpeed, type.maxSpeed);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile == null) 
        {
            return;
        }
        if (projectile.projectileColor.color == enemyColor.color)
        {
            GameManager.instance.EnemyDied(enemyColor.color, type.pointsWorth);
            Dead();
        }
        else if(projectile.projectileColor.color == WizardColor.ALL)
        {
            GameManager.instance.EnemyDied(type.pointsWorth);
            Dead();
        }
        else
        {
            GameManager.instance.HitFailed();
        }
    }

    void Dead()
    {
        movementSpeed = 0;
        enemyCollider.enabled = false;
        animator.SetBool("Dead", true);
        StartCoroutine(DisplayHitFrame());
    }

    private IEnumerator DisplayHitFrame()
    {
        yield return new WaitForSecondsRealtime(deadSpriteDuration);
        Destroy(gameObject);
    }
}