using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileColor projectileColor;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void ChangeProjectileColor(ProjectileColor color)
    {
        projectileColor = color;
        spriteRenderer.sprite = projectileColor.sprite;
    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            GameManager.instance.HitFailed();
            Destroy(gameObject);
        }
    }
}