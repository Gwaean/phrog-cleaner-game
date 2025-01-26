using UnityEngine;
using System.Collections.Generic;


public class ShellSpawn : MonoBehaviour
{
    public GameObject ShellPrefab; 
    private int sorteador;
    public Transform[] spawners; 
    private HashSet<Transform> occupiedSpawners = new HashSet<Transform>(); 
    private List<GameObject> activeShells = new List<GameObject>(); 
    private float nextSpawnTime; 
    public int maxShells; 
    public float spawnInterval = 2f;
    void Start()
    {
        sorteador = Random.Range(0, 10); 
        nextSpawnTime = Time.time + spawnInterval; 
    }

    void Update()
    {
        // Verifica se está na hora de spawnar uma nova concha e se não excede o limite de conchas
        if (Time.time >= nextSpawnTime && activeShells.Count < maxShells)
        {
            SpawnShell();
            nextSpawnTime = Time.time + spawnInterval; // Atualiza o tempo 
        }
    }

    void SpawnShell()
    {
        // Filtra os spawners disponiveis (nao ocupados)
        Transform[] availableSpawners = System.Array.FindAll(spawners, spawner => !occupiedSpawners.Contains(spawner));

        if (availableSpawners.Length > 0)
        {
        
            Transform selectedSpawner = availableSpawners[Random.Range(0, availableSpawners.Length)];

            GameObject newShell = Instantiate(ShellPrefab, selectedSpawner.position, Quaternion.identity);

            activeShells.Add(newShell);

            occupiedSpawners.Add(selectedSpawner);

            StartCoroutine(ClearOccupiedSpawner(selectedSpawner, 5f)); 
        }
    }

    private System.Collections.IEnumerator ClearOccupiedSpawner(Transform spawner, float delay)
    {
        yield return new WaitForSeconds(delay);
        occupiedSpawners.Remove(spawner);
    }

    public void OnShellInteracted(GameObject shell)
    {

        if (activeShells.Contains(shell))
        {
            activeShells.Remove(shell);
            Destroy(shell); 
        }
    }
}
