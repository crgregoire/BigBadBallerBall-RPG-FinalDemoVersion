using UnityEngine;
//Adding this to a projectile will mean that when the projectile hits something it will apply impact damage.
public class ImpactDamage : MonoBehaviour
{
    public float impactDamage = 1;
    private Sendable to;
    public Timer effectTimer;
    //Recharge for damage. Swords can charge over time.
    public float wait = 4;
    private Projectile pro;
    void Start()
    {
        effectTimer = new Timer(wait);
       to = GetComponent<Sendable>();
        pro = GetComponent<Projectile>();
    }

    public void OnCollisionEnter(Collision impact)
    {

        if (effectTimer.Ready())
        {
            if (to.IsReceiver(impact.gameObject))
            {
                int damage = Mathf.Max(Mathf.FloorToInt(impactDamage * impact.relativeVelocity.magnitude));
                Health.Damage(impact.collider.gameObject, damage);
            }
        }

    }
 }
