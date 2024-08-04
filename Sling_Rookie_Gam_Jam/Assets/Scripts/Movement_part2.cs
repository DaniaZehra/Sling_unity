using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_part2 : MonoBehaviour
{
    public Transform launchPoint;
    public Vector3 startMousePosition;
    public Vector3 endMousePosition;
    bool IsAiming;
    public float maxLaunchForce = 35f;
    private LineRenderer trajectoryLine;
    public bool destroyed = false;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        trajectoryLine = GetComponent<LineRenderer>();
        trajectoryLine.material = new Material(Shader.Find("Sprites/Default"));
        trajectoryLine.startWidth = 0.1f;
        trajectoryLine.endWidth = 0.1f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Hermit Robot Red")
        {
            Destroy(gameObject);
        }
    }


    public void UpdateAim(Vector3 start, Vector3 end)
    {
        startMousePosition = start;
        endMousePosition = end;

        Vector3 aimDirection = (endMousePosition - startMousePosition).normalized;

        ShowTrajectory(aimDirection, maxLaunchForce);
    }

    public void Launch(Vector3 start, Vector3 end)
    {
        startMousePosition = start;
        endMousePosition = end;

        Vector3 aimDirection = (endMousePosition - startMousePosition).normalized;
         Debug.Log("maunchForce: " + maxLaunchForce);

        rb.isKinematic = false;
        rb.velocity = aimDirection * maxLaunchForce;// Apply high initial velocity

        Debug.Log("velocity: " + rb.velocity);
        Debug.Log("launchForce: " + maxLaunchForce);
        Destroy(gameObject,2.0f);
    }

    void ShowTrajectory(Vector3 direction, float force)
    {
        int resolution = 35;
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
