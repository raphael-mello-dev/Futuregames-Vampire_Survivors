using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> basicEnemiesList;
    [SerializeField] private List<GameObject> hordeEnemiesList;
    [SerializeField] private int basicEnemyCount;
    [SerializeField] private int hordeEnemyCount;

    [SerializeField] private float spawnDistance;
    [SerializeField] private float timeToSpawn;
    [SerializeField] private float rewindTime;

    void Start()
    {
        for (int i = 0; i < basicEnemyCount; i++)
        {
            GameObject basicEnemy = Instantiate(Resources.Load<GameObject>("Prefabs/BasicEnemy"), Vector3.zero, Quaternion.identity, gameObject.transform);
            basicEnemy.SetActive(false);
            basicEnemiesList.Add(basicEnemy);
        }

        for (int i = 0; i < hordeEnemyCount; i++)
        {
            GameObject hordeEnemy = Instantiate(Resources.Load<GameObject>("Prefabs/HordeEnemy"), Vector3.zero, Quaternion.identity, gameObject.transform);
            hordeEnemy.SetActive(false);
            hordeEnemiesList.Add(hordeEnemy);
        }
    }

    private void OnEnable()
    {
        GameplayState.OnObjectsActivated -= CheckingObjEnable;
        EndGameState.OnObjectsDeactivated += gameObject.SetActive;
        PlayerLevel.OnLeveledUp += IncreaseEnemiesLevel;

        foreach (GameObject enemy in basicEnemiesList)
        {
            if (enemy.activeInHierarchy)
            {
                enemy.transform.position = Vector3.zero;
                enemy.SetActive(false);
            }
        }

        foreach (GameObject enemy in hordeEnemiesList)
        {
            if (enemy.activeInHierarchy)
            {
                enemy.transform.position = Vector3.zero;
                enemy.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        GameplayState.OnObjectsActivated += CheckingObjEnable;
        EndGameState.OnObjectsDeactivated -= gameObject.SetActive;
        PlayerLevel.OnLeveledUp -= IncreaseEnemiesLevel;
    }

    void Update()
    {
        if(GameManager.Instance.stateMachine.currentState.ToString() == "GameplayState")
        {
            timeToSpawn -= Time.deltaTime;
            if (timeToSpawn <= 0)
            {
                SpawnBasicEnemy();
                timeToSpawn = rewindTime;
            }

            OnMove();
        }
    }

    void OnMove()
    {
        foreach (GameObject enemy in basicEnemiesList)
            enemy.GetComponent<BasicEnemy>().OnMove();

        foreach (GameObject enemy in hordeEnemiesList)
            enemy.GetComponent<HordeEnemy>().OnMove();
    }

    void SpawnBasicEnemy()
    {
        foreach (GameObject enemy in basicEnemiesList)
        {
            if (!enemy.activeInHierarchy)
            {
                Vector3 spawnPosition = GetValidSpawnPosition();

                if (spawnPosition != Vector3.zero)
                {
                    enemy.transform.position = spawnPosition;
                    enemy.SetActive(true);
                    break;
                }
            }
        }

        foreach (GameObject enemy in hordeEnemiesList)
        {
            if (!enemy.activeInHierarchy)
            {
                Vector3 spawnPosition = GetValidSpawnPosition();

                if (spawnPosition != Vector3.zero)
                {
                    enemy.transform.position = spawnPosition;
                    enemy.SetActive(true);
                    break;
                }
            }
        }
    }

    Vector3 GetValidSpawnPosition()
    {
        Vector3 spawnPosition;
        
        int attempts = 0;

        do
        {
            spawnPosition = new Vector3(Random.Range(-20f, 20f), Random.Range(-18f, 18f), 0);
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

        foreach (GameObject enemy in hordeEnemiesList)
        {
            if (enemy.activeInHierarchy && Vector3.Distance(position, enemy.transform.position) < spawnDistance)
            {
                return false;
            }
        }

        return true;
    }

    void IncreaseEnemiesLevel()
    {
        switch (Mathf.Round(UnityEngine.Random.Range(0, 3f)))
        {
            case 0:
                if (timeToSpawn > 0.5f)
                    timeToSpawn -= 05f;
            break;
            case 1:
                foreach (GameObject enemy in basicEnemiesList)
                {
                    if (enemy.activeInHierarchy || !enemy.activeInHierarchy)
                        enemy.GetComponent<BasicEnemy>().speed += 0.02f;
                }
                foreach (GameObject enemy in hordeEnemiesList)
                {

                    if (enemy.activeInHierarchy || !enemy.activeInHierarchy)
                        enemy.GetComponent<HordeEnemy>().speed += 0.01f;
                }
                break;
            case 2:
                foreach (GameObject enemy in basicEnemiesList)
                {
                    if (enemy.activeInHierarchy || !enemy.activeInHierarchy)
                        enemy.GetComponent<BasicEnemy>().attack += 1;
                }
                foreach (GameObject enemy in hordeEnemiesList)
                {
                    if (enemy.activeInHierarchy || !enemy.activeInHierarchy)
                        enemy.GetComponent<HordeEnemy>().attack += 1;
                }
                break;
            default:
                foreach (GameObject enemy in basicEnemiesList)
                {
                    if (enemy.activeInHierarchy || !enemy.activeInHierarchy)
                        enemy.GetComponent<BasicEnemy>().health += 2;
                }
                foreach (GameObject enemy in hordeEnemiesList)
                {
                    if (enemy.activeInHierarchy || !enemy.activeInHierarchy)
                        enemy.GetComponent<HordeEnemy>().health += 1;
                }
                break;
        }
    }

    void CheckingObjEnable()
    {
        if(!gameObject.activeInHierarchy)
            gameObject.SetActive(true);
    }
}