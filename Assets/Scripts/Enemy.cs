using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyColor enemyColor;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody;
    private Vector2 movementDirection;
    private float movementSpeed;
    [SerializeField] private float movementSpeedLow;
    [SerializeField] private float movementSpeedHigh;

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

    public void SetEnemyColor(EnemyColor color)
    {
        enemyColor = color;
        spriteRenderer.sprite = enemyColor.sprite;
        movementSpeed = Random.Range(1f, 2.5f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile.projectileColor.color == enemyColor.color)
        {
            GameManager.instance.EnemyDied(enemyColor.color);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if(projectile.projectileColor.color == WizardColor.ALL)
        {
            GameManager.instance.EnemyDied();
            Destroy(gameObject);
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}