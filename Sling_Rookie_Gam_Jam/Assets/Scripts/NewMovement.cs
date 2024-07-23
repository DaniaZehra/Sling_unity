using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float initial_velocity = 10.0f;
    public float gravity = 10.0f;
    public int res = 15;

    private Rigidbody playerRb;
    private LineRenderer lineRenderer;
    private Vector3 dragStartPos;
    private Vector3 dragEndPos;
    private float velocity_multiplier = 0.75f;
    private bool isDragging = false;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();

        playerRb.useGravity = false;
        playerRb.isKinematic = true;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }
     bool MouseClicked(){
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
             if(Physics.Raycast(ray,out hit)){
                if (hit.transform.name == "Sphere")
                {
                    return true;
                }
            }
            return false;
   }
    void Update()
    {
        if (MouseClicked())
        {
                isDragging = true;
                dragStartPos = Input.mousePosition;
           
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            dragEndPos = Input.mousePosition;
            Vector3 dragVector = dragEndPos - dragStartPos;
            float dragDistance = dragVector.magnitude*velocity_multiplier;
            float initial_angle = Mathf.Atan2(dragVector.y, dragVector.x) * Mathf.Rad2Deg;
            initial_velocity = dragDistance * velocity_multiplier;
            Trajectory(initial_velocity, initial_angle);
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            playerRb.useGravity = true;
            playerRb.isKinematic = false;
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
        float initialHorizontalVelocity = initial_velocity * Mathf.Cos(angleRad);
        Vector3 jumpDirection = (transform.forward * initialHorizontalVelocity) + (Vector3.up * initialVerticalVelocity);
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
        float x = velocity * time * Mathf.Cos(angleRad);
        float y = velocity * time * Mathf.Sin(angleRad) - 0.5f * gravity * time * time;
        Vector3 forwardDirection = transform.forward;
        return transform.position + (forwardDirection * x) + (Vector3.up * y);
    }
}