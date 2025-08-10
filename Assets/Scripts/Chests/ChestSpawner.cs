using System;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    [SerializeField] private Transform chestPrefab;
    [SerializeField] private List<Transform> spawnLocations;

    [SerializeField] float minDistance;
    [SerializeField] float maxDistance;

    float sqrMin;
    float sqrMax;

    public Transform chest1;
    public Transform chest2;

    public static ChestSpawner instance;

    public event Action ChestSpawned;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        sqrMin = minDistance * minDistance;
        sqrMax = maxDistance * maxDistance;

        SpawnChest(1);
        SpawnChest(2);

        GameManager.instance.ChestCollected += SpawnChest;
    }

    void SpawnChest(int chestNumber)
    {
        Transform chest = Instantiate(chestPrefab, GetSpawn(), Quaternion.identity);
        chest.GetComponent<Chest>().chestNumber = chestNumber;
        if (chestNumber == 1)
        {
            chest1 = chest;
        }
        else
        {
            chest2 = chest;
        }

        ChestSpawned?.Invoke();
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
            if (sqrDist >= sqrMin && sqrDist <= sqrMax && (chest1==null || chest1.position!=location.position) && (chest2 == null || chest2.position != location.position))
            {
                validLocations.Add(location.transform.position);
            }
        }

        int rndIndex = UnityEngine.Random.Range(0, validLocations.Count);

        return validLocations[rndIndex];
    }

    private void OnDestroy()
    {
        GameManager.instance.ChestCollected -= SpawnChest;
    }
}