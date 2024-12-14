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

        Debug.DrawRay(groundCheckRight.position, Vector3.down * 0.5f, Color.red); // Dibuja el rayo


        // Lanza un raycast hacia abajo desde el groundCheck
        RaycastHit hitD;
        bool hitPlatformD = Physics.Raycast(groundCheckRight.position, Vector3.down, out hitD, 0.5f); // Distancia ajustada a 0.5f



        if (movingRight)
        {

            if (!hitPlatformD)
            {
                rb.velocity = new Vector3(speed * -1f, rb.velocity.y, 0);
                movingRight = false; // Cambiar dirección
                transform.eulerAngles = new Vector3(0, movingRight ? 0 : 180, 0); // Rotar sprite u objeto
            }
            rb.velocity = new Vector3(speed * 1f, rb.velocity.y, 0);

        }
        else
        {

            if (!hitPlatformD)
            {
                rb.velocity = new Vector3(speed * 1f, rb.velocity.y, 0);
                movingRight = true; // Cambiar dirección
                transform.eulerAngles = new Vector3(0, movingRight ? 0 : 180, 0); // Rotar sprite u objeto

            }
            rb.velocity = new Vector3(speed * - 1f, rb.velocity.y, 0);

        }
    }

  

    void Flip()
    {

    }
}
