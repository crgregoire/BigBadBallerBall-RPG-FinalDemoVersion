using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour {
    public List<string> enemies = new List<string>();
    public List<string> allies = new List<string>();
    public string faction = "Enemy";
    private void Start()
    {
        if(faction == "Enemy")
        {
            allies.Add("Enemy");
            enemies.Add("Player");
        }
        if(faction == "Player")
        {
            enemies.Add("Enemy");
            allies.Add("Player");
        }
    }
    public static Faction GetFactionScript(GameObject go)
    {
        Projectile ps = go.GetComponent<Projectile>();
        if (ps && ps.fs && ps.firer)
        {
            return ps.fs;
        }
            Faction fs = go.GetComponent<Faction>();
        
        if (!fs)
        {
            Transform parent = go.transform.parent;
            if (parent)
            {
                fs = GetFactionScript(parent.gameObject);
            }
            else
            {
                
                print("object has no factioned parent");
            }
        }
        return fs;
    }

}
