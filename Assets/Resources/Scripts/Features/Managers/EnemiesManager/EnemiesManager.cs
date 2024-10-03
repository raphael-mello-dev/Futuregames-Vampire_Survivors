using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> basicEnemiesList;
    [SerializeField] private int capacity;

    [SerializeField] private float spawnDistance;
    [SerializeField] private float timeToSpawn;
    private float rewindTime;


    void Start()
    {
        for (int i = 0; i < capacity; i++)
        {
            GameObject enemy = Instantiate(Resources.Load<GameObject>("Prefabs/BasicEnemy"), Vector3.zero, Quaternion.identity, gameObject.transform);
            enemy.SetActive(false);
            basicEnemiesList.Add(enemy);
        }

        rewindTime = timeToSpawn;
    }

    void Update()
    {
        SpawnBasicEnemy();
        OnMove();

    }

    void OnMove()
    {
        foreach (GameObject enemy in basicEnemiesList)
            enemy.GetComponent<BasicEnemy>().OnMove();
    }

    void SpawnBasicEnemy()
    {
        timeToSpawn -= Time.deltaTime;

        if (timeToSpawn <= 0)
        {
            foreach (GameObject enemy in basicEnemiesList)
            {
                if (!enemy.activeInHierarchy) // Check if the enemy is inactive
                {
                    Vector3 spawnPosition = GetValidSpawnPosition();

                    if (spawnPosition != Vector3.zero)
                    {
                        enemy.transform.position = spawnPosition; // Set the position
                        enemy.SetActive(true); // Activate the enemy
                        break; // Exit after spawning one enemy
                    }
                }
            }

            timeToSpawn = rewindTime; // Reset the spawn timer
        }
    }

    Vector3 GetValidSpawnPosition()
    {
        Vector3 spawnPosition;
        int attempts = 0;

        do
        {
            spawnPosition = new Vector3(Random.Range(-25f, 25f), Random.Range(-20f, 20f), 0);
            attempts++;

            if (attempts >= 10) return Vector3.zero;

        } while (!IsValidSpawnPosition(spawnPosition));

        return spawnPosition;
    }

    bool IsValidSpawnPosition(Vector3 position)
    {
        foreach (GameObject enemy in basicEnemiesList)
        {
            if (Vector3.Distance(position, enemy.GetComponent<BasicEnemy>().Player.position) < spawnDistance)
            {
                return false;
            }
        }

        foreach (GameObject enemy in basicEnemiesList)
        {
            if (enemy.activeInHierarchy && Vector3.Distance(position, enemy.transform.position) < spawnDistance)
            {
                return false;
            }
        }

        return true;
    }
}