using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//UNUSED class prototype for balancing objects. Would only come in hand with very heavy masses. Generates a ghost object with a weight opposite about the player.
public class AutoBalance : MonoBehaviour {
    //This class adds a mirror object for balance purposes. It is stupid but it is the only way I can think of. Seriously starting to get annoyed at physics. :(
    // Use this for initialization
    public bool justBalance = true;
    
	void Start () {
        Transform body = GetComponent<ConfigurableJoint>().connectedBody.transform;
        Vector3 pos = body.position - gameObject.transform.position;
        pos.y = gameObject.transform.position.y;
        if (justBalance)
        {
/*
            Component[] comps = gameObject.GetComponents<Component>();
            List<string> allowed = new List<string>();
            string[] allowedArray = {"UnityEngine.Rigidbody", "UnityEngine.Transform", "CenterMass"};
            allowed.AddRange(allowedArray);
            for (int i = 0; i < comps.Length; i++)
            {
                Component c = comps[i];
                Type t = c.GetType();
                if(all !=)
                Destroy(c);
                print(t);
            }
            */
        }
        else
        {
            

            GameObject balanceWeight = Instantiate(gameObject, pos, transform.rotation,transform.parent);
            Destroy(balanceWeight.GetComponent<AutoBalance>());
        }
  
	}
}
