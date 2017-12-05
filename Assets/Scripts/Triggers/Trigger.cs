using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Trigger : MonoBehaviour {
    //Active is only used if trigger is currently active and was last frame.
    //Activate function is executed if the condition is set to true from a false state.
    //Inactive is the opposite of Active
    //Deactivate is the opposite of Activate
    public Actor active;
    public Actor activate;
    public Actor deactivate;
    public Actor inactive;
    public string name = "generic";
    public Condition condition;
    public Targeting ts;
    private bool triggered = false;
    void Start()
    {
        this.activate = activate ?? No.Nothing;
        this.deactivate = deactivate ?? No.Nothing;
        this.active = active ?? No.Nothing;
        this.inactive = inactive ?? No.Nothing;
        ts = GetComponent<Targeting>();
        if (condition == null)
        {
            condition = () => { print("no condition"); return false; };
        }
    }
    //Just a setter function
    public void Set(Actor activate = null, Actor deactivate = null, Actor active = null, Actor inactive = null)
    {
        this.activate = activate ?? No.Nothing;
        this.deactivate = deactivate ?? No.Nothing;
        this.active = active ?? No.Nothing;
        this.inactive = inactive ?? No.Nothing;
    }

    void Update()
    {
        //this is where the trigger functions are called.
        if (condition())
        {
            if (!triggered)
            {
                triggered = true;
                activate();
            }
            else
            {
                active();
            }
        }
        else
        {
            if (triggered)
            {
                triggered = false;
                deactivate();
            }
            else
            {
                inactive();
            }
        }
    }
}
