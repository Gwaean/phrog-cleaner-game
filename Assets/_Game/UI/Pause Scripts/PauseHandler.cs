using UnityEngine;
using UnityEngine.UI;

public class PauseHandler : PanelHandler
{
    void OnEnable()
    {
        ButtonSelection.instance.SetPanelChanged (true);
        PrimaryButton.Select();
    }

    void OnDisable()
    {
        ButtonSelection.instance.ApplyChanges();
    }
}
