using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This is a for repulsion walls. Anything that collides with them is pushed in the repulseDirection.
public class Repulse : MonoBehaviour {
    private Vector3 position;
    public Vector3 repulseDirection;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        position = transform.position;
    }
    void Update()
    {
        transform.position = position;
        rb.velocity = repulseDirection;
    }
}
