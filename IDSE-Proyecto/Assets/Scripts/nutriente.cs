using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nutriente : MonoBehaviour
{
    [SerializeField] private contador puntaje;
    [SerializeField] private float cantidadPuntos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            puntaje.SumarPuntos(cantidadPuntos);
            Debug.Log("Nutriente!!!");
            gameObject.SetActive(false);
        }
    }

    public void Reinicio() {
        gameObject.SetActive(true);
    }
}
