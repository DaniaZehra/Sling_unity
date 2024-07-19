using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private Vector3 dragStartPosition;
    private Vector3 dragEndPosition;
    private Vector3 direction;
    private float power = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        dragStartPosition = Input.mousePosition;
        Debug.Log("Drag Start Position: " + dragStartPosition);
    }

    void OnMouseUp()
    {
        dragEndPosition = Input.mousePosition;
        Debug.Log("Drag End Position: " + dragEndPosition);

        direction = dragStartPosition - dragEndPosition;
        direction.z = direction.y;
        direction.y = 0;

        rb.AddForce(direction * power);
        Debug.Log("Direction: " + direction + " | Force Applied: " + direction * power);
    }
}

