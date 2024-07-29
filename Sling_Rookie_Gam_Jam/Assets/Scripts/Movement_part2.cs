using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_part2 : MonoBehaviour
{
    public Transform launchPoint;
    public Vector3 startMousePosition;
    public Vector3 endMousePosition;
    bool IsAiming;
    public float maxLaunchForce = 7.5f;
    public LineRenderer trajectoryLine;
    public bool destroyed = false;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        trajectoryLine = GetComponent<LineRenderer>();
        trajectoryLine.material = new Material(Shader.Find("Sprites/Default"));
        trajectoryLine.startWidth = 0.1f;
        trajectoryLine.endWidth = 0.1f;
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.name=="Plane"){
            Destroy(gameObject);
        }
    }

    public void UpdateAim(Vector3 start, Vector3 end)
    {
        startMousePosition = start;
        endMousePosition = end;
        Vector3 aimDirection = (endMousePosition - startMousePosition).normalized;
        float aimDistance = Vector3.Distance(endMousePosition, startMousePosition);
        float launchForce = Mathf.Clamp(aimDistance, 0, maxLaunchForce);

        ShowTrajectory(aimDirection, launchForce);
    }

    public void Launch(Vector3 start, Vector3 end)
    {
        startMousePosition = start;
        endMousePosition = end;

        Vector3 aimDirection = (endMousePosition - startMousePosition).normalized;
        float aimDistance = Vector3.Distance(endMousePosition, startMousePosition);
        float launchForce = Mathf.Clamp(aimDistance, 0, maxLaunchForce);

        rb.isKinematic = false;
        rb.velocity = aimDirection * launchForce; // Set velocity directly

    }

    void ShowTrajectory(Vector3 direction, float force)
    {
        int resolution = 20;
        float stepSize = 0.1f;
        Vector3[] points = new Vector3[resolution];

        for (int i = 0; i < resolution; i++)
        {
            float t = i * stepSize;
            points[i] = launchPoint.position + direction * force * t;
        }

        trajectoryLine.positionCount = resolution;
        trajectoryLine.SetPositions(points);
    }
}
