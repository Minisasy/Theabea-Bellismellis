using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] float spawnHeight = 4f;
    [SerializeField] float spawnSpanX = 8f;
    [SerializeField] float BorderSpawn;
    [SerializeField] float BorderSpawnY;

    [Header("Spawn Bools")]
    bool hasSpawnedHealth;
    bool hasSpawnedEnemyY;

    [SerializeField] GameObject oil;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject healthPickup;
    [SerializeField] GameObject spawnContainer;

    void Start()
    {
        StartCoroutine(SpawningOil());
        StartCoroutine(SpawningEnemyX());
    }

    public void StartHealthCoroutine()
    {
        if (hasSpawnedHealth == true)
        {
            return;
        }

        hasSpawnedHealth = true;
        StartCoroutine(SpawningHealth());
    }

    #region Enemy Spawning
    public void StartEnemySpawningY()
    {
        if (hasSpawnedEnemyY == true)
        {
            return;
        }

        hasSpawnedEnemyY = true;
        StartCoroutine(SpawningEnemyY());
    }

    IEnumerator SpawningEnemyX()
    {
        BorderSpawn = Random.Range(1, -1);

        if (BorderSpawn > 0)
        {
            Vector2 spawnPosition = new Vector2(-9f, Random.Range(-5f, 5f));
            GameObject mySpawnedEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
            mySpawnedEnemy.transform.SetParent(spawnContainer.transform);
        }
        else
        {
            Vector2 spawnPosition = new Vector2(9f, Random.Range(-5f, 5f));
            GameObject mySpawnedEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
            mySpawnedEnemy.transform.SetParent(spawnContainer.transform);
        }

        yield return new WaitForSeconds(Random.Range(2f, 4f));

        StartCoroutine(SpawningEnemyX());
    }

    IEnumerator SpawningEnemyY()
    {
        BorderSpawnY = Random.Range(1, -1);

        if (BorderSpawnY > 0)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), -5f);
            GameObject mySpawnedEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
            mySpawnedEnemy.transform.SetParent(spawnContainer.transform);
        }
        else
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), 5f);
            GameObject mySpawnedEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
            mySpawnedEnemy.transform.SetParent(spawnContainer.transform);
        }

        yield return new WaitForSeconds(Random.Range(2f, 4f));

        StartCoroutine(SpawningEnemyY());
    }

    #endregion 

    IEnumerator SpawningOil()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-spawnSpanX, spawnSpanX), Random.Range(-spawnHeight, spawnHeight));
        GameObject mySpawnedOil = Instantiate(oil, spawnPosition, Quaternion.identity);
        mySpawnedOil.transform.SetParent(spawnContainer.transform);

        yield return new WaitForSeconds(Random.Range(2f, 3.5f));

        StartCoroutine(SpawningOil());
    }

    IEnumerator SpawningHealth()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-spawnSpanX, spawnSpanX), Random.Range(-spawnHeight, spawnHeight));
        GameObject spawnedHealthPickup = Instantiate(healthPickup, spawnPosition, Quaternion.identity);
        spawnedHealthPickup.transform.SetParent(spawnContainer.transform);

        yield return new WaitForSeconds(Random.Range(16f, 25f));

        StartCoroutine(SpawningHealth());
    }
}
