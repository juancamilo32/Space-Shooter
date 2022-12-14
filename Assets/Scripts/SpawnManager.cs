using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    GameObject enemyContainer;

    [SerializeField]
    GameObject[] powerupPrefabs;

    bool stopSpawning = false;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(1f);
        while (!stopSpawning)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8.5f, 8.5f), 7.5f, 0);
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(1f);
        while (!stopSpawning)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8.5f, 8.5f), 7.5f, 0);
            GameObject powerup = Instantiate(powerupPrefabs[Random.Range(0, 3)], spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(7f, 10f));
        }
    }

    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }

}
