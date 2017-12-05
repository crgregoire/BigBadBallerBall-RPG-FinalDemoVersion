using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//This class handles object movement. It has static functions so that this could be called for various affects on any object.
public class Movement : MonoBehaviour {
    public Rigidbody rb;
    public float speed = 9;
    public float grip = 0.9f;
    public MoveDel defaultMovement;
    public bool velocity = false;
    void Start () {
       if(defaultMovement == null)
        {
            if (velocity)
            {
                defaultMovement = Velocity;
            }
            else
            {
                defaultMovement = Accelerate;
            }
        }
        rb = GetComponent<Rigidbody>();
    }
    //Change to not be static
    public static void Slow(Vector3 direction = new Vector3(), Rigidbody rb = null, float speed = 0)
    {
        Vector3 vect = new Vector3(rb.velocity.x,0,rb.velocity.z) * Mathf.Max((100-speed),50)/100;
        vect.y = rb.velocity.y;
        rb.velocity = vect;
    }
    public static void None(Vector3 direction = new Vector3(), Rigidbody rb = null, float speed = 0)
    {

    }
    public static void Velocity(Vector3 direction = new Vector3(), Rigidbody rb = null, float speed = 0)
    {
        // print(direction);
            rb.GetComponent<Rigidbody>().velocity = direction * speed * Time.deltaTime * 30f;
            

    }
    public static void Accelerate(Vector3 direction = new Vector3(), Rigidbody rb = null, float speed = 0)
    {
            rb.AddForce(direction * speed * Time.deltaTime * 30);

    }


}
