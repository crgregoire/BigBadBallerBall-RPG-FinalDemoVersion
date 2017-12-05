using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Adding this to anything decides how long it exists before disapearing. Usually for bullet disapearing.
public class Duration : MonoBehaviour {
    public float duration = 5;
    public bool useDuration = true;
    private float timeLeft;
    void Start()
    {
            timeLeft = duration;
    }

    void FixedUpdate()
    {
        if (useDuration)
        {
            if (timeLeft < 0)
            {
                Destroy(gameObject);
            }
            timeLeft -= Time.deltaTime;
        }
    }
}
