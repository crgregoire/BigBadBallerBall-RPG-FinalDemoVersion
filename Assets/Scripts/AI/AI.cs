using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//This class controls player movement and rotation relative to a target. 
//Ask Jason on the phone or in person and I can explain further whichever part you need to know.
public class AI : MonoBehaviour
{
    #region vars
    public float distance = 0;
    public int direction = 1;
    public int retreatDuration = 3;
    public bool kites = false;
    public bool relative = true;
    public bool charges = false;
    public bool movesFacing = false;
    public bool hold = false;
    public float pointSpeed = 0.1f;
    private Targeting targetScript;
    private Movement ms;
    public bool reholding = false;
    public float holdRange = 0.01f;
    public Vector3 holdPoint;
    public Rigidbody rb;
    private Sendable sendScript;
    public Trigger ts;
    public Vector3 basePoint;

    public int pursueRange = 10;

    //public Actor noTarget;
    #endregion
    public void Start(){
       
        ms = GetComponent<Movement>();
        //noTarget = Forward;
        targetScript = GetComponent<Targeting>();
    sendScript = GetComponent<Sendable>();
        holdPoint = gameObject.transform.position;
        if(basePoint == new Vector3())
        {
            basePoint = gameObject.transform.position;
        }
        rb = GetComponent<Rigidbody>();
        //The way to set triggers NOTE THIS IT IS REALLY IMPORTANT
        //See Trigger.cs for more info
        ts = gameObject.AddComponent<Trigger>();
        //this is the condition of the trigger. 
        ts.condition = () => Vector3.Magnitude(holdPoint - gameObject.transform.position) < holdRange;
        //this is a setter function, you could also just set with ts.active = () => {blah(); blah();};
        ts.Set(active: () => Movement.Slow(rb:ms.rb, speed:ms.speed), inactive: () => ms.defaultMovement(Vector3.Normalize(holdPoint - gameObject.transform.position), ms.rb,ms.speed));
        //name not really necessary
        ts.name = "hold motion trigger";
        //Only have this trigger active if hold is set.
        ts.enabled = hold;

    }
    //Toggles between hold states
    public void SetHold(bool isHolding)
    {
        ts.enabled = isHolding;
        hold = isHolding;
    }
    //The initial hold setting. Adds a hold point
    public void SetHold()
    {
        holdPoint = gameObject.transform.position;
        hold = true;
    }
    //The function that is called to make gunners stay at a range. It sets movement direction of AI.
    void Kite(){
        float dist = Vector3.Distance(transform.position, targetScript.target.transform.position);
        if (dist > distance)
        {
            direction = 1;
        }
        if(dist < distance - 5) { 
            direction = -1;
        }   

    }
    //The rest is just a bunch of conditions checked based on AI variables.
void Update()
{
        //if out of pursue range from base, move towards hold point.
        Vector3 baseDistance = basePoint - gameObject.transform.position;
        hold = baseDistance.sqrMagnitude > pursueRange * pursueRange;
        
        Vector3 where = Vector3.zero;
        if (targetScript.target) 
    {
            //Movement based on target
          
            Vector3 relativePos = targetScript.target.transform.position - transform.position;

                if (kites)
                {
                    Kite();
                }

                Vector3 forward;

                if (movesFacing)
                {

                    forward = transform.forward;
                }
                else
                {

                    forward = Vector3.Normalize(relativePos);
                }
                if (!relative)
                {
                    where = direction * Vector3.Normalize(Vector3.Scale(forward, new Vector3(1, 0, 1)));
                }
                else
                {
                    where = direction * forward;
                }
            
            //Rotation Based on target
            if (pointSpeed > 0)
            {
                if (pointSpeed > 10)
                {
                    transform.LookAt(targetScript.target.transform);
                }
                else
                {
                    Quaternion rotation = Quaternion.LookRotation(relativePos);
                    if (transform.rotation != rotation)
                    {
                        //still happens in pause... Oops
                        rotation = Quaternion.Slerp(transform.rotation, rotation, Time.time * pointSpeed);
                        /*
                        print(rotation.eulerAngles + " " + transform.rotation.eulerAngles);
                        rb.AddTorque(rotation.eulerAngles - transform.rotation.eulerAngles);
                        */
                        transform.rotation = rotation;
                    }
                }
            }

           
        }
        else
        {
}


        if (hold)
        {
            Vector3 difference = holdPoint - gameObject.transform.position;
            where = Vector3.Normalize(difference);
        }
        try
        {
            ms.defaultMovement(where, ms.rb, ms.speed);
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ms.name + ms.rb + gameObject.transform.parent.name);
        }
    }
void OnCollisionEnter(Collision collision)
    {//delegates are the answer if this section gets bogged down
            checkRetreat(collision.gameObject);
    }
    void OnTriggerEnter(Collider collision)
    {
        checkRetreat(collision.gameObject);
    }
    void checkRetreat(GameObject obj)
    {
        if (charges)
        {
            //print(sendScript);
            if (sendScript.IsReceiver(obj))
            {
                Retreat();
            }
        }
    }
    public void Retreat()
    {
        if (direction > 0)
        {
            Reverse();
            Invoke("Reverse", retreatDuration);
        }
    }


    public void Reverse()
{
        direction *= -1;
}
}
