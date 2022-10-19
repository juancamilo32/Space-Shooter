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
    GameObject tripleShotPrefab;

    bool stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (!stopSpawning)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8.5f, 8.5f), 7.5f, 0);
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (!stopSpawning)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8.5f, 8.5f), 7.5f, 0);
            GameObject powerup = Instantiate(tripleShotPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(30f);
        }
    }

    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }

}
