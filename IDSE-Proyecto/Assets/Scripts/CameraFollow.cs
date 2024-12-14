using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;    // Objeto a seguir (cohete)
    [SerializeField] private Vector3 offset;     // Posici�n base de la c�mara
    [SerializeField] private float followSpeed = 5f; // Velocidad de seguimiento
    [SerializeField] private float maxForwardDistance = 5f; // Distancia m�xima de adelantamiento
    [SerializeField] private float speedThreshold = 10f; // Velocidad m�xima del cohete para calcular el adelantamiento

    private Rigidbody targetRb; // Referencia al Rigidbody del cohete

    void Start()
    {
        if (target != null)
        {
            targetRb = target.GetComponent<Rigidbody>();
            if (targetRb == null)
            {
                Debug.LogError("El objeto objetivo no tiene un Rigidbody.");
            }
        }
    }

    void LateUpdate()
    {
        if (target != null && targetRb != null)
        {
            // Magnitud de la velocidad del cohete
            float currentSpeed = targetRb.velocity.magnitude;

            // Calcular cu�nto adelantarse proporcional a la velocidad
            float forwardFactor = Mathf.Clamp(currentSpeed / speedThreshold, 0, 1); // Normalizado entre 0 y 1
            Vector3 forwardOffset = targetRb.velocity.normalized * (maxForwardDistance * forwardFactor);

            // Nueva posici�n de la c�mara
            Vector3 desiredPosition = target.position + offset + forwardOffset;

            // Mover la c�mara suavemente hacia la posici�n deseada
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        }
    }
}
