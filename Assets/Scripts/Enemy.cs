using System.Collections;
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
    private AudioSource audioSource;
    private Animator animator;
    [SerializeField] AnimatorOverrideController blueAnimator;
    [SerializeField] AnimatorOverrideController redAnimator;
    [SerializeField] AnimatorOverrideController yellowAnimator;
    [SerializeField] AnimatorOverrideController greenAnimator;
    [SerializeField] AnimatorOverrideController orangeAnimator;
    [SerializeField] AnimatorOverrideController purpleAnimator;


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
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    public void SetEnemyColor(EnemyColor enemyColor, float currentSpawnRate)
    {
        this.enemyColor = enemyColor;
        switch (this.enemyColor.color)
        {
            case WizardColor.BLUE:
                animator.runtimeAnimatorController = blueAnimator;
                break;
            case WizardColor.YELLOW:
                animator.runtimeAnimatorController = yellowAnimator;
                break;
            case WizardColor.RED:
                animator.runtimeAnimatorController = redAnimator;
                break;
            case WizardColor.GREEN:
                animator.runtimeAnimatorController = greenAnimator;
                break;
            case WizardColor.ORANGE:
                animator.runtimeAnimatorController = orangeAnimator;
                break;
            case WizardColor.PURPLE:
                animator.runtimeAnimatorController = purpleAnimator;
                break;
        }
        movementSpeed = Random.Range(movementSpeedLow - currentSpawnRate, movementSpeedHigh - 2*currentSpawnRate);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile.projectileColor.color == enemyColor.color)
        {
            GameManager.instance.EnemyDied(enemyColor.color);
            Destroy(collision.gameObject);
            Dead();
        }

        if(projectile.projectileColor.color == WizardColor.ALL)
        {
            GameManager.instance.EnemyDied();
            Dead();
        }
        else
        {
            Destroy(collision.gameObject);
            audioSource.Play();
        }
    }

    void Dead()
    {
        movementSpeed = 0;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        animator.StopPlayback();
        spriteRenderer.sprite = hitSprite;
        StartCoroutine(DisplayHitFrame());
    }

    private IEnumerator DisplayHitFrame()
    {
        yield return new WaitForSecondsRealtime(deadSpriteDuration);
        Destroy(gameObject);
    }
}