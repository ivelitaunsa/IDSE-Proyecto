using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoAereo : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 100f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Si la distancia es negativa, invertir la dirección del movimiento
        float movement = Mathf.PingPong(Time.time * speed, Mathf.Abs(distance));

        // Ajustar la dirección si la distancia es negativa
        if (distance < 0)
        {
            movement = -movement;
        }

        // El movimiento es a lo largo del eje Y si la distancia es positiva o negativa
        // Si la distancia es negativa, se invierte el movimiento en el eje Y
        transform.position = startPosition + new Vector3(0, movement, 0);
    }
}
