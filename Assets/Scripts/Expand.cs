using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expand : MonoBehaviour {

    public Vector3 speed;
	// Update is called once per frame
	void Update () {
        gameObject.transform.localScale += speed*Time.deltaTime;
	}
}
