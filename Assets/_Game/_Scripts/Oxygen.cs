using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.SceneManagement;

public class Oxygen : MonoBehaviour
{
    public float totalOxygen = 100;
    [SerializeField] float loss;
    [SerializeField] float interval;
    [SerializeField] Sprite[] states;
    [SerializeField] PlayMusic playMusic;
    [SerializeField] Image oxygenBar;

    public GameObject HUD;
    private Image image;

    void Awake()
    {
        image = HUD.GetComponent<Image>();
    }

    void Start()
    {
        oxygenBar.fillAmount = totalOxygen/100;
        InvokeRepeating(nameof(LoseOxygen), interval, interval);
    }

    void Update()
    {
        playMusic.ChangeParameter("Oxygen", totalOxygen, false);
    }

    void LoseOxygen()
    {
        totalOxygen -= loss;

        UpdateOxygenSprite();
    }

    public void AddOxygen(float amount)
    {
        totalOxygen += amount;
        if (totalOxygen > 100) totalOxygen = 100;

        UpdateOxygenSprite();
    }

    public void UpdateOxygenSprite()
    {
        oxygenBar.fillAmount = totalOxygen/100;

        if (totalOxygen >= 75)
            image.sprite = states[0];
        else if (totalOxygen >= 50)
            image.sprite = states[1];
        else if (totalOxygen >= 25)
            image.sprite = states[2];
        else
            image.sprite = states[3];

        if (totalOxygen <= 0)
        {
            totalOxygen = 0;
            CancelInvoke(nameof(LoseOxygen));
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public float ReturnAmount()
    {
        return totalOxygen;
    }
}