using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovement : MonoBehaviour
{
    public float initial_velocity = 10.0f;
    public float gravity = 10.0f;
    public int res = 15;

    private Rigidbody playerRb;
    private LineRenderer lineRenderer;
    private Vector3 dragStartPos;
    private Vector3 dragEndPos;
    private bool isDragging = false;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer component not found on the GameObject.");
            return;
        }

        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragStartPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            dragEndPos = Input.mousePosition;
            Vector3 dragVector = dragEndPos - dragStartPos;

            // Calculate angle and velocity based on drag along z-axis
            float dragDistance = dragVector.magnitude;
            float initial_angle = Mathf.Atan2(dragVector.y, dragVector.x) * Mathf.Rad2Deg;
            initial_velocity = dragDistance * 0.1f; // Adjust the multiplier to scale the velocity appropriately
            Trajectory(initial_velocity, initial_angle);
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            Vector3 jumpDirection = CalculateJumpDirection();
            playerRb.AddForce(jumpDirection, ForceMode.Impulse);
        }
    }

    Vector3 CalculateJumpDirection()
    {
        Vector3 dragVector = dragEndPos - dragStartPos;
        float initial_angle = Mathf.Atan2(dragVector.y, dragVector.x) * Mathf.Rad2Deg;
        float angleRad = initial_angle * Mathf.Deg2Rad;
        float initialVerticalVelocity = initial_velocity * Mathf.Sin(angleRad);
        float initialForwardVelocity = initial_velocity * Mathf.Cos(angleRad);
        Vector3 jumpDirection = (transform.forward * initialForwardVelocity) + (Vector3.up * initialVerticalVelocity);
        return jumpDirection;
    }

    void Trajectory(float velocity, float angle)
    {
        Vector3[] trajectory_points = new Vector3[res];
        float angleRad = angle * Mathf.Deg2Rad;
        float totalTime = (2 * velocity * Mathf.Sin(angleRad)) / gravity;
        float timeStep = totalTime / res;

        for (int i = 0; i < res; i++)
        {
            float time = i * timeStep;
            trajectory_points[i] = CalculatePosition(time, velocity, angleRad);
        }

        lineRenderer.positionCount = trajectory_points.Length;
        lineRenderer.SetPositions(trajectory_points);
    }

    Vector3 CalculatePosition(float time, float velocity, float angleRad)
    {
        float z = velocity * time * Mathf.Cos(angleRad);
        float y = velocity * time * Mathf.Sin(angleRad) - 0.5f * gravity * time * time;
        return transform.position + (Vector3.forward * z) + (Vector3.up * y);
    }
}
