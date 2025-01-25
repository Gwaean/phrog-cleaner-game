using UnityEngine;

public class CrabInteraction : MonoBehaviour, IInteractable
{
  

    // Update is called once per frame
    public void Interact()
    {
        ShellInteraction.DecrementShellCount();
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
