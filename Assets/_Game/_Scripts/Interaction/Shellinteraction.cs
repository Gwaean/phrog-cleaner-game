using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class ShellInteraction : MonoBehaviour, IInteractable
{
    public static int CollectedShell = 0;
    public static TextMeshProUGUI text;


    public void Interact()
    {
        GameManager.Instance.IncrementShellCount();
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject);
    }
   
}
