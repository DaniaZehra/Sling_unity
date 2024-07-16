using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float JumpForce = 5.0f;
    public float forwardJumpForce = 5.0f;
    private Rigidbody playerRb;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
    
        if(Input.GetKeyDown(KeyCode.Space)){
            Vector3 jumpDirection = Vector3.up*JumpForce + transform.forward *forwardJumpForce;
            playerRb.AddForce(jumpDirection,ForceMode.Impulse);
        }
    }
}
