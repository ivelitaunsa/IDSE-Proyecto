using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Control_nave : MonoBehaviour
{
    private Rigidbody rb;
    private AudioSource audioSource;
    public Slider barraDeAgua;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject nivelCompletoPanel;
    [SerializeField] private Image[] nutrientIcons; // Array of icons for nutrients
    [SerializeField] private Sprite litSprite; // Sprite for "lit" icon
    [SerializeField] private Sprite dimmedSprite; // Sprite for "dimmed" icon
    [SerializeField] private float propulseForce = 3f;
    [SerializeField] private float rotationSpeed = 100f;

    public int nutrientesRecolectados = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (nivelCompletoPanel != null)
        {
            nivelCompletoPanel.SetActive(false);
        }

        // Initialize icons to the dimmed state
        UpdateNutrientIcons();
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

    private void OnTriggerEnter(Collider other)
    {
        // Detecta si el objeto tiene el Tag "Nutriente"
        if (other.CompareTag("nutriente"))
        {
            Debug.Log("Nutriente recolectado!");

            // Incrementa el contador de nutrientes
            IncrementarNutrientes();
        }
        else if (other.CompareTag("Muerto"))
        {
            GameOver();

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
            NivelCompletado();
        }
    }

    private void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        nutrientesRecolectados = 0;
        UpdateNutrientIcons();

        gameObject.SetActive(false);
        barraDeAgua.gameObject.SetActive(false);
    }

    private void NivelCompletado()
    {
        if (nivelCompletoPanel != null)
        {
            nivelCompletoPanel.SetActive(true);
        }

        gameObject.SetActive(false);
        barraDeAgua.gameObject.SetActive(false);
    }

    public void IncrementarNutrientes()
    {
        nutrientesRecolectados++;

        // Update the UI icons based on the number of nutrients collected
        UpdateNutrientIcons();
    }

    public void UpdateNutrientIcons()
    {
        for (int i = 0; i < nutrientIcons.Length; i++)
        {
            if (i < nutrientesRecolectados)
            {
                nutrientIcons[i].sprite = litSprite; // Light up the icon
            }
            else
            {
                nutrientIcons[i].sprite = dimmedSprite; // Keep the icon dimmed
            }
        }
    }
}
