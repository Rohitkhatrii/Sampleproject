using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private int score = 0;
    [SerializeField] private int scoreAddedForRandomSquare = 50;
    [SerializeField] private int scoreAddedForRandomSquare2 = -50;
    [SerializeField] private float moveSpeed = 4f; 

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        float xaxis = Input.GetAxis("Horizontal");
        float yaxis = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(xaxis, yaxis);
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PositiveSquare"))
        {
            AddScore(scoreAddedForRandomSquare);
            Debug.Log("Score Added Positive. Current Score: " + score);
            
            collision.gameObject.SetActive(false); 
            SquareSpawner.Instance.SquareDeactivated(); 
        }
        else if (collision.CompareTag("NegativeSquare"))
        {
            AddScore(scoreAddedForRandomSquare2);
            Debug.Log("Score Added Negative. Current Score: " + score);
            
            collision.gameObject.SetActive(false); 
            SquareSpawner.Instance.SquareDeactivated(); 
        }
    }

    public void AddScore(int scoreNumber)
    {
        score += scoreNumber;
    }
}