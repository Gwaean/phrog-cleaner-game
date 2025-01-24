using UnityEngine;
using UnityEngine.InputSystem;
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

    [SerializeField]
    private TextMeshProUGUI progressText;

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

                progressText.text = progress + "%";
            }
        }
    }
}