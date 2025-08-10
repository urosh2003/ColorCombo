using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<Transform> spawnLocations;
    [SerializeField] float spawnTimer;
    [SerializeField] float timeElapsed;
    [SerializeField] List<EnemyColor> enemyColors;
    [SerializeField] List<EnemyType> enemies;
    [SerializeField] Transform enemyPrefab;
    [SerializeField] float spawnRampStep;

    [SerializeField] float addEnemiesTimer;
    [SerializeField] float addEnemiesElapsed;

    [SerializeField] int currentMaxEnemy;
    [SerializeField] int currentMaxColor;

    [SerializeField] float minDistance;
    [SerializeField] float maxDistance;
    float sqrMin;
    float sqrMax;

    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
        addEnemiesElapsed = 0;
        currentMaxEnemy = 1;
        currentMaxColor = 3;
        sqrMin = minDistance * minDistance;
        sqrMax = maxDistance * maxDistance;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        addEnemiesElapsed += Time.deltaTime;

        if(addEnemiesElapsed > addEnemiesTimer && currentMaxEnemy <= enemies.Count)
        {
            addEnemiesElapsed = 0;

            if(currentMaxColor == 3)
                currentMaxColor = 6;
            else if (currentMaxEnemy < enemies.Count)
            {
                currentMaxColor = 3;
                currentMaxEnemy += 1;
            }
        }

        if (timeElapsed > spawnTimer)
        {
            timeElapsed = 0;
            if (spawnTimer > 0.05)
            {
                spawnTimer -= spawnRampStep;
            }
            SpawnEnemy();
            SpawnEnemy();

        }
    }

    void SpawnEnemy()
    {
        int spawnColor = Random.Range(0, currentMaxColor);
        int enemyType = Random.Range(0, currentMaxEnemy);


        Transform spawnedEnemy = Instantiate(enemyPrefab, GetSpawn(), Quaternion.identity);


        spawnedEnemy.GetComponent<Enemy>().SetEnemy(enemyColors[spawnColor], enemies[enemyType]);
    }

    Vector3 GetSpawn()
    {
        Vector3 playerPos = PlayerManager.instance.transform.position;
        

        List<Vector3> validLocations = new List<Vector3>();

        foreach (Transform location in spawnLocations)
        {
            // Calculate squared distance to avoid expensive sqrt operation
            float sqrDist = (playerPos - location.position).sqrMagnitude;

            // Check if distance is within the valid range
            if (sqrDist >= sqrMin && sqrDist <= sqrMax)
            {
                validLocations.Add(location.transform.position);
            }
        }

        int rndIndex = Random.Range(0, validLocations.Count);

        return validLocations[rndIndex];
    }
    
}
