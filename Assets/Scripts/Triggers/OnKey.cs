using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//Executes an event on a key stroke.
public class OnKey : MonoBehaviour {
    
    public UnityEvent myUnityEvent;
    public KeyCode key;
    void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            myUnityEvent.Invoke();
        }
    }
}
