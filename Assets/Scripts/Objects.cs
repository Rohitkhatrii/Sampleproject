using System.Collections.Generic;
using UnityEngine;

public class SquareSpawner : MonoBehaviour
{
    public static SquareSpawner Instance { get; private set; }

    public GameObject positiveSquarePrefab;
    public GameObject negativeSquarePrefab;
    
    public int poolSizePerSquare = 5; // Reduced since only 2 are on screen at once

    private Queue<GameObject> positivePool = new Queue<GameObject>();
    private Queue<GameObject> negativePool = new Queue<GameObject>();

    private int activeSquareCount = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // 1. Generate the inactive pools
        for (int i = 0; i < poolSizePerSquare; i++)
        {
            GameObject posObj = Instantiate(positiveSquarePrefab);
            posObj.SetActive(false);
            positivePool.Enqueue(posObj);

            GameObject negObj = Instantiate(negativeSquarePrefab);
            negObj.SetActive(false);
            negativePool.Enqueue(negObj);
        }

        // 2. Spawn the very first pair of squares
        SpawnWave();
    }

    public void SquareDeactivated()
    {
        activeSquareCount--;

        // If all squares are collected or fell off screen, spawn 2 more
        if (activeSquareCount <= 0)
        {
            activeSquareCount = 0; // Safety reset
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        SpawnSingleSquare();
        SpawnSingleSquare();
    }

    void SpawnSingleSquare()
    {
        // 50/50 chance to pick either positive or negative
        bool spawnPositive = Random.Range(0, 2) == 0;
        Queue<GameObject> selectedPool = spawnPositive ? positivePool : negativePool;

        if (selectedPool.Count > 0)
        {
            GameObject square = selectedPool.Dequeue();
            
            // Spawn at the top of the screen (Y = 6). 
            // Random X limits (-8 to 8). Random Y offset (6 to 8) so they don't fall at the exact same height.
            Vector2 randomPos = new Vector2(Random.Range(-8f, 8f), Random.Range(6f, 8f));
            
            square.transform.position = randomPos;
            square.SetActive(true);
            
            selectedPool.Enqueue(square);
            activeSquareCount++; // Increase the tracker
        }
    }
}