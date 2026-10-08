using UnityEngine;

public class SquareBehavior : MonoBehaviour
{
    public float fallSpeed = 3f;
    private float offScreenBottomY = -6f; 

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        if (transform.position.y < offScreenBottomY)
        {
            gameObject.SetActive(false);
            
            SquareSpawner.Instance.SquareDeactivated();
        }
    }
}