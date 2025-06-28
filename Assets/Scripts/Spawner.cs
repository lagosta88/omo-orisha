using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configurações Básicas")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int enemiesToSpawn = 7;
    [SerializeField] private float spawnRadius = 5f;
    [SerializeField] private float minDistanceFromPlayer = 2f;

    private Transform player;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SpawnEnemies()
    {
        // Limpa inimigos existentes
        ClearEnemies();
        
        // Spawn de novos inimigos
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector2 spawnPosition = GetValidSpawnPosition();
            GameObject enemyPrefab = GetRandomEnemyPrefab();
            
            if (enemyPrefab != null)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                spawnedEnemies.Add(enemy);
            }
        }
    }

    private Vector2 GetValidSpawnPosition()
    {
        Vector2 spawnPosition;
        int attempts = 0;
        int maxAttempts = 30;

        do
        {
            // Posição aleatória dentro de um círculo
            spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
            attempts++;
            
            if (attempts >= maxAttempts)
            {
                Debug.LogWarning("Não encontrou posição válida para spawn");
                return transform.position;
            }
            
        } while (Vector2.Distance(spawnPosition, player.position) < minDistanceFromPlayer);

        return spawnPosition;
    }

    private GameObject GetRandomEnemyPrefab()
    {
        if (enemyPrefabs.Length == 0) return null;
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
    }

    private void ClearEnemies()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        spawnedEnemies.Clear();
    }
}