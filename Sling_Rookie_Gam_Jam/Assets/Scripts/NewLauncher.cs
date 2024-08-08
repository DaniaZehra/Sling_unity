using UnityEngine;
using System.Collections;

public class NewLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint; // Ensure this is assigned in the Inspector
    public float projectileSpeed = 50f; // Increased projectile speed for a straighter trajectory
    public int trajectoryPoints = 30;
    public float timeBetweenPoints = 0.1f;
    public float recoilForce = 0.05f; // Very small recoil force magnitude
    private bool isAiming;
    private LineRenderer lineRender;
    private Rigidbody rb;
    private Animator animator;
    public double t = 0.4f;

    void Start()
    {
        lineRender = GetComponent<LineRenderer>();
        lineRender.material = new Material(Shader.Find("Sprites/Default"));
        lineRender.startWidth = 0.1f;
        lineRender.endWidth = 0.1f;
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
        animator = GetComponent<Animator>(); // Get the Animator component if available
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isAiming = true;
        }

        if (isAiming)
        {
            AimAtMouse();
            ShowTrajectory();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isAiming)
            {
                Shoot();
                isAiming = false;
            }
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
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.velocity = shootPoint.forward * projectileSpeed;

        StartCoroutine(ApplyRecoil()); // Apply recoil to the launcher
        t += 0.01;
}

    IEnumerator ApplyRecoil()
    {
        Vector3 recoilDirection = -shootPoint.forward; // Recoil is in the opposite direction of the shot
        recoilDirection.y = 0; // Zero out the y-component to keep y-axis constant
        float startTime = Time.time;

        while (Time.time < startTime + t)
        {
            if (animator != null && animator.enabled)
            {
                animator.enabled = false; // Disable Animator during recoil
            }

            rb.AddForce(recoilDirection * recoilForce, ForceMode.Impulse); // Apply an impulse force
            yield return null; // Wait until the next frame
        }

        if (animator != null)
        {
            animator.enabled = true; // Re-enable Animator after recoil
        }

        // Optionally, stop the launcher completely after the recoil duration
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Debug.Log("Recoil applied with force: " + (recoilDirection * recoilForce).ToString());
    }

    void ShowTrajectory()
    {
        Vector3[] points = new Vector3[trajectoryPoints];
        Vector3 startingPosition = shootPoint.position;
        Vector3 startingVelocity = shootPoint.forward * projectileSpeed;

        for (int i = 0; i < trajectoryPoints; i++)
        {
            float time = i * timeBetweenPoints;
            Vector3 point = startingPosition + startingVelocity * time;
            point.y = startingPosition.y;

            points[i] = point;
            if (i > 0 && Physics.Linecast(points[i - 1], points[i], out RaycastHit hit))
            {
                points[i] = hit.point;
                lineRender.positionCount = i + 1;
                lineRender.SetPositions(points);
                return;
            }
        }

        lineRender.positionCount = trajectoryPoints;
        lineRender.SetPositions(points);
    }
}
