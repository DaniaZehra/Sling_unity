using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float initial_velocity = 10.0f;
    public float initial_angle = 45.0f;
    private Rigidbody playerRb;
    public float gravity = 10.0f;
    private float JumpForce;
    private float forwardJumpForce;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); 
        CalculateJumpForce();
    }

    // Update is called once per frame
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
    void CalculateEndPoint(){
        float Angle_Rad = initial_angle*Mathf.Deg2Rad;
        float range = (initial_velocity * initial_velocity * Mathf.Sin(2*Angle_Rad))/gravity;
        float height = (initial_velocity*initial_velocity*Mathf.Sin(Angle_Rad)*Mathf.Sin(Angle_Rad))/2*gravity;
        Vector3 endpoint = new Vector3(range, height, 0f);
    }
}
