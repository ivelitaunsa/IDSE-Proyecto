using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Control_nave : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioSource;
    public Slider barraDeAgua;  // Referencia al Slider de la barra de combustible

    [SerializeField] private GameObject gameOverPanel; // Panel de Game Over
    [SerializeField] private float propulseForce = 3f; // Fuerza de propulsi�n (ajustable)
    [SerializeField] private float rotationSpeed = 100f; // Velocidad de rotaci�n (ajustable)

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on the GameObject. Please add an AudioSource.");
        }

        // Asegurarse de que el panel de Game Over est� desactivado al inicio
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    void Update()
    {
        Rotar();
        Propulsar();
    }

    private void Rotar()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime, Space.Self);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime, Space.Self);
        }
    }

    private void Propulsar()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.freezeRotation = true;
            rb.AddRelativeForce(Vector3.up * propulseForce * Time.deltaTime, ForceMode.Acceleration);

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Seguro"))
        {
            Debug.Log("Colision Segura");
        }
        else if (collision.gameObject.CompareTag("Peligroso"))
        {
            Debug.Log("Colision peligrosa");
        }
        else if (collision.gameObject.CompareTag("Muerto"))
        {
            GameOver();
        }
        else if (collision.gameObject.CompareTag("Meta"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void GameOver()
    {
        // Mostrar el panel de Game Over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Desactivar el cohete
        gameObject.SetActive(false);
        barraDeAgua.gameObject.SetActive(false);
        
    }
}
