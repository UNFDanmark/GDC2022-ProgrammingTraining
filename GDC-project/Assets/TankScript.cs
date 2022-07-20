using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankScript : MonoBehaviour
{
    public float speed = 1f;
    public float turnSpeed = 1f;
    public Rigidbody rb;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.zero;
        float turnInput = Input.GetAxis("Horizontal");
        float moveInput = Input.GetAxis("Vertical");

        rb.AddForce(transform.forward * speed * moveInput);// =  Time.deltaTime;
        transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.deltaTime);
    }
}
