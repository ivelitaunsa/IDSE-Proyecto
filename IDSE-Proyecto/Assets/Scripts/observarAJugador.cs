using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class observarAJugador : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float rotationSpeed = 5f; // Speed of rotation

    void Update()
    {
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Create the target rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
