using UnityEngine;
using UnityEngine.UI;

public class PanelHandler : MonoBehaviour
{
    [SerializeField] protected GameObject m_panel;
    [SerializeField] protected Button PrimaryButton;
    [SerializeField] public bool AcceptAnyCancelKey = false;

    public void Open()
    {
        m_panel.SetActive (true);
        ButtonSelection.instance.SetPanelChanged (true);
        PrimaryButton.Select();
    }

    //default behaviour is to call OnPanel(), can use UpdatePanel to
    //override for inherited classes
    virtual public void Update ()
    {
        OnPanel();
        UpdatePanel();
        if (PressedCancel())
        {
            Close();
        }
    }

    bool PressedCancel()
    {
        return Input.GetButtonDown("Cancel") || (AcceptAnyCancelKey && Input.anyKey);
    }

    virtual public void UpdatePanel(){}

    private void OnPanel()
    {
        ButtonSelection.instance.SetPanelChanged (false);
    }

    public void Close()
    {
        m_panel.SetActive (false);
        ButtonSelection.instance.ApplyChanges();
    }
}

