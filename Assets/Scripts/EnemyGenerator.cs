using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this is the class that controls enemy spawning.
public class EnemyGenerator : MonoBehaviour {
    public float startTime;
	// Use this for initialization
	void Start () {
        startTime = Time.time;
        InvokeRepeating("MakeCharger", 15f, 7f);

        InvokeRepeating("MakeHeavy", 60f, 45f);

        InvokeRepeating("MakeGunner", 350f, 35f);
    }
    void MakeCharger()
    {
        GameObject charger = Create.Unit(RandomPosition(), "ChargerBody", "Enemy", level: 1);
        Create.AddReward(charger);
    }
    public void MakeHeavy()
    {
        GameObject heavy = Create.Unit(RandomPosition(), "HeavyBody", "Enemy", level: 10);
        Create.AddReward(heavy,3);
    }
    public void MakeGunner()
    {
        GameObject gunner = Create.Unit(RandomPosition(), "KiterBody", "Enemy", "Gunny", level: 1);
        gunner.GetComponent<Modifiers>().reload = 2f;
        Create.AddReward(gunner, 3);
    }
    // Update is called once per frame
    public Vector3 RandomPosition()
    {
        Vector2 direction = Random.insideUnitCircle;
        float distance = Random.Range(0,30);
        Vector2 position = (direction * distance)+(direction * 40);
        Vector3 location = new Vector3(position.x,0.5f,position.y);
        return location;
    }
    void Update () {
	}
}
