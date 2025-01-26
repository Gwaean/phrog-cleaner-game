using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using FMODUnity;
using System.Collections;

public class CleaningMechanic : MonoBehaviour
{
    public bool cleaning = false;
    //-----------------------

    [SerializeField]
    private Transform[] dirtList;

    private int cleaned = 0;

    public float progress = 0;
    //-----------------------

    [SerializeField] Image progressBar;
    private Coroutine fillAnim;
    [SerializeField] PlayMusic playMusic;
    [SerializeField] TextMeshProUGUI text;

    //--------------------------

    public static UnityEvent victory = new();

    private PlayerMovements playerMovements;
    public Volume postProcessing;
    [SerializeField] private ScreenSpaceLensFlare screenSpaceLensFlare;
    public EventReference cleanSound;

    void Awake()
    {
        playerMovements = GetComponent<PlayerMovements>();

        postProcessing.profile.TryGet(out ScreenSpaceLensFlare component);
        screenSpaceLensFlare = component;
        screenSpaceLensFlare.intensity.value = 30f;
    }

    void Start()
    {
        progressBar.fillAmount = progress;
    }

    void Update()
    {
        if (screenSpaceLensFlare != null)
        {
            screenSpaceLensFlare.intensity.value = Mathf.Lerp(30, 0, progress / 100);

            playMusic.ChangeParameter("Intensity", progress, true);
        }
    }

    public void OnClean(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            cleaning = true;
        }
        if (context.canceled)
        {
            cleaning = false;
        }

        playerMovements.HandlePlayerAnimations();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Dirt"))
        {
            if (cleaning)
            {
                other.GetComponent<BoxCollider2D>().enabled = false;
                other.GetComponent<FadeOut>().FadeOutAnim();
                cleaned++;
                progress = Mathf.RoundToInt((float)cleaned / dirtList.Length * 100);

                if (fillAnim != null) StopCoroutine(fillAnim);
                fillAnim = StartCoroutine(UpdateHUD());

                text.text = progress + "/100%";

                RuntimeManager.PlayOneShot(cleanSound, transform.position);

                if (progress >= 100)
                {
                    progress = 100;
                    victory.Invoke();
                }
            }
        }
    }

    IEnumerator UpdateHUD()
    {
        float elapsedTime = 0f;
        float startValue = progressBar.fillAmount;
        float targetFillAmount = progress / 100;

        while (elapsedTime < 0.5f /*duration*/)
        {
            progressBar.fillAmount = Mathf.Lerp(startValue, targetFillAmount, elapsedTime / 0.5f/*duration*/);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        progressBar.fillAmount = targetFillAmount;
    }
}