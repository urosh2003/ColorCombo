using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyColor enemyColor;
    private SpriteRenderer spriteRenderer;
       

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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