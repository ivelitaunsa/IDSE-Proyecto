using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con UI
using UnityEngine.SceneManagement;
using System; // Agregar esta l�nea

public class GameManager : MonoBehaviour
{
    public Transform rocket;         // Referencia al cohete
    public GameObject gameOverPanel; // Referencia al panel de Game Over
    public float cameraReturnSpeed = 5f; // Velocidad del desplazamiento de la c�mara

    private Vector3 initialRocketPosition; // Posici�n inicial del cohete
    private Quaternion initialRocketRotation; // Rotaci�n inicial del cohete

    void Start()
    {
        // Guardar las posiciones y rotaciones iniciales
        if (rocket != null)
        {
            initialRocketPosition = rocket.position;
            initialRocketRotation = rocket.rotation;
        }

        // Asegurarse de que el panel de Game Over est� desactivado al inicio
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Asegurarse de que el tiempo est� activo

        // Ocultar el panel de Game Over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // Restaurar la posici�n y rotaci�n del cohete
        if (rocket != null)
        {
            rocket.position = initialRocketPosition;
            rocket.rotation = initialRocketRotation;


            // Reactivar el cohete si est� desactivado
            rocket.gameObject.SetActive(true);

            // Si el cohete tiene un Rigidbody, reiniciar su velocidad
            Rigidbody rocketRb = rocket.GetComponent<Rigidbody>();
            if (rocketRb != null)
            {
                rocketRb.velocity = Vector3.zero;
                rocketRb.angularVelocity = Vector3.zero;
            }
        }
    }

    public void LoadNextLevel(String level)
    {
        SceneManager.LoadScene(level);
    }

    public static GameManager instance;
    public int playerScore = 0;

    public void AddScore(int score)
    {
        playerScore += score;
    }
}
