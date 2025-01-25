using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private Oxygen oxygenLogic;
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

    public void GetOxygenAmount()
    {
        oxygenLogic.ReturnAmount();
    }

    public void WinScreen()
    {
        VictoryPanel.SetActive(true);
    }
}