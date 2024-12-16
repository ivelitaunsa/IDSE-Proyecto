using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoRectangular : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento
    public Vector3[] waypoints; // Las esquinas del rectángulo (4 puntos)
    private int currentWaypointIndex = 0; // El índice del waypoint al que se dirige el enemigo

    void Start()
    {
        // Asegúrate de que se han asignado correctamente las posiciones de los waypoints
        if (waypoints.Length != 4)
        {
            Debug.LogError("Se deben asignar exactamente 4 waypoints (esquinas del rectángulo).");
        }
    }

    void Update()
    {
        // Mover hacia el siguiente waypoint
        MoveAlongPath();
    }

    void MoveAlongPath()
    {
        if (waypoints.Length == 4)
        {
            // Mover al enemigo hacia el waypoint actual
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex], speed * Time.deltaTime);

            // Si ha llegado al waypoint actual, ir al siguiente
            if (transform.position == waypoints[currentWaypointIndex])
            {
                // Avanzar al siguiente waypoint
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
        }
    }
}
