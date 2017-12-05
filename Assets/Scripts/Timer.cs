using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
//this is a simple timer class. You check if timer is up with Ready(); You may notice this is not used every where I need timing. It is a recent addition.
public class Timer : Object {
    public float lastUse;
    public float duration = -1;
    public Timer(float dur)
    {
        duration = dur;
    }
    public bool Ready() {
         if (Time.time > lastUse + duration)
        {
            lastUse = Time.time;
            return true;
        }
        else
        {
            return false;
        }
    }
}
