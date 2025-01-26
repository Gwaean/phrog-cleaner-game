using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using FMODUnity;

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
                other.gameObject.SetActive(false);
                cleaned++;
                progress = Mathf.RoundToInt((float)cleaned / dirtList.Length * 100);
                UpdateHUD();

                RuntimeManager.PlayOneShot(cleanSound, transform.position);

                if (progress >= 100)
                {
                    victory.Invoke();
                }
            }
        }
    }
    private void UpdateHUD()
    {
        if (progress > 100) progress = 100;
        progressBar.fillAmount = (float)cleaned / dirtList.Length;
        text.text = progress + "/100%";
    }
}