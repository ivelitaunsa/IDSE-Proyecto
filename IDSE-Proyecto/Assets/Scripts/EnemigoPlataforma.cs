using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoPlataforma : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento
    public Transform groundCheckRight; // Objeto vacío para el extremo derecho
    public string platformTag = "Peligroso"; // Tag de las plataformas válidas
    private Rigidbody rb; // O Rigidbody2D para 2D

    private bool movingRight = true; // Dirección inicial
    private float raycastDistance = 1f; // Distancia del raycast

    void Start()
    {
        // Asegúrate de que el Rigidbody está asignado
        rb = GetComponent<Rigidbody>(); // O Rigidbody2D para 2D

        if (rb == null)
        {
            Debug.LogError("Rigidbody no encontrado en el objeto. Asegúrate de que el objeto tenga un Rigidbody asignado.");
        }
    }

    void Update()
    {
        // Dibuja el rayo para depuración en el espacio global
        Debug.DrawRay(groundCheckRight.position, transform.TransformDirection(Vector3.down) * raycastDistance, Color.red); // Dibuja el rayo

        // Lanza un raycast hacia abajo desde la posición relativa del objeto (en el espacio local del objeto)
        RaycastHit hitD;
        bool hitPlatformD = Physics.Raycast(groundCheckRight.position, transform.TransformDirection(Vector3.down), out hitD, raycastDistance); // Usamos TransformDirection para convertir la dirección a global

        // Verifica si la colisión es con un objeto con el tag "Pared"
        if (hitPlatformD)
        {
            Debug.Log("Tag del objeto colisionado: " + hitD.collider.tag);
        }

        // Si se detecta una "Pared", el enemigo sigue moviéndose hacia adelante sin cambiar de dirección
        if (hitPlatformD && hitD.collider.CompareTag("Pared"))
        {
            // El enemigo sigue moviéndose hacia adelante
            if (movingRight)
            {
                rb.velocity = new Vector3(speed * 1f, rb.velocity.y, 0); // Mover hacia la derecha
            }
            else
            {
                rb.velocity = new Vector3(speed * -1f, rb.velocity.y, 0); // Mover hacia la izquierda
            }
            return; // No cambiar de dirección
        }

        // Si no detectamos "Pared", y el enemigo está moviéndose a la derecha
        // En el método Update(), donde se maneja la rotación
        if (movingRight)
        {
            if (!hitPlatformD || !hitD.collider.CompareTag("Pared")) // Si no hay una plataforma válida a la derecha
            {
                rb.velocity = new Vector3(speed * -1f, rb.velocity.y, 0); // Cambiar dirección hacia la izquierda
                movingRight = false; // Cambiar dirección
            }
            else
            {
                rb.velocity = new Vector3(speed * 1f, rb.velocity.y, 0); // Mantener movimiento hacia la derecha
            }

            // Ajustar la rotación y la dirección del movimiento en el eje X
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, movingRight ? 180 : 0, transform.localRotation.eulerAngles.z);
            if (transform.localRotation.eulerAngles.y == 180) // Si está mirando hacia la izquierda
            {
                rb.velocity = new Vector3(speed * -1f, rb.velocity.y, 0); // Mover a la izquierda
            }
        }
        else // Si está moviéndose hacia la izquierda
        {
            if (!hitPlatformD || !hitD.collider.CompareTag("Pared")) // Si no hay una plataforma válida a la izquierda
            {
                rb.velocity = new Vector3(speed * 1f, rb.velocity.y, 0); // Cambiar dirección hacia la derecha
                movingRight = true; // Cambiar dirección
            }
            else
            {
                rb.velocity = new Vector3(speed * -1f, rb.velocity.y, 0); // Mantener movimiento hacia la izquierda
            }

            // Ajustar la rotación y la dirección del movimiento en el eje X
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, movingRight ? 180 : 0, transform.localRotation.eulerAngles.z);
            if (transform.localRotation.eulerAngles.y == 0) // Si está mirando hacia la derecha
            {
                rb.velocity = new Vector3(speed * 1f, rb.velocity.y, 0); // Mover a la derecha
            }
        }
    }
}