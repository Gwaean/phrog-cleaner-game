using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;

public class Oxygen : MonoBehaviour
{
    public float totalOxygen = 100;
    [SerializeField] float loss;
    [SerializeField] float interval;
    [SerializeField] Sprite[] states;

    public GameObject HUD;

    void Start()
    {
        InvokeRepeating(nameof(LoseOxygen), interval, interval);
    }

    void LoseOxygen()
    {
        totalOxygen -= loss;
        //change sprite
        switch(totalOxygen)
        {
            case 75:
            HUD.GetComponent<Image>().sprite = states[1];
            break;
            case 50:
            HUD.GetComponent<Image>().sprite = states[2];
            break;
            case 25:
            HUD.GetComponent<Image>().sprite = states[3];
            break;
        }
        //verify end of the game
        if (totalOxygen < 0)
        {
            totalOxygen = 0;
            CleaningMechanic.victory.Invoke();
        }
    }

    public void AddOxygen(float amount)
    {
        totalOxygen += amount;
        if (totalOxygen > 100) totalOxygen = 100;

        switch(totalOxygen)
        {
            case 50:
            HUD.GetComponent<Image>().sprite = states[2];
            break;
            case 75:
            HUD.GetComponent<Image>().sprite = states[1];
            break;
            case 100:
            HUD.GetComponent<Image>().sprite = states[0];
            break;
        }
    }
    public float ReturnAmount()
    {
        return totalOxygen;
    }
}