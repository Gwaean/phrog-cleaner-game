using Animancer;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public AnimationClip idleAnimation;
    [SerializeField] float reward;

    private AnimancerComponent animancerComponent;

    void Awake()
    {
        animancerComponent = GetComponent<AnimancerComponent>();
    }

    void Start()
    {
        animancerComponent.Play(idleAnimation);
    }

    public void Interact()
    {
        GameManager.Instance.AddOxygen(reward);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}