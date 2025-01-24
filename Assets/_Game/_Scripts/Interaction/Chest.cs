using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{

    [SerializeField] float reward;

    public void Interact()
    {
        GameManager.Instance.AddOxygen(reward);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}