using System.Collections.Generic;
using UnityEngine;

public class SquareSpawner : MonoBehaviour
{
    public static SquareSpawner Instance { get; private set; }

    public GameObject positiveSquarePrefab;
    public GameObject negativeSquarePrefab;
    
    public int poolSizePerSquare = 5; 

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
        for (int i = 0; i < poolSizePerSquare; i++)
        {
            GameObject posObj = Instantiate(positiveSquarePrefab);
            posObj.SetActive(false);
            positivePool.Enqueue(posObj);

            GameObject negObj = Instantiate(negativeSquarePrefab);
            negObj.SetActive(false);
            negativePool.Enqueue(negObj);
        }

        SpawnWave();
    }

    public void SquareDeactivated()
    {
        activeSquareCount--;

        if (activeSquareCount <= 0)
        {
            activeSquareCount = 0; 
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
        bool spawnPositive = Random.Range(0, 2) == 0;
        Queue<GameObject> selectedPool = spawnPositive ? positivePool : negativePool;

        if (selectedPool.Count > 0)
        {
            GameObject square = selectedPool.Dequeue();
            
            Vector2 randomPos = new Vector2(Random.Range(-8f, 8f), Random.Range(6f, 8f));
            
            square.transform.position = randomPos;
            square.SetActive(true);
            
            selectedPool.Enqueue(square);
            activeSquareCount++; 
        }
    }
}