using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con UI
using UnityEngine.SceneManagement; // Agregar esta línea

public class GameManager : MonoBehaviour
{
    public Transform rocket;         // Referencia al cohete
    public Transform camera;         // Referencia a la cámara
    public GameObject gameOverPanel; // Referencia al panel de Game Over
    public float cameraReturnSpeed = 5f; // Velocidad del desplazamiento de la cámara

    private Vector3 initialRocketPosition; // Posición inicial del cohete
    private Quaternion initialRocketRotation; // Rotación inicial del cohete

    private Vector3 initialCameraPosition; // Posición inicial de la cámara
    private Quaternion initialCameraRotation; // Rotación inicial de la cámara

    private bool isResettingCamera = false; // Controla si la cámara está regresando

    void Start()
    {
        // Guardar las posiciones y rotaciones iniciales
        if (rocket != null)
        {
            initialRocketPosition = rocket.position;
            initialRocketRotation = rocket.rotation;
        }

        if (camera != null)
        {
            initialCameraPosition = camera.position;
            initialCameraRotation = camera.rotation;
        }

        // Asegurarse de que el panel de Game Over esté desactivado al inicio
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

    }

    void Update()
    {
        // Mover la cámara suavemente a su posición inicial si está en proceso de reinicio
        if (isResettingCamera && camera != null)
        {
            camera.position = Vector3.Lerp(camera.position, initialCameraPosition, cameraReturnSpeed * Time.deltaTime);
            camera.rotation = Quaternion.Lerp(camera.rotation, initialCameraRotation, cameraReturnSpeed * Time.deltaTime);

            // Si la cámara ha llegado cerca de su posición y rotación inicial, detener el movimiento
            if (Vector3.Distance(camera.position, initialCameraPosition) < 0.01f &&
                Quaternion.Angle(camera.rotation, initialCameraRotation) < 0.1f)
            {
                camera.position = initialCameraPosition;
                camera.rotation = initialCameraRotation;
                isResettingCamera = false; // Terminar el reinicio
            }
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Asegurarse de que el tiempo esté activo

        // Ocultar el panel de Game Over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // Restaurar la posición y rotación del cohete
        if (rocket != null)
        {
            rocket.position = initialRocketPosition;
            rocket.rotation = initialRocketRotation;


            // Reactivar el cohete si está desactivado
            rocket.gameObject.SetActive(true);

            // Si el cohete tiene un Rigidbody, reiniciar su velocidad
            Rigidbody rocketRb = rocket.GetComponent<Rigidbody>();
            if (rocketRb != null)
            {
                rocketRb.velocity = Vector3.zero;
                rocketRb.angularVelocity = Vector3.zero;
            }
        }

        // Iniciar el proceso de regreso suave de la cámara
        isResettingCamera = true;
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;  // Cargar el siguiente nivel

        // Verificar si hay un siguiente nivel
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // Si no hay siguiente nivel, reinicia el primer nivel o muestra un mensaje de victoria
            Debug.Log("¡Has completado todos los niveles!");
            SceneManager.LoadScene(0); // Reinicia el primer nivel
        }
    }

    public static GameManager instance;
    public int playerScore = 0;

    void Awake()
    {
        // Asegurarse de que solo haya una instancia de GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // No destruir al cargar nueva escena
        }
        else
        {
            Destroy(gameObject); // Destruir otras instancias
        }
    }

    public void AddScore(int score)
    {
        playerScore += score;
    }
}
