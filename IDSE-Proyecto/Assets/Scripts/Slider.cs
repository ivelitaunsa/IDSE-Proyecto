using UnityEngine;
using UnityEngine.UI;

public class BarraDeCombustible : MonoBehaviour
{
    public Slider barraDeAgua;  // Referencia al Slider de la barra de combustible
    public float cantidadMaxima = 1f;  // Máxima cantidad de combustible (1 es el máximo)
    public float cantidadActual = 1f;  // Cantidad de combustible actual
    public float consumoPorSegundos = 0.05f;  // Cuánto disminuye la barra por segundo
    public float tiempoRecarga = 0.5f;  // Tiempo que tarda en recargar completamente al tocar agua
    public Transform jugador;  // Referencia al objeto jugador (Transform)
    public Vector3 offsetBarra = new Vector3(0, 2, 0);  // Offset para que el slider esté sobre la cabeza del jugador

    public GameObject gameOverPanel;  // Referencia al panel de Game Over

    private bool pausa = false;

    void Start()
    {
        // Inicializar la barra de agua en su valor máximo
        barraDeAgua.maxValue = cantidadMaxima;
        barraDeAgua.value = cantidadMaxima;
    }

    void Update()
    {
        if (!pausa)
        {
            // Comprobar si se está manteniendo presionado el espacio
            if (Input.GetKey(KeyCode.Space) && cantidadActual > 0)
            {
                DisminuirCombustible();
            }

            // Actualizar el valor de la barra en la UI
            barraDeAgua.value = cantidadActual;

            // Asegurarse de que la barra siga al jugador
            SeguirAlJugador();

            // Verificar si la barra ha llegado a 0 y ejecutar Game Over
            if (cantidadActual <= 0)
            {
                GameOver();
            }
        }
    }

    void DisminuirCombustible()
    {
        // Disminuir la cantidad de agua (combustible) mientras se presiona el espacio
        cantidadActual -= consumoPorSegundos * Time.deltaTime;

        // Asegurarse de que no se vuelva negativa
        cantidadActual = Mathf.Max(cantidadActual, 0);
    }

    // Este método se ejecuta cuando el jugador entra en el área del agua
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Agua"))
        {
            // Si el jugador entra en el área del agua, recargar la barra de combustible al máximo
            cantidadActual = cantidadMaxima;
        }
    }

    void SeguirAlJugador()
    {
        if (jugador != null)
        {
            // Posiciona el Slider sobre la cabeza del jugador, utilizando el offset
            Vector3 posicionMundial = jugador.position + offsetBarra;

            // Actualiza la posición del Slider en el mundo
            barraDeAgua.transform.position = posicionMundial;
        }
    }

    private void GameOver()
    {
        pausa = true;
        Debug.Log(pausa);


        // Mostrar el panel de Game Over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Desactivar el objeto del jugador
        if (jugador != null)
        {
            jugador.gameObject.SetActive(false);  // Desactivar el jugador
            Control_nave controlNave = jugador.GetComponent<Control_nave>();

            controlNave.nutrientesRecolectados = 0;
            controlNave.UpdateNutrientIcons();

        }

        // Hacer invisible el slider (deactivarlo)
        if (barraDeAgua != null)
        {
            barraDeAgua.gameObject.SetActive(false);  // Desactivar el Slider
        }
    }

    public void Reiniciar()
    {
        // Restablecer cantidad actual al valor máximo
        cantidadActual = cantidadMaxima;

        jugador.gameObject.SetActive(true);
        
        barraDeAgua.gameObject.SetActive(true);
        barraDeAgua.value = cantidadActual; // Asegurarse de que refleje el valor actua

        Debug.Log("El juego se ha reiniciado");
        pausa = false;
        Debug.Log(pausa);
    }
}
