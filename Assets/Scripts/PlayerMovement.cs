using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private Vector2 movementVecror;
    private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector2 intendedVelocity = movementVecror * movementSpeed;
        float xVel = intendedVelocity.x;
        float yVel = intendedVelocity.y;

        if (transform.position.x >= 8.5 && xVel > 0) xVel = 0;
        if (transform.position.x <= -8.5 && xVel < 0) xVel = 0;
        if (transform.position.y >= 4.5f && yVel > 0) yVel = 0;
        if (transform.position.y <= -4.5f && yVel < 0) yVel = 0;

        rigidbody.velocity = new Vector2(xVel, yVel);
    }

    public void MoveInput(InputAction.CallbackContext context)
    {

        movementVecror = context.ReadValue<Vector2>();

    }
}
