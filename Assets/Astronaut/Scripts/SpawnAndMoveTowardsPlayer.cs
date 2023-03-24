using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndMoveTowardsPlayer : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public Transform playerTransform;
    public float initialSpawnInterval = 3f;
    public float minSpawnInterval = 0.5f;
    public float spawnIntervalDecrease = 0.1f;
    public float objectSpeed = 5f;
    public float objectLifetime = 10f;
    public Transform[] spawnPoints;

    private float spawnInterval;
    private float spawnTimer = 0f;

    void Start()
    {
        spawnInterval = initialSpawnInterval;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;

            int randomIndex = Random.Range(0, objectsToSpawn.Length);
            GameObject objectToSpawn = objectsToSpawn[randomIndex];
            Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            GameObject newObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

            Vector3 directionToPlayer = (playerTransform.position - spawnPosition).normalized;
            newObject.GetComponent<Rigidbody2D>().velocity = directionToPlayer * objectSpeed;

            Destroy(newObject, objectLifetime);

            // decrease spawn interval
            spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - spawnIntervalDecrease);
        }
    }
}
