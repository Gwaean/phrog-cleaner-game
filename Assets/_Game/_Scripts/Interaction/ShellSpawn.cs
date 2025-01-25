using UnityEngine;
using System.Collections.Generic;
public class ShellSpawn : MonoBehaviour
{
    public GameObject ShellPrefab;
    public Transform[] groundSpawners; 
    public Transform[] platformSpawners; 
    private HashSet<Transform> occupiedSpawners = new HashSet<Transform>(); 
    public float spawnInterval = 2f; 
    private float nextSpawnTime;
    private int sorteador; 

    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
        sorteador = Random.Range(0, 10);
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnShell();
            nextSpawnTime = Time.time + spawnInterval;

  
            sorteador = Random.Range(0, 10);
        }
    }

    void SpawnShell()
    {
        // Decide se vai spawnar no chão ou na plataforma usando o sorteador
        Transform[] spawners = (sorteador % 2 == 0) ? groundSpawners : platformSpawners;

        // Filtra os spawners disponíveis (não ocupados)
        Transform[] availableSpawners = System.Array.FindAll(spawners, spawner => !occupiedSpawners.Contains(spawner));

        if (availableSpawners.Length > 0)
        {
            // Sorteia um spawner disponível
            Transform selectedSpawner = availableSpawners[Random.Range(0, availableSpawners.Length)];

            // Instancia a concha e marca o spawner como ocupado
            Instantiate(ShellPrefab, selectedSpawner.position, Quaternion.identity);
            occupiedSpawners.Add(selectedSpawner);

            // Libera o spawner após um tempo
            StartCoroutine(ClearOccupiedSpawner(selectedSpawner, 5f)); // Libera após 5 segundos
        }
    }

    private System.Collections.IEnumerator ClearOccupiedSpawner(Transform spawner, float delay)
    {
        yield return new WaitForSeconds(delay);
        occupiedSpawners.Remove(spawner);
    }
}
