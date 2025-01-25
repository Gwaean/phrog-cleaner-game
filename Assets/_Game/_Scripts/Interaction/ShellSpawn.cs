using UnityEngine;

public class ShellSpawn : MonoBehaviour
{
    public GameObject ShellPrefab;
    private int sorteador;

    void Start()
    {

    }

    void Update()
    {
      //Vector2 Shellposition = new Vector2(,);
        Instantiate(ShellPrefab, Shellposition, Quaternion.identity);
    }
}
