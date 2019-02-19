using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _ItemPrefab; // the Item prefab spawn
    [SerializeField] private float _spawnCooldown = 3f; // the time between spawning items

    private float currentCooldown = 0;
    private static bool stopSpawning = false;

    // offset to not spawn the item on the ground
    private float _offsetYPosition = 1.5f;

    private void Start()
    {
        //InvokeRepeating("SpawnAsteroid", 0f, _spawnCooldown)
        currentCooldown = 0;
        stopSpawning = false;
    }

    private void Update()
    {
        if (currentCooldown >= _spawnCooldown && !stopSpawning)
        {
            Invoke("SpawnItem", 0f);
            currentCooldown = 0;
        }
        else
        {
            currentCooldown += Time.deltaTime;
        }
    }

    public void StopSpawning()
    {
        stopSpawning = true;
    }

    private void SpawnItem()
    {
        // get the spawn position for the item
        Vector2 spawnPosition = GenerateSpawnPosition();
        float itemIndex = Random.Range(0, 1);
        GameObject itemPrefab = _ItemPrefab[(int)itemIndex];

        Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
    }

    // This function will generate a position near to the Ground
    private Vector2 GenerateSpawnPosition()
    {
        // Get all possible platforms on the level
        GameObject[] possibleGrounds = GameObject.FindGameObjectsWithTag("Ground");

        // Select a random platform
        int randomGround = Random.Range(0, possibleGrounds.Length - 1);
        SpriteRenderer ground = possibleGrounds[randomGround].GetComponent<SpriteRenderer>();

        //gets the X-axis position of the ground
        float positionX = ground.transform.position.x;

        //calculate the initial allowed position to spawn the item 
        //this code gets the positionX (pivot in the middle of the object) and subtract by half of the width of the ground
        float initial = positionX - ground.size.x / 2;

        // this code gets the end position by instead of subtracting, adding the half of width
        float end = positionX + ground.size.x / 2;
        float spawnX = Random.Range(initial, end);

        //get the Y-Axis position
        float positionY = ground.transform.position.y;

        // adding the offset to not spawn the item on the ground
        float spawnY = positionY + _offsetYPosition;

        //returning the generated Vector2 position
        return new Vector2(spawnX, spawnY);
    }
}
