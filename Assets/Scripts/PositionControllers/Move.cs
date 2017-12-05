using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class is used for objects like bullets that perpetually use their forward motion type.
public class Move : MonoBehaviour {
    Movement ms;
	// Use this for initialization
	void Start () {
        ms = GetComponent<Movement>();
        
	}
	
	// Update is called once per frame
	public void Update () {
        ms.defaultMovement(transform.forward, ms.rb, ms.speed);
    }
}
