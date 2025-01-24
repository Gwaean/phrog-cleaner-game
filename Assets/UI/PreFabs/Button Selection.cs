using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelection : MonoBehaviour
{
    //singleton
    public static ButtonSelection instance;
    void Awake()
    {
       instance = this;
       DontDestroyOnLoad(instance);
    }
    //...

    public EventSystem eventSystem; //drag on Editor
    private GameObject lastSelected;
    private GameObject currentSelected;
    private GameObject lastPanelSelection;
    private bool m_changedPanel = false;

    void Update()
    {
        UpdateSelection();
    }
    
    private void UpdateSelection() {

        if (eventSystem.currentSelectedGameObject != currentSelected)
        {
            lastSelected = currentSelected;
            currentSelected = eventSystem.currentSelectedGameObject;
        }
        else if (m_changedPanel)
        {
            lastPanelSelection = lastSelected;
        }
    }

    public void SetPanelChanged(bool value)
    {
        m_changedPanel = value;
    }

    private void SetSelectionToLastSelected ()
    {
        eventSystem.SetSelectedGameObject(lastPanelSelection);
    }

    public void ApplyChanges()
    {
        SetPanelChanged(true);
        SetSelectionToLastSelected();
    }

}
