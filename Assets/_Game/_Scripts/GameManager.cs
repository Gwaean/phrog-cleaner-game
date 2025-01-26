using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private Oxygen oxygenLogic;
     public int CollectedShell { get; private set; } = 0; 
    public TextMeshProUGUI shellCountText;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (oxygenLogic == null)
        {
            oxygenLogic = FindAnyObjectByType<Oxygen>();
        }
    }

    public void AddOxygen(float amount)
    {
        oxygenLogic.AddOxygen(amount);
    }

    public void GetOxygenAmount()
    {
        oxygenLogic.ReturnAmount();
    }
    public void IncrementShellCount()
    {
        CollectedShell++;
        UpdateShellUI();
        Debug.Log("Shell count incremented: " + CollectedShell);
    }
    public void DecrementShellCount()
    {
        if (CollectedShell > 0)
        {
            CollectedShell--;
            UpdateShellUI();
            Debug.Log("Shell count decremented: " + CollectedShell);
        }
        else
        {
            Debug.Log("No shells left to decrement!");
        }
    }

    // Update the shell count UI
    private void UpdateShellUI()
    {
        if (shellCountText != null)
        {
            shellCountText.text = CollectedShell.ToString();
        }
        else
        {
            Debug.LogWarning("Shell count UI text is not assigned in the GameManager.");
        }
    }
}