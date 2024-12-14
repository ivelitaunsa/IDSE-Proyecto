using UnityEngine;

public class DistribuirPinchos : MonoBehaviour
{
    public GameObject pinchoPrefab; // Prefab del pincho
    public Vector3 pinchoRotation = Vector3.zero; // Rotación en grados para los pinchos
    public float solapamiento = 0.5f; // Proporción de solapamiento (0 = sin solapamiento, 1 = completamente solapados)
    public Vector3 escalaPinchos = Vector3.one; // Escala personalizada para los pinchos
    public float multiplicadorEscalaGlobal = 10f; // Multiplicador global para escalar los pinchos

    void Start()
    {
        GenerarPinchos();
    }

    void GenerarPinchos()
    {
        // Obtener el BoxCollider del GameObject
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            Debug.LogError("Este GameObject necesita un BoxCollider");
            return;
        }

        // Obtener el tamaño real del BoxCollider
        Vector3 boxSize = boxCollider.size;

        // Obtener el tamaño real del prefab
        Vector3 pinchoSizeOriginal = ObtenerTamañoReal(pinchoPrefab);

        // Aplicar la escala personalizada y el multiplicador global al tamaño del pincho
        Vector3 pinchoSize = new Vector3(
            pinchoSizeOriginal.x * escalaPinchos.x * multiplicadorEscalaGlobal,
            pinchoSizeOriginal.y * escalaPinchos.y * multiplicadorEscalaGlobal,
            pinchoSizeOriginal.z * escalaPinchos.z * multiplicadorEscalaGlobal
        );

        // Calcular la separación entre los pinchos considerando el solapamiento
        Vector3 pinchoSeparation = new Vector3(
            pinchoSize.x * (1 - solapamiento),
            pinchoSize.y * (1 - solapamiento),
            pinchoSize.z * (1 - solapamiento)
        );

        // Calcular la cantidad de pinchos necesarios en cada eje
        int countX = Mathf.CeilToInt(boxSize.x / pinchoSeparation.x);
        int countY = Mathf.CeilToInt(boxSize.y / pinchoSeparation.y);
        int countZ = Mathf.CeilToInt(boxSize.z / pinchoSeparation.z);

        // Generar los pinchos en la posición correcta
        for (int x = 0; x < countX; x++)
        {
            for (int y = 0; y < countY; y++)
            {
                for (int z = 0; z < countZ; z++)
                {
                    // Calcular la posición de cada pincho
                    Vector3 pinchoPosition = transform.position +
                        new Vector3(
                            (x * pinchoSeparation.x) - (boxSize.x / 2) + (pinchoSize.x / 2),
                            (y * pinchoSeparation.y) - (boxSize.y / 2) + (pinchoSize.y / 2),
                            (z * pinchoSeparation.z) - (boxSize.z / 2) + (pinchoSize.z / 2)
                        );

                    // Convertir rotación a Quaternion
                    Quaternion pinchoRotationQuat = Quaternion.Euler(pinchoRotation);

                    // Instanciar el pincho
                    GameObject pincho = Instantiate(pinchoPrefab, pinchoPosition, pinchoRotationQuat, transform);

                    // Ajustar la escala del pincho al valor configurado
                    pincho.transform.localScale = escalaPinchos * multiplicadorEscalaGlobal;
                }
            }
        }
    }

    Vector3 ObtenerTamañoReal(GameObject prefab)
    {
        // Crear una instancia temporal del prefab
        GameObject tempPincho = Instantiate(prefab);
        Renderer renderer = tempPincho.GetComponent<Renderer>();

        if (renderer != null)
        {
            // Obtener el tamaño real del modelo
            Vector3 tamaño = renderer.bounds.size;

            // Destruir la instancia temporal
            Destroy(tempPincho);

            return tamaño;
        }
        else
        {
            Debug.LogError("El prefab no tiene un Renderer.");
            return Vector3.one; // Valor por defecto si falla
        }
    }
}
