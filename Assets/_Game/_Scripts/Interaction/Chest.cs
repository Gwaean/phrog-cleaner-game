using Animancer;
using FMODUnity;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public AnimationClip idleAnimation;
    [SerializeField] float reward;

    private AnimancerComponent animancerComponent;
    public EventReference openChest;

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
        RuntimeManager.PlayOneShot(openChest, transform.position);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<FadeOut>().FadeOutAnim();
    }
}