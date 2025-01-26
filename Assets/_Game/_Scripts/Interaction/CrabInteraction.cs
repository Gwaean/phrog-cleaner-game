using UnityEngine;

public class CrabInteraction : MonoBehaviour, IInteractable
{
  


    public void Interact()
    {
        GameManager.Instance.DecrementShellCount();
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
