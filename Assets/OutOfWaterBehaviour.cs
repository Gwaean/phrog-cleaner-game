using UnityEngine;

public class OutOfWaterBehaviour : MonoBehaviour
{
    public float rechargeAmount = 0.2f;
    private Oxygen oxygen;

    void Start()
    {
        oxygen = GameObject.FindGameObjectWithTag("Player").GetComponent<Oxygen>();
    }
    void OnTriggerStay2D()
    {
        oxygen.AddOxygen(rechargeAmount);
        Debug.Log("breathing");
    }
}
