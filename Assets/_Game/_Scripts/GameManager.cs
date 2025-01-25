using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private Oxygen oxygenLogic;

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
}