using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Adding this to a projectile will mean that the projectile now pushes away opponents on collision in a given radius.
public class ExplosionEffect : MonoBehaviour {
    public float radius = 100.0F;
    public float power = 1000.0F;
    private Sendable to;
    public Projectile pro;
    void Start()
    {
        to = GetComponent<Sendable>();
        pro = GetComponent<Projectile>();
    }

    void OnCollisionEnter(Collision impact)
    {
        if (to.IsReceiver(impact.gameObject))
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {

                if (to.affected.Contains(hit.gameObject.tag))
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();

                    if (rb != null)
                    {
                        rb.AddExplosionForce(power, explosionPos, radius, power);
                    }
                }
            }
        }
	}
}
