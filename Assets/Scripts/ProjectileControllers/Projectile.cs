using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Anything that is a projectile and disapears after a certain amount of collisions with opponent. Also contains some variables used by other classes.
public class Projectile : MonoBehaviour {
    //How many collisions the bullet will have till it is destroyed.
    public int hitsAllowed = 1;
    private int hitCount = 0;
    public int collisionsAllowed = 1;
    public int collisionCount =0;
    //The following two variables are for things like vampire bullets, or bullets that fire the gun again when they hit. Just references to be used by other classes.
    public GameObject firer;
    public GameObject spawner;
    //How far away the bullet will spawn from gun. Used it ToolController
    public float distance;
    public Sendable to;
    public Faction fs;
    private void Start()
    {

        to = GetComponent<Sendable>();
    }
    public void OnCollisionEnter(Collision collision)
    {//could do broadcast from  Sendable
        if (collisionsAllowed >= 0)
        {
            collisionCount += 1;
            if (collisionCount >= collisionsAllowed)
            {
                Destroy(gameObject);
            }
        }
        if (to.IsReceiver(collision.gameObject))
        {
            if (hitsAllowed >= 0)
            {
                hitCount += 1;
                if (hitCount >= hitsAllowed)
                {
                    Destroy(gameObject);
                }
            }
        }
        //possibly
    }
}