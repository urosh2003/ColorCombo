using System.Collections;
using System.Collections.Generic;
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
        rigidbody.velocity = movementVecror * movementSpeed;
    }

    public void MoveInput(InputAction.CallbackContext context)
    {

        movementVecror = context.ReadValue<Vector2>();

    }
}
