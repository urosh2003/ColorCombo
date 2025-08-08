using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<Vector3> spawnOffsets;
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

    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
        addEnemiesElapsed = 0;
        currentMaxEnemy = 1;
        currentMaxColor = 3;
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
        }
    }

    void SpawnEnemy()
    {
        int spawnPosition = Random.Range(0, spawnOffsets.Count);
        int spawnColor = Random.Range(0, currentMaxColor);
        int enemyType = Random.Range(0, currentMaxEnemy);


        Transform spawnedEnemy = Instantiate(enemyPrefab, PlayerManager.instance.transform.position + spawnOffsets[spawnPosition], Quaternion.identity);


        spawnedEnemy.GetComponent<Enemy>().SetEnemy(enemyColors[spawnColor], enemies[enemyType]);
    }


    
}
