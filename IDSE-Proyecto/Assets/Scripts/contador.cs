using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contador : MonoBehaviour
{
    public static contador Instance;
    [SerializeField] private float puntos;
    public UnityEngine.UI.Text textoPuntos;

    private void Awake()
    {
        if(contador.Instance == null)
        {
            contador.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void SumarPuntos(float cantidad)
    {
        puntos += cantidad;
        textoPuntos.text = "Puntaje: " + puntos;
    }

    public void Reiniciar()
    {
        puntos = 0;
        textoPuntos.text = "Puntaje: " + puntos;
    }
}
