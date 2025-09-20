using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.SceneManagement;

//TODO: reajustar e testar o consumo de oxigenio 
//DOING: mudar valor  de consumo, falta testar e ver como se sai
public class Oxygen : MonoBehaviour
{
    public float totalOxygen = 100;
    [SerializeField] float ConsumptionRate = 1.5f;
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

    void Start() // ao iniciar, ele  seta a barra e comeca a perder oxigenio
    {
        oxygenBar.fillAmount = totalOxygen / 100;
        InvokeRepeating(nameof(LoseOxygen), interval, interval); // usa a funcao loseoxygen a cada intervalo
    }

    void Update()
    {
        playMusic.ChangeParameter("Oxygen", totalOxygen, false);
    }

    void LoseOxygen()
    {
        totalOxygen -= ConsumptionRate; // nao sei quanto ele perde, imagino que de 1 em 1? nao ta setado o loss em lugar algum, ele so é chamado no comeco ??

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
        oxygenBar.fillAmount = totalOxygen / 100;

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