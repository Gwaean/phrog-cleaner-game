using Animancer;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f; // Horizontal and vertical movement speed
    [SerializeField] private float upwardSlowFactor = 0.5f; // Factor to slow upward movement
    private Rigidbody2D body;
    private Vector2 inputMovement;
    public AnimancerComponent animancer;
    public AnimationClip idle;
    public AnimationClip swimming;
    public AnimationClip swimmingUp;
    public AnimationClip cleaningIdle;
    public AnimationClip cleaningSwimming;
    public AnimationClip cleaningSwimmingUp;
    private CleaningMechanic cleaningMechanic;

    private bool _isSwimming;
    public bool IsSwimming
    {
        get { return _isSwimming; }
        set
        {
            if (IsSwimming != value)
            {
                _isSwimming = value;

                HandlePlayerAnimations();
            }
        }
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animancer = GetComponent<AnimancerComponent>();
        cleaningMechanic = GetComponent<CleaningMechanic>();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        inputMovement = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        float horizontal = inputMovement.x;
        float vertical = inputMovement.y;

        IsSwimming = inputMovement != Vector2.zero;

        body.linearVelocity = new Vector2(horizontal * speed, body.linearVelocity.y);

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

    public void HandlePlayerAnimations()
    {
        if (!cleaningMechanic.cleaning)
        {
            if (IsSwimming)
            {
                bool isGoingUp = inputMovement.y > 0;
                bool isGoingDown = inputMovement.y < 0;

                if (isGoingUp) animancer.Play(swimmingUp);
                else if (!isGoingDown) animancer.Play(swimming);
            }
            else
            {
                animancer.Play(idle);
            }
        }
        else
        {
            if (IsSwimming)
            {
                bool isGoingUp = inputMovement.y > 0;
                bool isGoingDown = inputMovement.y < 0;

                if (isGoingUp) animancer.Play(cleaningSwimmingUp);
                else if (!isGoingDown) animancer.Play(cleaningSwimming);
            }
            else
            {
                animancer.Play(cleaningIdle);
            }
        }
    }
}
