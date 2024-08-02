using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform spawnPosition;
    public float maxLaunchForce = 35f;
    private GameObject currentBall;
    private Rigidbody rb;

    private bool isAiming;
    private Vector3 startMousePosition;
    private Vector3 endMousePosition;

    public LineRenderer trajectoryLine;
    public float recoilForceMagnitude = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SpawnObject();

        trajectoryLine = GetComponent<LineRenderer>();
        trajectoryLine.material = new Material(Shader.Find("Sprites/Default"));
        trajectoryLine.startWidth = 0.1f;
        trajectoryLine.endWidth = 0.1f;
    }

    void Update()
    {
        HandleAimingAndShooting();
        if(currentBall == null){
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        currentBall = Instantiate(ballPrefab, spawnPosition.position, spawnPosition.rotation);
    }

    void HandleAimingAndShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isAiming = true;
            startMousePosition = Input.mousePosition;

        if (currentBall == null)
        {
            SpawnObject();
        }
        }

        if (Input.GetMouseButton(0) && isAiming && currentBall!=null)
        {
            endMousePosition = Input.mousePosition;
            Movement_part2 movementScript = currentBall.GetComponent<Movement_part2>();
            if (movementScript != null)
            {
                movementScript.UpdateAim(startMousePosition, endMousePosition);
            }
        }

        if (Input.GetMouseButtonUp(0) && isAiming && currentBall!=null)
        {
            isAiming = false;
            endMousePosition = Input.mousePosition;
            trajectoryLine.positionCount = 0;

            Movement_part2 movementScript = currentBall.GetComponent<Movement_part2>();
            if (movementScript != null)
            {
                movementScript.Launch(startMousePosition, endMousePosition);
            }

            Vector3 aimDirection = (endMousePosition - startMousePosition).normalized;
            Vector3 recoilForce = -aimDirection * recoilForceMagnitude;
            rb.AddForce(recoilForce, ForceMode.Impulse);

        }
    }
}
