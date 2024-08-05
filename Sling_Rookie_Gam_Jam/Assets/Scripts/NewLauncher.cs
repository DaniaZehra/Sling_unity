using UnityEngine;

public class AimAndShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // The object to shoot
    public Transform shootPoint; // The point from which the projectile is shot
    public float projectileSpeed = 10f; // The speed of the projectile

    void Update()
    {
        AimAtMouse();

        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Shoot();
        }
    }

    void AimAtMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPoint = hit.point;
            Vector3 direction = (targetPoint - shootPoint.position).normalized;

            shootPoint.forward = direction;
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = shootPoint.forward * projectileSpeed;
    }
}
