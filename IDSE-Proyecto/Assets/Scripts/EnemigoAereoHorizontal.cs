using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoAereoHorizontal : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento
    public float distance = 100f; // Distancia m�xima de movimiento
    private Vector3 startPosition; // Posici�n inicial de la plataforma
    private Rigidbody rb; // Referencia al Rigidbody

    private bool movingRight = true; // Direcci�n inicial del movimiento

    void Start()
    {
        // Guardar la posici�n inicial
        startPosition = transform.position;

        // Obtener el Rigidbody y verificar que exista
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No se encontr� un Rigidbody en la plataforma.");
        }
        else
        {
            rb.useGravity = false; // Desactiva la gravedad
            rb.isKinematic = true; // Configurar como cinem�tico
        }
    }

    void FixedUpdate()
    {
        // Calcular la posici�n relativa actual
        float relativePositionX = transform.position.x - startPosition.x;

        // Cambiar direcci�n si alcanza los l�mites de la distancia
        if (movingRight && relativePositionX >= distance)
        {
            movingRight = false;
        }
        else if (!movingRight && relativePositionX <= 0)
        {
            movingRight = true;
        }

        // Calcular la direcci�n de movimiento
        float movement = (movingRight ? speed : -speed) * Time.deltaTime;

        // Aplicar el movimiento usando MovePosition
        rb.MovePosition(transform.position + new Vector3(movement, 0, 0));
    }
}