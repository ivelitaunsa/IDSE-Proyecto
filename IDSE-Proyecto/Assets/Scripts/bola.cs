using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Destruir el objeto automáticamente después de 10 segundos
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Muerto!!!");

            // Accede a la torreta y detén el disparo
            Torreta torreta = FindObjectOfType<Torreta>();
            if (torreta != null)
            {
                torreta.isShooting = false;
            }
        }
        else if (!collision.gameObject.CompareTag("Pared"))
        {
            gameObject.SetActive(false);
        }
    }
}
