using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _EnergyPrefab; // the Item prefab spawn
    [SerializeField] private float _spawnCooldown = 3f; // the time between spawning items

    private float currentCooldown = 0;
    private static bool stopSpawning = false;

    // offset to not spawn the item on the ground
    private float _offsetYPosition = 1.5f;

    private void Start()
    {
        currentCooldown = 0;
        stopSpawning = false;
    }

    private void Update()
    {
        if (currentCooldown >= _spawnCooldown && !stopSpawning)
        {
            Invoke("SpawnEnergyBall", 0f);
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

    private void SpawnEnergyBall()
    {
        // get the spawn position for the item
        float[] possibleYPositions = new float[2];
        possibleYPositions[0] = -4.6f;
        possibleYPositions[1] = -1.97f;
        //Getting one of the possible Y positions        
        float yPosition = possibleYPositions[Random.Range(0, possibleYPositions.Length)];
        // Getting the X position - 0 for right, 1 for left.
        // Random.Range with int values never returns the max
        // https://docs.unity3d.com/ScriptReference/Random.Range.html
        int leftOrRight = Random.Range(0, 2);
        float xPosition;

        Quaternion direction;
        if (leftOrRight == 0)
        {
            xPosition = -13f;
            //left
            direction = Quaternion.AngleAxis(180, Vector3.forward);
        }
        else
        {
            xPosition = 13f;
            //right
            direction = Quaternion.AngleAxis(0, Vector3.forward);
        }

        Vector2 spawnPosition = new Vector2(xPosition, yPosition);
        float itemIndex = Random.Range(0, 1);
        GameObject itemPrefab = _EnergyPrefab[(int)itemIndex];

        Instantiate(itemPrefab, spawnPosition, direction);
    }

}
