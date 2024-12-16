using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contador : MonoBehaviour
{
    private float puntos = 0;
    public UnityEngine.UI.Text textoPuntos;

    private void Start()
    {
        textoPuntos.text = "Puntaje: 0";
    }

    private void Update()
    {
        textoPuntos.text = "Puntaje: " + puntos;
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
