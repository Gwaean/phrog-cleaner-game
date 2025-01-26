using UnityEngine;
using UnityEngine.UI;

public class VictorySelectButton : MonoBehaviour
{
    [SerializeField] protected Button PrimaryButton;

    public void OnEnable()
    {
        Open();
    }

    public void Open()
    {
        PrimaryButton.Select();
    }

}
