using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<Vector3> spawnOffsets;
    [SerializeField] float spawnTimer;
    [SerializeField] float timeElapsed;
    [SerializeField] List<EnemyColor> enemyColors;
    [SerializeField] Transform enemyPrefab;
    [SerializeField] float spawnRampStep;

    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

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
        int spawnColor = Random.Range(0, enemyColors.Count);

        Transform spawnedEnemy = Instantiate(enemyPrefab, PlayerManager.instance.transform.position + spawnOffsets[spawnPosition], Quaternion.identity);
        spawnedEnemy.GetComponent<Enemy>().SetEnemyColor(enemyColors[spawnColor], spawnTimer);
    }
}
