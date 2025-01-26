using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private Oxygen oxygenLogic;

     public int CollectedShell { get; private set; } = 0; 
    public TextMeshProUGUI shellCountText;

    public GameObject VictoryPanel;


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

    void OnEnable()
    {
        CleaningMechanic.victory.AddListener(WinScreen);
    }

    void OnDisable()
    {
        CleaningMechanic.victory.RemoveListener(WinScreen);
    }

    public void AddOxygen(float amount)
    {
        oxygenLogic.AddOxygen(amount);
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
    public void WinScreen()
    {
        Time.timeScale = 0;
        VictoryPanel.SetActive(true);
    }
}