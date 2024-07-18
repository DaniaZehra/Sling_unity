using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float initial_velocity = 10.0f;
    public float initial_angle = 45.0f;
    private Rigidbody playerRb;
    private LineRenderer lineRenderer;
    public float gravity = 10.0f;
    private float JumpForce;
    public int res = 15;
    private float forwardJumpForce;
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); 
        lineRenderer =  GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        CalculateJumpForce();
        Trajectory();
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Vector3 jumpDirection = Vector3.up*JumpForce + transform.forward *forwardJumpForce;
            playerRb.AddForce(jumpDirection,ForceMode.Impulse);
        }
    }
    void CalculateJumpForce(){
        float Angle_Rad = initial_angle*Mathf.Deg2Rad;
        float initial_vertical_velocity = initial_velocity *Mathf.Sin(Angle_Rad);
        float initial_horizontal_velocity = initial_velocity *Mathf.Cos(Angle_Rad);
        JumpForce = initial_vertical_velocity*playerRb.mass;
        forwardJumpForce = initial_horizontal_velocity*playerRb.mass;
    }
    void Trajectory(){
        Vector3[] trajectory_points = new Vector3[res];
        float Ang_Rad = initial_angle*Mathf.Deg2Rad;
        float Total_time = (2*initial_velocity*Mathf.Sin(Ang_Rad))/gravity;
        float TimeStep = Total_time/res;
        for(int i=0;i<res;i++){
            float time = i*TimeStep;
            trajectory_points[i] = CalculateEndPoint(time,initial_velocity,Ang_Rad); 
        }
        lineRenderer.positionCount = trajectory_points.Length;
        lineRenderer.SetPositions(trajectory_points);

        
    }
    Vector3 CalculateEndPoint(float time, float initial_velocity, float Angle_Rad){
        float x = initial_velocity * time * Mathf.Cos(Angle_Rad);
        float y = initial_velocity * time * Mathf.Sin(Angle_Rad) - ((time*time*gravity)/2);
        Vector3 forwardDirection = transform.forward;
        return transform.position + (forwardDirection * x) + (Vector3.up * y);
    }
}
