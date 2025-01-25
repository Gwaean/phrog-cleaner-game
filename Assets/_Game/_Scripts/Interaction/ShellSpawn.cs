using UnityEngine;

public class ShellSpawn : MonoBehaviour
{
 public GameObject ShellPrefab;
    // Update is called once per frame
    void Update()
    {
        Vector2 Shellposition = new Vector2(,);
        Instantiate(ShellPrefab,Shellposition,Quaternion.identity);
    }
}
