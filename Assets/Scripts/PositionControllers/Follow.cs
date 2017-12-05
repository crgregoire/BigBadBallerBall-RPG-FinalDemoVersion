using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This is used for health bars. It makes an object be at another plus an offset.
public class Follow : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.

    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = target.transform.position + offset;
    }
}
