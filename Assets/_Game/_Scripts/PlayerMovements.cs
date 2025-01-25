using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovements: MonoBehaviour
{
    [SerializeField] private float speed = 5.0f; // Horizontal and vertical movement speed
    [SerializeField] private float upwardSlowFactor = 0.5f; // Factor to slow upward movement
    private Rigidbody2D body;
    private Vector2 inputMovement; // Stores input values from the new system

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Called by the Input System when the "Move" action is performed
    public void OnMove(InputAction.CallbackContext context)
    {
        inputMovement = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Apply movement based on input
        float horizontal = inputMovement.x;
        float vertical = inputMovement.y;

        // Horizontal and vertical movement
       body.linearVelocity = new Vector2(horizontal * speed, body.linearVelocity.y);

        // Upward movement
        if (vertical > 0)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, vertical * speed);
        }

        // Slow upward velocity if not actively moving up
        if (vertical <= 0 && body.linearVelocity.y > 0)
        {
           body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y * upwardSlowFactor);
        }

        // Flipping player horizontally
        if (horizontal > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontal < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
