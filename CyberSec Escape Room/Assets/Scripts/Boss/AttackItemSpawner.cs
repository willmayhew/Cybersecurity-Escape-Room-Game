using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackItemSpawner : MonoBehaviour
{
    public GameObject attackItemPrefab;
    public Transform spawnArea;

    public GameObject[] avoidanceObjects;
    public float minDistanceToAvoidance = 2f;

    public float spawnInterval = 5f;

    private float timer;

    private bool spawns = false;

    public GameObject challengeCanvas;

    private void Update()
    {

        if(!spawns) { return; }

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnGem();
            timer = 0f;
        }
    }

    public void ToggleSpawning(bool toggle)
    {
        spawns = toggle;
    }

    public void SpawnGem()
    {
        Vector3 spawnPosition = GetRandomPosition();

        foreach (GameObject avoidanceObject in avoidanceObjects)
        {
            if (Vector3.Distance(spawnPosition, avoidanceObject.transform.position) < minDistanceToAvoidance)
            {
                spawnPosition = GetRandomPosition();
                break;
            }
        }

        GameObject attackItem = Instantiate(attackItemPrefab, spawnPosition, Quaternion.identity);

        attackItem.GetComponent<AttackitemScript>().SetCanvasObject(challengeCanvas);
    }

    private Vector3 GetRandomPosition()
    {
        // Calculate random position within the spawn area
        float randomX = Random.Range(spawnArea.position.x - spawnArea.localScale.x / 2f, spawnArea.position.x + spawnArea.localScale.x / 2f);
        float randomY = Random.Range(spawnArea.position.y - spawnArea.localScale.y / 2f, spawnArea.position.y + spawnArea.localScale.y / 2f);

        // Return the random position
        return new Vector3(randomX, randomY, spawnArea.position.z);
    }
}
