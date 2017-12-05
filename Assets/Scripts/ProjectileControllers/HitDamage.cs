using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Damage applied by a projectile
public class HitDamage : MonoBehaviour {
    public int damage = 1;
    private Sendable to;
    public Projectile pro;
    public Modifiers ms;
    void Start()
    {
        ms = GetComponent<Modifiers>();
        if (!ms) {
            ms = GetComponentInParent<Modifiers>();
        }
        to = GetComponent<Sendable>();
    }

    public void OnCollisionEnter(Collision impact)
    {
        if (to.IsReceiver(impact.gameObject))
        {
            int theDamage = Mathf.FloorToInt((damage + ms.HitDamagePlus) * ms.HitDamageMult);
            Health.Damage(impact.gameObject,theDamage);
        }

    }
}
