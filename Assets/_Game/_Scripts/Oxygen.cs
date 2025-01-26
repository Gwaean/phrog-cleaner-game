using UnityEngine;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;

public class Oxygen : MonoBehaviour
{
    public float totalOxygen = 100;
    [SerializeField] float loss;
    [SerializeField] float interval;
    [SerializeField] TextMeshProUGUI text;

    void Start()
    {
        if (text != null) text.text = totalOxygen + "%";
        InvokeRepeating(nameof(LoseOxygen), interval, interval);
    }

    void LoseOxygen()
    {
        totalOxygen -= loss;
        if (totalOxygen < 0)
        {
            totalOxygen = 0;
            CleaningMechanic.victory.Invoke();
        }
        if (text != null) text.text = totalOxygen + "%";
    }

    public void AddOxygen(float amount)
    {
        totalOxygen += amount;
        if (totalOxygen > 100) totalOxygen = 100;
        if (text != null) text.text = totalOxygen + "%";
    }
    public float ReturnAmount()
    {
        return totalOxygen;
    }
}