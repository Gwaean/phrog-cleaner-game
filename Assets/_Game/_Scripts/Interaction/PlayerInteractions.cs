using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    private IInteractable interactable;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started && interactable != null)
        {
            interactable.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        interactable = other.GetComponent<IInteractable>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        interactable = other.GetComponent<IInteractable>();
    }
}