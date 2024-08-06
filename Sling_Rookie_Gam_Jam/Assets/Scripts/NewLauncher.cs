using UnityEngine;

public class NewLauncher : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public Transform shootPoint; 
    public float projectileSpeed = 10f; 
    public int trajectoryPoints = 30;
    public float timeBetweenPoints = 0.1f;
    private bool isAiming;
    private LineRenderer lineRender;

    void Start(){
        lineRender = GetComponent<LineRenderer>();
        lineRender.material = new Material(Shader.Find("Sprites/Default"));
        lineRender.startWidth = 0.1f;
        lineRender.endWidth = 0.1f;
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
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = shootPoint.forward * projectileSpeed;
    }
    void ShowTrajectory(){
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
