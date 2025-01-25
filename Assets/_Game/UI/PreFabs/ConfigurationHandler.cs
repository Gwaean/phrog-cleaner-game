using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConfigurationHandler : PanelHandler
{
    [SerializeField] public GameObject m_returnToButton;

    void Awake()
    {
        DontDestroyOnLoad(m_panel);
        DontDestroyOnLoad(this);
    }

    override public void Close()
    {

        m_panel.SetActive (false);
        ButtonSelection.instance.ApplyChanges();
        ButtonSelection.instance.SetSelectionToButton(m_returnToButton);
    }
}