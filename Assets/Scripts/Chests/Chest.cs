using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public int chestNumber;
    [SerializeField] ParticleSystem pickedUp;
    [SerializeField] GameObject idle;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {

            GameManager.instance.ChestPickedUp(chestNumber);
            StartCoroutine(DisplayImpactParticles());
        }
    }

    private IEnumerator DisplayImpactParticles()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        idle.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;
        pickedUp.Play();
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(gameObject);
    }
}