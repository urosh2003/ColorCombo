using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyColor enemyColor;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody;
    private Vector2 movementDirection;
    [SerializeField] private float movementSpeed;

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
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile.projectileColor.color == enemyColor.color)
        { 
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        Destroy(collision.gameObject);
    }
}