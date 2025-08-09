using UnityEngine;

public class Chest : MonoBehaviour
{
    public int chestNumber;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            GameManager.instance.ChestPickedUp(chestNumber);
            Destroy(gameObject);
        }
    }
}