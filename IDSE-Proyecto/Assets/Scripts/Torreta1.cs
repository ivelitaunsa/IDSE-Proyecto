using UnityEngine;

public class Torreta : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float shootInterval = 5f;
    public float projectileSpeed = 10f;

    private float shootTimer;
    public GameObject player; // Referencia al jugador
    public bool isShooting = true; // Nueva variable pública para controlar el disparo

    void Update()
    {
        // Verificar si el jugador está activo
        if (player != null && !player.activeInHierarchy)
        {
            isShooting = false; // Detener el disparo si el jugador está desactivado
        }

        if (isShooting) // Comprueba si la torreta puede disparar
        {
            shootTimer += Time.deltaTime;

            if (shootTimer >= shootInterval)
            {
                Shoot();
                shootTimer = 0f;
            }
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * projectileSpeed;
        }
    }

    public void Reinicio()
    {
        isShooting = true;
    }
}
