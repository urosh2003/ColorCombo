using System.Collections;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileColor projectileColor;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject whiteParticles;
    [SerializeField] private GameObject redParticles;
    [SerializeField] private GameObject blueParticles;
    [SerializeField] private GameObject yellowParticles;
    [SerializeField] private GameObject greenParticles;
    [SerializeField] private GameObject orangeParticles;
    [SerializeField] private GameObject purpleParticles;
    [SerializeField] private GameObject blackParticles;

    [SerializeField] GameObject whiteImpact;
    [SerializeField] GameObject redImpact;
    [SerializeField] GameObject blueImpact;
    [SerializeField] GameObject yellowImpact;
    [SerializeField] GameObject greenImpact;
    [SerializeField] GameObject orangeImpact;
    [SerializeField] GameObject purpleImpact;
    [SerializeField] GameObject blackImpact;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void ChangeProjectileColor(ProjectileColor color)
    {
        projectileColor = color;
        spriteRenderer.sprite = projectileColor.sprite;
        switch (projectileColor.color)
        {
            case WizardColor.WHITE: whiteParticles.SetActive(true); break;
            case WizardColor.BLUE: blueParticles.SetActive(true); break;
            case WizardColor.YELLOW: yellowParticles.SetActive(true); break;
            case WizardColor.RED: redParticles.SetActive(true); break;
            case WizardColor.ORANGE: orangeParticles.SetActive(true); break;
            case WizardColor.GREEN: greenParticles.SetActive(true); break;
            case WizardColor.PURPLE: purpleParticles.SetActive(true); break;
            case WizardColor.BLACK: blackParticles.SetActive(true); break;
            case WizardColor.ALL:
                yellowParticles.SetActive(true); 
                redParticles.SetActive(true);
                blueParticles.SetActive(true); 
                orangeParticles.SetActive(true); 
                greenParticles.SetActive(true);
                purpleParticles.SetActive(true);
                break;

        }
    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            return;
        }

        if (collision.gameObject.layer == 6)
        {
            GameManager.instance.HitFailed();
            
            StartCoroutine(DisplayImpactParticles());
        }
        else if (projectileColor.color != WizardColor.ALL)
        {
            StartCoroutine(DisplayImpactParticles());
        }
    }

    private IEnumerator DisplayImpactParticles()
    {
        spriteRenderer.enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<CircleCollider2D>().enabled = false;
        switch (projectileColor.color)
        {
            case WizardColor.WHITE:
                whiteImpact.SetActive(true);
                whiteImpact.GetComponent<ParticleSystem>().Play();
                break;
            case WizardColor.BLUE: 
                blueImpact.SetActive(true);
                blueImpact.GetComponent<ParticleSystem>().Play();
                break;
            case WizardColor.YELLOW:
                yellowImpact.SetActive(true);
                yellowImpact.GetComponent<ParticleSystem>().Play();
                break;
            case WizardColor.RED: 
                redImpact.SetActive(true);
                redImpact.GetComponent<ParticleSystem>().Play();
                break;
            case WizardColor.ORANGE: 
                orangeImpact.SetActive(true);
                orangeImpact.GetComponent<ParticleSystem>().Play();
                break;
            case WizardColor.GREEN:
                greenImpact.SetActive(true);
                greenImpact.GetComponent<ParticleSystem>().Play();
                break;
            case WizardColor.PURPLE:
                purpleImpact.SetActive(true);
                purpleImpact.GetComponent<ParticleSystem>().Play();
                break;
            case WizardColor.BLACK:
                blackImpact.SetActive(true);
                blackImpact.GetComponent<ParticleSystem>().Play();
                break;
            case WizardColor.ALL:
                blueImpact.SetActive(true);
                redImpact.SetActive(true);
                yellowImpact.SetActive(true);
                orangeImpact.SetActive(true);
                greenImpact.SetActive(true);
                purpleImpact.SetActive(true);
                blueImpact.GetComponent<ParticleSystem>().Play();
                redImpact.GetComponent<ParticleSystem>().Play();
                yellowImpact.GetComponent<ParticleSystem>().Play();
                greenImpact.GetComponent<ParticleSystem>().Play();
                purpleImpact.GetComponent<ParticleSystem>().Play();
                orangeImpact.GetComponent<ParticleSystem>().Play();
                break;
        }
        yield return new WaitForSecondsRealtime(0.15f);
        Destroy(gameObject);
    }
}