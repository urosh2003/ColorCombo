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

    private void Update()
    {
        movementDirection = PlayerManager.instance.transform.position - transform.position;
        rigidbody.velocity = movementDirection.normalized * movementSpeed;
    }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetEnemyColor(EnemyColor color, float currentSpawnRate)
    {
        enemyColor = color;
        spriteRenderer.sprite = enemyColor.sprite;
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
        }
    }

    void Dead()
    {
        movementSpeed = 0;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        spriteRenderer.sprite = hitSprite;
        StartCoroutine(DisplayHitFrame());
    }

    private IEnumerator DisplayHitFrame()
    {
        yield return new WaitForSecondsRealtime(deadSpriteDuration);
        Destroy(gameObject);
    }
}