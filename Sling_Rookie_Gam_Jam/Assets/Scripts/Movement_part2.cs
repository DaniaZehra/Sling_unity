using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Movement_part2 : MonoBehaviour
{
    public Transform launchPoint;
    public Vector3 startMousePosition;
    public Vector3 endMousePosition;
    bool IsAiming;
    public float maxLaunchForce = 15f;
    public LineRenderer trajectoryLine;

    // Start is called before the first frame update
    void Start()
    {
        trajectoryLine = GetComponent<LineRenderer>();
        trajectoryLine.material = new Material(Shader.Find("Sprites/Default"));
        trajectoryLine.startWidth = 0.1f;
        trajectoryLine.endWidth = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsAiming = true;
            startMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && IsAiming)
        {
            endMousePosition = Input.mousePosition;
            UpdateAim();
        }

        if (Input.GetMouseButtonUp(0) && IsAiming)
        {
            IsAiming = false;
            endMousePosition = Input.mousePosition;
            trajectoryLine.positionCount = 0;
            SlingShot();
        }
    }

    void UpdateAim()
    {
        Vector3 aimDirection = (endMousePosition - startMousePosition).normalized;
        float aimDistance = Vector3.Distance(endMousePosition, startMousePosition);
        float launchForce = Mathf.Clamp(aimDistance, 0, maxLaunchForce);

        ShowTrajectory(aimDirection, launchForce);
    }

    void SlingShot()
    {
        Vector3 aimDirection = (endMousePosition - startMousePosition).normalized;
        float aimDistance = Vector3.Distance(endMousePosition, startMousePosition);
        float launchForce = Mathf.Clamp(aimDistance, 0, maxLaunchForce);

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(aimDirection * launchForce, ForceMode.Impulse);
    }

    void ShowTrajectory(Vector3 direction, float force)
    {
        int resolution = 30;
        float timeStep = 0.1f;

        Vector3[] points = new Vector3[resolution];
        for (int i = 0; i < resolution; i++)
        {
            float t = i * timeStep;
            points[i] = launchPoint.position + direction * force * t + Physics.gravity * t * t / 2;
        }

        trajectoryLine.positionCount = resolution;
        trajectoryLine.SetPositions(points);
    }
}
