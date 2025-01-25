using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class CleaningMechanic : MonoBehaviour
{
    bool cleaning = false;
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

    void Start()
    {
        progressBar.fillAmount = progress;
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
                playMusic.ChangeParameter("Intensity", progress);
                UpdateHUD();

                if (progress >= 100)
                    victory.Invoke();
            }
        }
    }
    private void UpdateHUD()
    {
        progressBar.fillAmount = (float)cleaned / dirtList.Length;
        text.text = progress + "/100%";
    }
}