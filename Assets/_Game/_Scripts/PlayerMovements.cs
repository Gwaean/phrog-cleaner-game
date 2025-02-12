using Animancer;
using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovements : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f; // Horizontal and vertical movement speed
    [SerializeField] private float upwardSlowFactor = 0.5f; // Factor to slow upward movement
    private Rigidbody2D body;
    private Vector2 inputMovement;
    public AnimancerComponent animancer;
    public InputSystem_Actions inputActions;
    public AnimationClip idle;
    public AnimationClip swimming;
    public AnimationClip swimmingUp;
    public AnimationClip cleaningIdle;
    public AnimationClip cleaningSwimming;
    public AnimationClip cleaningSwimmingUp;
    
    private CleaningMechanic cleaningMechanic;
    private PlayerInteractions playerInteractions;

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

    private bool _isFlipped;
    public bool IsFlipped
    {
        get { return _isFlipped; }
        set
        {
            if (IsFlipped != value)
            {
                _isFlipped = value;
                RuntimeManager.PlayOneShot(changeDirectionSound, transform.position);
            }
        }
    }

    public EventReference changeDirectionSound;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animancer = GetComponent<AnimancerComponent>();
        cleaningMechanic = GetComponent<CleaningMechanic>();
        playerInteractions = GetComponent<PlayerInteractions>();

        inputActions = new();
        inputActions.Enable();
    }

    void OnEnable()
    {
        inputActions.Player.Move.started += OnMove;
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;

        inputActions.Player.Clean.started += cleaningMechanic.OnClean;
        inputActions.Player.Clean.canceled += cleaningMechanic.OnClean;
        inputActions.Player.Clean.performed += cleaningMechanic.OnClean;

        inputActions.Player.Interact.started += playerInteractions.OnInteract;
        inputActions.Player.Interact.canceled += playerInteractions.OnInteract;
        inputActions.Player.Interact.performed += playerInteractions.OnInteract;
    }

    void OnDisable()
    {
        inputActions.Player.Move.started -= OnMove;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;

        inputActions.Player.Clean.started -= cleaningMechanic.OnClean;
        inputActions.Player.Clean.canceled -= cleaningMechanic.OnClean;
        inputActions.Player.Clean.performed -= cleaningMechanic.OnClean;

        inputActions.Player.Interact.started -= playerInteractions.OnInteract;
        inputActions.Player.Interact.canceled -= playerInteractions.OnInteract;
        inputActions.Player.Interact.performed -= playerInteractions.OnInteract;

        inputActions.Disable();
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
        {
            transform.localScale = Vector3.one;
            IsFlipped = false;
        }
        else if (horizontal < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            IsFlipped = true;
        }
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
