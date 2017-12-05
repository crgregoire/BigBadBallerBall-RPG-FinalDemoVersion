using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Used for health bars. Makes them always face the camera.
public class AlwaysVisible : MonoBehaviour
{

    void LateUpdate()
    {
         var fwd = Camera.main.transform.forward;
        transform.rotation = Quaternion.LookRotation(fwd);
       
    }

}
