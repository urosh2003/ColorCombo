using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private ProjectileColor projectileColor;
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


}