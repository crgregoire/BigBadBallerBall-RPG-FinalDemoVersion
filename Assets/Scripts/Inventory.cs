using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public Dictionary<string, int> items = new Dictionary<string,int>();
    private Purchases ps;
    private void Start()
    {
        Add("Townhall");
        ps = gameObject.GetComponent<Purchases>();
    }
    public void Add(string name, int amount = 1)
    {
        if (!items.ContainsKey(name))
        {
            items.Add(name, amount);
        }
        else
        {
            items[name] += amount;
        }
    }
    public void Use(string name)
    {
        ps.Buy(name, gameObject, free: true);
        items[name] -= 1;
        if (items[name] < 1)
        {
            items.Remove(name);
        }
    }
}
