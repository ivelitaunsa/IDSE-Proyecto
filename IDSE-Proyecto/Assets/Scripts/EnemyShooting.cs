using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // El prefab del proyectil
    public Transform firePoint;        // El punto desde donde se dispara
    public float shootInterval = 5f;   // Intervalo de disparo en segundos
    public float projectileSpeed = 10f; // Velocidad del proyectil

    private float shootTimer;

    void Update()
    {
        shootTimer += Time.deltaTime;

        // Disparar cada 5 segundos
        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f; // Reinicia el temporizador
        }
    }

    void Shoot()
    {
        // Crear el proyectil en el firePoint
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Aplicar velocidad al proyectil
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * projectileSpeed; // Hacia adelante en base a la rotación
        }
    }
}
