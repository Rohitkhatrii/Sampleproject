using UnityEngine;

public class SquareBehavior : MonoBehaviour
{
    public float fallSpeed = 3f;
    private float offScreenBottomY = -6f; // Adjust this if your camera is larger

    void Update()
    {
        // Move the square downward every frame
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        // If the player misses it and it falls off the bottom of the screen
        if (transform.position.y < offScreenBottomY)
        {
            gameObject.SetActive(false);
            
            // Tell the Spawner that a square was removed from the board
            SquareSpawner.Instance.SquareDeactivated();
        }
    }
}