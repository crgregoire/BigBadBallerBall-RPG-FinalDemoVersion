using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//All gear uses this. It helps keep the player balanced so it is not hard to move. Far from perfect, but it definitely helps.
public class CenterMass : MonoBehaviour {
    public Vector3 relativePos;
    // Use this for initialization
    public void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = relativePos;
    }

}
