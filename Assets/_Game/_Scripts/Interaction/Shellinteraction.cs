using UnityEngine;
using TMPro;
public class ShellInteraction : MonoBehaviour, IInteractable
{
    public static int CollectedShell = 0;
    public static TextMeshProUGUI text;

    void Start()
    {
        text.text = "0";
    }
    public void Interact()
    {
        CollectedShell++;
        text.text = CollectedShell.ToString();

        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
    public static void DecrementShellCount()
    {
        if (CollectedShell > 0)
        {
            CollectedShell--;
        }
    }

    // Update is called once per frame
    public int GetCollected()
    {
        return CollectedShell;
    }
}
