using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularPathEnemy : MonoBehaviour
{
    public float radius = 10f; // Radius of the circle
    public float speed = 5f; // Speed of the enemy's rotation
    public Vector3 centerPoint; // Center of the circular path (can also be the enemy's starting position)

    private float angle = 0f; // Current angle of rotation

    void Start()
    {
        // Initialize the center point to the current position if not set
        if (centerPoint == Vector3.zero)
        {
            centerPoint = transform.position;
        }
    }

    void Update()
    {
        // Increment the angle based on the speed
        angle += speed * Time.deltaTime;

        // Calculate the new position using sine and cosine
        float x = centerPoint.x + radius * Mathf.Cos(angle);
        float y = centerPoint.y + radius * Mathf.Sin(angle);

        // Update the position of the enemy
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
