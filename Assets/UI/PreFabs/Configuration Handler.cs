using UnityEngine;
using UnityEngine.UI;

public class ConfigurationHandler : PanelHandler
{
    void Awake()
    {
        DontDestroyOnLoad(m_panel);
    }
}
